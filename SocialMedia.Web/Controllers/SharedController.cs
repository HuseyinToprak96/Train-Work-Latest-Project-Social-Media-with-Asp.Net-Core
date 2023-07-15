using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Web.Areas.Manager.Models.Dtos;
using SocialMedia.Web.Filters;
using System.IO;
using System;
using System.Threading.Tasks;
using SocialMedia.Web.ApiHandler;
using SocialMedia.Web.Helpers;
using SocialMedia.Web.Models.Dtos;
using SocialMedia.Web.Models.Enums;
using System.Linq;

namespace SocialMedia.Web.Controllers
{
    [LoginFilter]
    public class SharedController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RestApiHandler _restApiHandler;
        public SharedController(IWebHostEnvironment webHostEnvironment, RestApiHandler restApiHandler)
        {
            _webHostEnvironment = webHostEnvironment;
            _restApiHandler = restApiHandler;
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SharedDto sharedDto,IFormFile file)
        {
            var Token = CookieHelper.GetCookieValue(HttpContext, "Token");
            if (file != null && file.Length==0)
            {
                var path = await _restApiHandler.UploadImage(file, "File/Image");
                var uzanti = file.FileName.Split(".")[file.FileName.Split('.').Length - 1];
                var imagesUzantilar = new string[] { "jpg", "jpeg" };
                if (uzanti == "mp4")
                    sharedDto.Type = EFileType.Video;
                else if (imagesUzantilar.Contains(uzanti))
                    sharedDto.Type = EFileType.Image;

                sharedDto.Path = path;
                string folder = path;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }
            else
                sharedDto.Type = EFileType.Text;
            var result = await _restApiHandler.PostAsync<SharedDto,CustomResponseDto<SharedDto>>("Shared/Add", sharedDto, Token);
            return Redirect("/Home/Index");
        }
        public async Task<IActionResult> RepeatShared(int SharedId)
        {
            SharedDto sharedDto=new SharedDto { Id=SharedId , UserId=SessionHelper.GetSession(HttpContext,"ID")};
            var Token = CookieHelper.GetCookieValue(HttpContext, "Token");
            var result = await _restApiHandler.PostAsync<SharedDto,CustomResponseDto<SharedDto>>("Shared/RepeatShared", sharedDto, Token);
            return Redirect("/Home/Index");
        }
    }
}

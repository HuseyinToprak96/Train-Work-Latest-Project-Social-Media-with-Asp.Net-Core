using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Web.ApiHandler;
using SocialMedia.Web.Filters;
using SocialMedia.Web.Helpers;
using SocialMedia.Web.Models.Dtos;
using System.Threading.Tasks;

namespace SocialMedia.Web.Controllers
{
    [AllowAnonymous]
    [LoginFilter]
    public class ProfileController : Controller
    {
        private readonly RestApiHandler _restApiHandler;

        public ProfileController(RestApiHandler restApiHandler)
        {
            _restApiHandler = restApiHandler;
        }

        public async Task<IActionResult> Index()
        {
            var Token = CookieHelper.GetCookieValue(HttpContext, "Token");
            var response = await _restApiHandler.GetAsync<CustomResponseDto<ProfileDto>>("User/Profile", Token);
            return View(response.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserDto updateUserDto)
        {
            var Token = CookieHelper.GetCookieValue(HttpContext, "Token");
            var response = await _restApiHandler.PutAsync<UpdateUserDto,CustomResponseDto<NoDataDto>>("User/Update", updateUserDto, Token);
            return RedirectToAction("Index");
        }
    }
}

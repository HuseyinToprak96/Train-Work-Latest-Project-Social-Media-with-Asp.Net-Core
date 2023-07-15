using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.Web.ApiHandler;
using SocialMedia.Web.Helpers;
using SocialMedia.Web.Models.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly RestApiHandler _restApiHandler;

        public UserController(RestApiHandler restApiHandler)
        {
            _restApiHandler = restApiHandler;
        }

        public async Task<IActionResult> Search(int page=1,string search="")
        {
            var Token = CookieHelper.GetCookieValue(HttpContext, "Token");
            var response = await _restApiHandler.GetAsync<CustomResponseDto<List<UserAppDto>>>("User/GetUsers", Token);
            if(search!="")
            response.Data = response.Data.Where(x => x.Username.Contains(search)).ToList();
            var result = PagesHelper.Pages<UserAppDto>(response.Data, page);
            return View(result);
        }
    }
}

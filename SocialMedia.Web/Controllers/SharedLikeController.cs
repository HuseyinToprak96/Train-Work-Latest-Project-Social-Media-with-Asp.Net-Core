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
    public class SharedLikeController : Controller
    {
        private readonly RestApiHandler _restApiHandler;

        public SharedLikeController(RestApiHandler restApiHandler)
        {
            _restApiHandler = restApiHandler;
        }

        public async Task<IActionResult> Like(int sharedId)
        {
            var Token = CookieHelper.GetCookieValue(HttpContext, "Token");
            SharedLikeDto sharedLikeDto = new SharedLikeDto { SharedId=sharedId, UserId = SessionHelper.GetSession(HttpContext, "ID") };
            var result= await _restApiHandler.PostAsync<SharedLikeDto, CustomResponseDto<SharedLikeDto>>("Like/NewLike", sharedLikeDto, Token);
            return Redirect("/Home/Index");
        }
        public async Task<IActionResult> DisLike(int sharedId)
        {
            var Token = CookieHelper.GetCookieValue(HttpContext, "Token");
            string UserId = SessionHelper.GetSession(HttpContext, "ID");
            var result= await _restApiHandler.DeleteAsync("Like/DeleteLike?SharedId=" + sharedId+"&UserId="+UserId, Token);
            return Redirect("/Home/Index");
        }
    }
}

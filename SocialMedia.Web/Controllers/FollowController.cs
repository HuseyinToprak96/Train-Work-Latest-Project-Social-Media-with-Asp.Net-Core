using Microsoft.AspNetCore.Mvc;
using SocialMedia.Web.ApiHandler;
using SocialMedia.Web.Helpers;
using SocialMedia.Web.Models.Dtos;
using System.Threading.Tasks;

namespace SocialMedia.Web.Controllers
{
    public class FollowController : Controller
    {
        private readonly RestApiHandler _restApiHandler;

        public FollowController(RestApiHandler restApiHandler)
        {
            _restApiHandler = restApiHandler;
        }
        public async Task<IActionResult> Add(string userId)
        {
            var Token = CookieHelper.GetCookieValue(HttpContext, "Token");
            FollowDto followDto =new FollowDto { FollowId=SessionHelper.GetSession(HttpContext,"ID"),FollowingId=userId};
            var result = await _restApiHandler.PostAsync<FollowDto, CustomResponseDto<FollowDto>>("Follow/Add", followDto, Token);
            return Redirect("/User/Search");
        }
        public async Task<IActionResult> Remove(string userId,int transaction)
        {
            var Token = CookieHelper.GetCookieValue(HttpContext, "Token");
            var activeUserId = SessionHelper.GetSession(HttpContext,"ID");
            if (transaction == 1)
            {
                var result = await _restApiHandler.DeleteAsync("Follow/Remove?followId=" + userId + "&&followingId=" + activeUserId, Token);
            }
            else if (transaction == 2)
            {
                var result = await _restApiHandler.DeleteAsync("Follow/Remove?followId=" + activeUserId + "&&followingId=" + userId, Token);
            }
            return Redirect("/Profile/Index");
        }
    }
}

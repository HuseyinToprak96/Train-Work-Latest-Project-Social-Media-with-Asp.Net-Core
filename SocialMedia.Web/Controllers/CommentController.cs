using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Web.ApiHandler;
using SocialMedia.Web.Areas.Manager.Models.Dtos;
using SocialMedia.Web.Filters;
using SocialMedia.Web.Helpers;
using SocialMedia.Web.Models.Dtos;
using System;
using System.Threading.Tasks;

namespace SocialMedia.Web.Controllers
{
    [AllowAnonymous]
    [LoginFilter]
    public class CommentController : Controller
    {
        private readonly RestApiHandler _restApiHandler;

        public CommentController(RestApiHandler restApiHandler)
        {
            _restApiHandler = restApiHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CommentDto commentDto)
        {
            commentDto.UserId = SessionHelper.GetSession(HttpContext,"ID");
            var Token = CookieHelper.GetCookieValue(HttpContext, "Token");
            var response = await _restApiHandler.PostAsync<CommentDto, CustomResponseDto<CommentDto>>("Comment/Add", commentDto, Token);
            return Redirect("/Home/Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            string UserId =SessionHelper.GetSession(HttpContext, "ID");
            var Token = CookieHelper.GetCookieValue(HttpContext, "Token");
            var response= await _restApiHandler.DeleteAsync("Comment/Delete?Id="+id+"&UserId="+UserId, Token);
            return Redirect("/Home/Index");
        }
    }
}

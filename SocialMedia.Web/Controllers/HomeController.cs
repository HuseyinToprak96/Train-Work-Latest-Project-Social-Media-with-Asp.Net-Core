using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialMedia.Web.ApiHandler;
using SocialMedia.Web.Areas.Manager.Models.Dtos;
using SocialMedia.Web.Filters;
using SocialMedia.Web.Helpers;
using SocialMedia.Web.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Web.Controllers
{
    [AllowAnonymous]
    [LoginFilter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RestApiHandler _restApiHandler;
        public HomeController(ILogger<HomeController> logger, RestApiHandler restApiHandler)
        {
            _logger = logger;
            _restApiHandler = restApiHandler;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            var Token = CookieHelper.GetCookieValue(HttpContext, "Token");
            var response = await _restApiHandler.GetAsync<CustomResponseDto<List<SharedDto>>>("Shared/ListDetail?UserId="+SessionHelper.GetSession(HttpContext,"ID"), Token);
            var result=PagesHelper.Pages<SharedDto>(response.Data, page);
            return View(result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SocialMedia.Web.ApiHandler;
using SocialMedia.Web.Areas.Manager.Models.Dtos;
using SocialMedia.Web.Helpers;
using SocialMedia.Web.Models.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class DashboardController : Controller
    {
        private readonly RestApiHandler _restApiHandler;

        public DashboardController(RestApiHandler restApiHandler)
        {
            _restApiHandler = restApiHandler;
        }

        public async Task<IActionResult> Index()
        {
            var Token = CookieHelper.GetCookieValue(HttpContext, "Token");
            var newUsersResult = await _restApiHandler.GetAsync<CustomResponseDto<IEnumerable<UserAppDto>>>("ManagerUser/GetUsersNotShow", Token);
            var newSharedsResult = await _restApiHandler.GetAsync<CustomResponseDto<IEnumerable<SharedDto>>>("ManagerShared/NotShow", Token);
            var resultShowUser = await _restApiHandler.PostAsync <NoDataDto,CustomResponseDto<NoDataDto>>("ManagerUser/Show", new NoDataDto(),Token);
            var resultShowShared = await _restApiHandler.PostAsync<NoDataDto, CustomResponseDto<NoDataDto>>("ManagerShared/Show", new NoDataDto(), Token);

            return View(Tuple.Create(newSharedsResult.Data,newUsersResult.Data));
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Web.ApiHandler;
using SocialMedia.Web.Areas.Manager.Models.Dtos;
using SocialMedia.Web.Filters;
using SocialMedia.Web.Helpers;
using SocialMedia.Web.Models.Dtos;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    [AllowAnonymous]
    [LoginFilter]
    public class SharedController : Controller
    {
        private readonly RestApiHandler _restApiHandler;

        public SharedController(RestApiHandler restApiHandler)
        {
            _restApiHandler = restApiHandler;
        }

        public async Task<IActionResult> List()
        {
            var Token = CookieHelper.GetCookieValue(HttpContext,"Token");
            var response = await _restApiHandler.GetAsync<CustomResponseDto<List<SharedDto>>>("Shared/GetAll", Token);
            if (response.StatusCode==200)
            return View(response.Data);
            foreach (var error in response.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return View(response.Data);
        }
        public IActionResult Details(int id)
        {
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var Token = CookieHelper.GetCookieValue(HttpContext, "Token");
            await _restApiHandler.DeleteAsync($"Shared/Delete?id={id}", Token);
            return RedirectToAction("List");
        }
        public IActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(string shared)
        {
            return RedirectToAction("List");
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(string shared)
        {
            return RedirectToAction("List");
        }
    }
}

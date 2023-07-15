using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Web.ApiHandler;
using SocialMedia.Web.Models.Dtos;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialMedia.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly RestApiHandler _restApiHandler;

        public AuthController(RestApiHandler restApiHandler)
        {
            _restApiHandler = restApiHandler;
        }

        public IActionResult Login(int status=0)
        {
            if (status==1)
            {
                ViewBag.Message = "Üyeliğiniz Başarıyla Oluşturuldu";
            }
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _restApiHandler.PostAsync<LoginDto, CustomResponseDto<TokenDto>>("Auth/CreateToken",loginDto, "");
            if (response.StatusCode == 200)
            {
                var user = await _restApiHandler.GetAsync<CustomResponseDto<UserAppDto>>("User/GetUser", response.Data.AccessToken);
                HttpContext.Session.SetString("ID", user.Data.Id);
                HttpContext.Response.Cookies.Append("Token", response.Data.AccessToken);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Authentication,response.Data.AccessToken)
                };
                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto createUserDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _restApiHandler.PostAsync<CreateUserDto, CustomResponseDto<UserAppDto>>("User/CreateUser", createUserDto, "");
                return RedirectToAction("Login",new {status=1});
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

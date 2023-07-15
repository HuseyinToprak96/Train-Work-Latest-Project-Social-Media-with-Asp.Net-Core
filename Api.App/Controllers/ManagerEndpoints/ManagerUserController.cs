using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Types.Layer.Contracts.Interfaces;

namespace Api.App.Controllers.ManagerEndpoints
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class ManagerUserController : CustomBaseController
    {
        private readonly IUserOrchestration _userOrchestration;

        public ManagerUserController(IUserOrchestration userOrchestration)
        {
            _userOrchestration = userOrchestration;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersNotShow()
        {
            var result=await _userOrchestration.GetUsersNotShow();
            return ActionResultInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> Show()
        {
            var result =await _userOrchestration.Show();
            return ActionResultInstance(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userOrchestration.GetUsers();
            return ActionResultInstance(result);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(string Id)
        {
            var result = await _userOrchestration.GetUser(Id);
            return ActionResultInstance(result);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Types.Layer.Contracts.Interfaces;

namespace Api.App.Controllers.ManagerEndpoints
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerSharedController : CustomBaseController
    {
        private readonly ISharedOrchestration _sharedOrchestration;

        public ManagerSharedController(ISharedOrchestration sharedOrchestration)
        {
            _sharedOrchestration = sharedOrchestration;
        }

        [HttpGet]
        public IActionResult NotShow()
        {
            return ActionResultInstance(_sharedOrchestration.GetSharedsNotShow());
        }
        [HttpPost]
        public async Task<IActionResult> Show()
        {
            return ActionResultInstance(await _sharedOrchestration.SharedsShowDto());
        }
    }
}

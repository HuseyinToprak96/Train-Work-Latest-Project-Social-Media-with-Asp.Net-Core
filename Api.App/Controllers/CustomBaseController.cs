using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Types.Layer.Contracts.Dtos;

namespace Api.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        public IActionResult ActionResultInstance<T>(CustomResponseDto<T> response) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
        }
    }
}

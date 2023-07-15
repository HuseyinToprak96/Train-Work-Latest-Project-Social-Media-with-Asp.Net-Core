using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Types.Layer.Contracts.Dtos;
using Types.Layer.Contracts.Interfaces;
using Types.Layer.Dtos;

namespace Api.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : CustomBaseController
    {
        private readonly IMailOrchestration _mailOrchestration;

        public MailController(IMailOrchestration mailOrchestration)
        {
            _mailOrchestration = mailOrchestration;
        }

        [HttpPost]
        public IActionResult SendMail(MailDto mailDto)
        {
            if (_mailOrchestration.SendMail(mailDto))
                return ActionResultInstance(CustomResponseDto<NoDataDto>.Success(200));
            return ActionResultInstance(CustomResponseDto<NoDataDto>.Fail(404,"Mail Gönderilemedi!"));
        }
    }
}

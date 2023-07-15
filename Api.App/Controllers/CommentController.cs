using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orchestration.Layer.Mapping;
using Types.Layer.Contracts;
using Types.Layer.Contracts.Dtos;
using Types.Layer.Contracts.Interfaces;
using Types.Layer.Dtos;

namespace Api.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class CommentController : CustomBaseController
    {
        private readonly ICommentOrchestration _commentOrchestration;
        private readonly IMailOrchestration _mailOrchestration;
        private readonly ISharedOrchestration _sharedOrchestration;
        private readonly IUserOrchestration _userOrchestration;
        public CommentController(ICommentOrchestration commentOrchestration, IMailOrchestration mailOrchestration, ISharedOrchestration sharedOrchestration, IUserOrchestration userOrchestration)
        {
            _commentOrchestration = commentOrchestration;
            _mailOrchestration = mailOrchestration;
            _sharedOrchestration = sharedOrchestration;
            _userOrchestration = userOrchestration;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentDto">Eklenecek ve listelenecek olan yorumlar için kullanılmaktadır.</param>
        /// <returns>İşlem sonucunda eklenen yorum geri dönülmektedir.</returns>
        [HttpPost]
        public async Task<IActionResult> Add(CommentDto commentDto)
        {
            var comment = new CommentContract { Comment=commentDto.Comment, TopCommentId=Convert.ToInt32(commentDto.TopCommentId), UserId=commentDto.UserId, SharedId=commentDto.SharedId};
            var result = await _commentOrchestration.AddAsync(comment);
            if (result.StatusCode == 200)
            {
                var shared =await _sharedOrchestration.GetAsync(commentDto.SharedId);
                var sharedUser = await _userOrchestration.GetUserMail(shared.Data.UserId);
                if (sharedUser.Data != commentDto.UserId)
                {
                    var user = await _userOrchestration.GetUserMail(commentDto.UserId);
                    MailDto mailDto = new MailDto { Subject = "<h6>Bir Yeni Yorum", Body = shared.Data.Title + " başlıklı gönderinize " + user.Data + " kullanıcı adlı bir kullanıcı şu yorumu yaptı;</h6><br/><strong>" + comment.Comment + "</strong>", Contact = sharedUser.Data };
                    _mailOrchestration.SendMail(mailDto);
                }
                return ActionResultInstance(CustomResponseDto<CommentDto>.Success(200, new CommentDto { Id = result.Data.Id, Comment=result.Data.Comment, CreatedDate=result.Data.CreatedDate, TopCommentId=result.Data.TopCommentId, Username=sharedUser.Data }));
            }
            return ActionResultInstance(CustomResponseDto<CommentDto>.Fail(500,"Eklenemedi"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">Yorumun Id sini temsil eder.</param>
        /// <param name="UserId">Yorumu yapan kullanıcının Idsini temsil eder.</param>
        /// <returns>Öncelikle bu kullanıcının böyle bir yorumu varmı control edilir.Daha sonra var ise işlem yapılır ve geriye boş class döndürülür.</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int Id, string UserId)
        {
            var controlResult = await _commentOrchestration.AnyAsync(x => x.Id == Id && x.UserId == UserId);
            if (controlResult.Data)
            {
                var result = await _commentOrchestration.Delete(Id);
                return ActionResultInstance(result);
            }
            return ActionResultInstance(CustomResponseDto<NoDataDto>.Fail(404, "Bu kullanıcının böyle bir yorumu bulunamadı!"));
        }
    }
}

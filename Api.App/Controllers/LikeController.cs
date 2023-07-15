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
    public class LikeController : CustomBaseController
    {
        private readonly ISharedLikeOrchestration _sharedLikeOrchestration;

        public LikeController(ISharedLikeOrchestration sharedLikeOrchestration)
        {
            _sharedLikeOrchestration = sharedLikeOrchestration;
        }

        [HttpPost]
        public async Task<IActionResult> NewLike(SharedLikeDto sharedLikeDto)
        {
            var result=await _sharedLikeOrchestration.AddAsync(ObjectMapper.Mapper.Map<SharedLikeContract>(sharedLikeDto));
            return ActionResultInstance(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteLike(int SharedId,string UserId)
        {
            var result =  _sharedLikeOrchestration.Where(x => x.UserId == UserId && x.SharedId == SharedId).Data.FirstOrDefault();
            if (result != null)
            {
                var resultDelete = await _sharedLikeOrchestration.Delete(result.Id);
                return ActionResultInstance(resultDelete);
            }
            return ActionResultInstance(CustomResponseDto<NoDataDto>.Fail(404, "Beğeni bulunamadı!"));
        }
    }
}

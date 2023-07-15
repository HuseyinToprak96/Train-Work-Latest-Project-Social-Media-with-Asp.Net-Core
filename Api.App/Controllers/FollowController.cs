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
    public class FollowController : CustomBaseController
    {
        private readonly IFollowOrchestration _followOrchestration;

        public FollowController(IFollowOrchestration followOrchestration)
        {
            _followOrchestration = followOrchestration;
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(string followId,string followingId)
        {
            var result = _followOrchestration.Where(x => x.FollowId == followId && x.FollowingId == followingId).Data.FirstOrDefault()?.Id;
            if (result != null)
               return ActionResultInstance(await _followOrchestration.Delete(Convert.ToInt32(result)));
            return Ok(CustomResponseDto<NoDataDto>.Fail(404,"Bulunamadı!"));
        }
        [HttpPost]
        public async Task<IActionResult> Add(FollowDto followDto)
        {
            var result = await _followOrchestration.AddAsync(ObjectMapper.Mapper.Map<FollowContract>(followDto));
           return ActionResultInstance(CustomResponseDto<FollowDto>.Success(200,ObjectMapper.Mapper.Map<FollowDto>(result.Data)));
        }
    }
}

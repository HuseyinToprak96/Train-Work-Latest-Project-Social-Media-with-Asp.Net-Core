using AutoMapper;
using AutoMapper.Internal.Mappers;
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
    public class SharedController : CustomBaseController
    {
        private readonly ISharedOrchestration _sharedOrchestration;
        private readonly IUserOrchestration _userOrchestration;
        public SharedController(ISharedOrchestration sharedOrchestration, IUserOrchestration userOrchestration)
        {
            _sharedOrchestration = sharedOrchestration;
            _userOrchestration = userOrchestration;
        }
        [HttpGet]
        public async Task<IActionResult> ListDetail(string userId)
        {
            var result = await _sharedOrchestration.ListDetail(userId);
            var data = ObjectMapper.Mapper.Map<IEnumerable<SharedDto>>(result.Data);
            return ActionResultInstance(CustomResponseDto<IEnumerable<SharedDto>>.Success(200, data)); ;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sharedOrchestration.GetAllAsync();
            var data= ObjectMapper.Mapper.Map<IEnumerable<SharedDto>>(result.Data);
            return ActionResultInstance(CustomResponseDto<IEnumerable<SharedDto>>.Success(200,data)); ;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            return ActionResultInstance(await _sharedOrchestration.GetAsync(id));
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(SharedDto sharedDto)
        {
            var onlineUser = await _userOrchestration.GetUserByNameAsync(HttpContext.User.Identity.Name);
            sharedDto.CreatedUserId = onlineUser.Data.Id;
            sharedDto.UserId = onlineUser.Data.Id;
            var result= await _sharedOrchestration.AddAsync(ObjectMapper.Mapper.Map<SharedContract>(sharedDto));
            return ActionResultInstance(CustomResponseDto<SharedDto>.Success(200,ObjectMapper.Mapper.Map<SharedDto>(result.Data)));
        }
        [HttpPost]
        public async Task<IActionResult> RepeatShared(SharedDto sharedDto)
        {
            var shared = await _sharedOrchestration.GetAsync(sharedDto.Id);
            SharedContract sharedContract=new SharedContract { Description = shared.Data.Description , Path=shared.Data.Path, Title=shared.Data.Title, CreatedUserId=shared.Data.CreatedUserId, Type=shared.Data.Type, UserId=sharedDto.UserId};
            return ActionResultInstance(await _sharedOrchestration.AddAsync(sharedContract));
        }
        [HttpPut]
        public async Task<IActionResult> Update(SharedDto sharedDto)
        {
            var result = await _sharedOrchestration.Update(ObjectMapper.Mapper.Map<SharedContract>(sharedDto));
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result=await _sharedOrchestration.Delete(id);
            return Ok(result);
        }
    }
}

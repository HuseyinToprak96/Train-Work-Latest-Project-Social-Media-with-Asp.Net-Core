using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orchestration.Layer.Mapping;
using System.Xml.Linq;
using Types.Layer.Contracts;
using Types.Layer.Contracts.Dtos;
using Types.Layer.Contracts.Interfaces;
using Types.Layer.Dtos;

namespace Api.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserOrchestration _userOrchestration;
        private readonly ISharedOrchestration _sharedOrchestration;
        private readonly IFollowOrchestration _followOrchestration;
        private readonly ISharedLikeOrchestration _sharedLikeOrchestration;
        private readonly ICommentOrchestration _commentOrchestration;
        private readonly UserManager<AppUserContract> _userManager;
        public UserController(IUserOrchestration userOrchestration, ISharedOrchestration sharedOrchestration, IFollowOrchestration followOrchestration, UserManager<AppUserContract> userManager, ISharedLikeOrchestration sharedLikeOrchestration, ICommentOrchestration commentOrchestration)
        {
            _userOrchestration = userOrchestration;
            _sharedOrchestration = sharedOrchestration;
            _followOrchestration = followOrchestration;
            _userManager = userManager;
            _sharedLikeOrchestration = sharedLikeOrchestration;
            _commentOrchestration = commentOrchestration;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            if (String.IsNullOrEmpty(createUserDto.Username))
            {
                createUserDto.Username=createUserDto.Email.Split('@')[0];
            }
            return ActionResultInstance(await _userOrchestration.CreateUserAsync(createUserDto));
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return ActionResultInstance(await _userOrchestration.GetUserByNameAsync(HttpContext.User.Identity.Name));
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var result = await _userOrchestration.GetUserByNameAsync2(HttpContext.User.Identity.Name);
            ProfileDto profileDto = new ProfileDto { User = result.Data };
            var shareds= _sharedOrchestration.Where(x => x.UserId == result.Data.Id);
            var sharedLikes = await _sharedLikeOrchestration.GetAllAsync();
            var userList = await _userManager.Users.ToListAsync();
            var CommentsResult = await _commentOrchestration.GetAllAsync();
            var Comments = CommentsResult.Data;
            var Users = _userManager.Users.ToList();
            profileDto.SharedDtos = (from s in shareds.Data
                                     select new SharedDto
                                     {
                                         Title = s.Title,
                                         Description = s.Description,
                                         Path = s.Path,
                                         Type = s.Type,
                                         CreatedUserId = s.CreatedUserId,
                                         UserId = s.UserId,
                                         Id = s.Id,
                                         Comments=ObjectMapper.Mapper.Map<List<CommentDto>>(s.Comments.ToList()),
                                          LikeUsers=s.Likes.Select(x=>x.User.UserName).ToList(),
                                     }).ToList();
            var followers=_followOrchestration.Where(x=>x.FollowingId == result.Data.Id);
            var followings = _followOrchestration.Where(x => x.FollowId == result.Data.Id);
            var users =await _userManager.Users.ToListAsync();
            profileDto.Followers = (from f in followers.Data.ToList()
                                    join u in users
                                    on f.FollowId equals u.Id
                                    select new UserAppDto
                                    {
                                         Username=u.UserName,
                                          Id=u.Id
                                    }).ToList();
            profileDto.Followings = (from f in followings.Data.ToList()
                                    join u in users
                                    on f.FollowingId equals u.Id
                                    select new UserAppDto
                                    {
                                        Username = u.UserName,
                                        Id = u.Id
                                    }).ToList();
            return ActionResultInstance(CustomResponseDto<ProfileDto>.Success(200, profileDto));
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDto updateUserDto)
        {
            return ActionResultInstance(await _userOrchestration.Update(updateUserDto));
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var data = await _userOrchestration.GetUsers(HttpContext.User.Identity.Name);
            return ActionResultInstance(data);
        }
    }
}

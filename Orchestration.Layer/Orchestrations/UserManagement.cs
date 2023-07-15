using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orchestration.Layer.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts;
using Types.Layer.Contracts.Dtos;
using Types.Layer.Contracts.Interfaces;
using Types.Layer.Dtos;

namespace Orchestration.Layer.Orchestrations
{
    public class UserManagement:IUserOrchestration
    {
        public UserManager<AppUserContract> _userManager;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IFollowRepository _followRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ISharedLikeRepository _sharedLikeRepository;
        private readonly ISharedRepository _sharedRepository;

        public UserManagement(UserManager<AppUserContract> userManager, IUnitOfWork unitOfWork, IFollowRepository followRepository, ICommentRepository commentRepository, ISharedRepository sharedRepository, ISharedLikeRepository sharedLikeRepository)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _followRepository = followRepository;
            _commentRepository = commentRepository;
            _sharedRepository = sharedRepository;
            _sharedLikeRepository = sharedLikeRepository;
        }

        public async Task<CustomResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
        var user= new AppUserContract { Email = createUserDto.Email , UserName=createUserDto.Username};
            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<UserAppDto>.Fail(404,errors);
            }
            return CustomResponseDto<UserAppDto>.Success(200,ObjectMapper.Mapper.Map<UserAppDto>(user));
        }

        public async Task<CustomResponseDto<UserAppDto>> GetUserByNameAsync(string username)
        {
        var user=await _userManager.FindByNameAsync(username);
            if (user==null)
            {
                return CustomResponseDto<UserAppDto>.Fail(404, "Username Not Found!");
            }

            return CustomResponseDto<UserAppDto>.Success(200, ObjectMapper.Mapper.Map<UserAppDto>(user));
        }
        public async Task<CustomResponseDto<UpdateUserDto>> GetUserByNameAsync2(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return CustomResponseDto<UpdateUserDto>.Fail(404, "Username Not Found!");
            }

            return CustomResponseDto<UpdateUserDto>.Success(200, ObjectMapper.Mapper.Map<UpdateUserDto>(user));
        }

        public async Task<CustomResponseDto<List<UserAppDto>>> GetUsersNotShow()
        {
            var users = await _userManager.Users.Where(x => !x.IsShow).ToListAsync();
            if (users==null)
            {
                return CustomResponseDto<List<UserAppDto>>.Fail(200, "Yeni Kullanıcı Eklenmemiştir!");
            }
            return CustomResponseDto<List<UserAppDto>>.Success(200, ObjectMapper.Mapper.Map<List<UserAppDto>>(users));
        }

        public async Task<CustomResponseDto<NoDataDto>> Show()
        {
            var users = await _userManager.Users.Where(x => !x.IsShow).ToListAsync();
            foreach (var item in users)
            {
                item.IsShow = !item.IsShow;
                await _unitOfWork.CommitAsync();
            }
            return CustomResponseDto<NoDataDto>.Success(200);
        }

        public async Task<CustomResponseDto<NoDataDto>> Update(UpdateUserDto updateUserDto)
        {
            //var data = ObjectMapper.Mapper.Map<AppUserContract>(updateUserDto);
            //await _userManager.UpdateAsync(data);
            var result = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == updateUserDto.Id);
            result.Id = updateUserDto.Id;
            result.Email = updateUserDto.Email;
            result.Name = updateUserDto.Name;
            result.Surname = updateUserDto.Surname;
            result.PhoneNumber = updateUserDto.PhoneNumber; result.UserName = updateUserDto.UserName;
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoDataDto>.Success(200);
        }

        public async Task<CustomResponseDto<string>> GetUserMail(string id)
        {
            var user= await _userManager.Users.FirstOrDefaultAsync(x=>x.Id== id);
            return CustomResponseDto<string>.Success(200 ,user.Email);
        }

        public async Task<CustomResponseDto<List<UserAppDto>>> GetUsers(string username)
        {
            var user=await GetUserByNameAsync(username);
            var followingIds=_followRepository.Where(x => x.FollowId == user.Data.Id).Select(x => x.FollowingId).ToList();
            var users = await _userManager.Users.Where(x => x.UserName != username && !followingIds.Contains(x.Id)).ToListAsync();
            return CustomResponseDto<List<UserAppDto>>.Success(200,ObjectMapper.Mapper.Map<List<UserAppDto>>(users)); 
        }
        public async Task<CustomResponseDto<List<UserAppDto>>> GetUsers()
        {
            var result = ObjectMapper.Mapper.Map<List<UserAppDto>>(await _userManager.Users.ToListAsync());
            return CustomResponseDto<List<UserAppDto>>.Success(200,result);
        }
        public async Task<CustomResponseDto<ProfileDto>> GetUser(string id)
        {
            var shareds = _sharedRepository.Where(x => x.UserId == id).ToList();
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            var users = await _userManager.Users.ToListAsync();
            ProfileDto profileDto = new ProfileDto { User = ObjectMapper.Mapper.Map<UpdateUserDto>(user), SharedDtos=ObjectMapper.Mapper.Map<List<SharedDto>>( shareds)};
            var followers = _followRepository.Where(x => x.FollowingId == user.Id);
            var followings = _followRepository.Where(x => x.FollowId == user.Id);
            profileDto.Followers = (from f in followers.ToList()
                                    join u in users
                                    on f.FollowId equals u.Id
                                    select new UserAppDto
                                    {
                                        Username = u.UserName,
                                        Id = u.Id
                                    }).ToList();
            profileDto.Followings = (from f in followings.ToList()
                                     join u in users
                                     on f.FollowingId equals u.Id
                                     select new UserAppDto
                                     {
                                         Username = u.UserName,
                                         Id = u.Id
                                     }).ToList();
            return CustomResponseDto<ProfileDto>.Success(200,profileDto);
        }

    }
}

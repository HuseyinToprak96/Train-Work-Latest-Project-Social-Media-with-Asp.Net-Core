using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts.Dtos;
using Types.Layer.Dtos;

namespace Types.Layer.Contracts.Interfaces
{
    public interface IUserOrchestration
    {
        Task<CustomResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<CustomResponseDto<UserAppDto>> GetUserByNameAsync(string username);
        Task<CustomResponseDto<UpdateUserDto>> GetUserByNameAsync2(string username);
        Task<CustomResponseDto<List<UserAppDto>>> GetUsersNotShow();
        Task<CustomResponseDto<NoDataDto>> Show();
        Task<CustomResponseDto<NoDataDto>> Update(UpdateUserDto updateUserDto);
        Task<CustomResponseDto<string>> GetUserMail(string id);
        Task<CustomResponseDto<List<UserAppDto>>> GetUsers(string username);
        Task<CustomResponseDto<List<UserAppDto>>> GetUsers();
        Task<CustomResponseDto<ProfileDto>> GetUser(string id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts.Dtos;
using Types.Layer.Dtos;

namespace Types.Layer.Contracts.Interfaces
{
    public interface IAuthenticationService
    {
        Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto);

        Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken);

        Task<CustomResponseDto<NoDataDto>> RevokeRefreshToken(string refreshToken);//çıkış yapıldığında kullanılacak.

        CustomResponseDto<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto);

    }
}

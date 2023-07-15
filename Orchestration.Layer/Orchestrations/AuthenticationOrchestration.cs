using Business.Layer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Configuration;
using Types.Layer.Contracts;
using Types.Layer.Contracts.Dtos;
using Types.Layer.Contracts.Interfaces;
using Types.Layer.Dtos;

namespace Orchestration.Layer.Orchestrations
{
    public class AuthenticationOrchestration : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUserContract> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository;
        public AuthenticationOrchestration(IOptions<List<Client>> optionsClient, ITokenService tokenService, UserManager<AppUserContract> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenRepository)
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public async Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto==null)
            {
                throw new ArgumentNullException(nameof(loginDto)); 
            }
            var user=await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return CustomResponseDto<TokenDto>.Fail(404,"Email or Password is wrong");
            if(!await _userManager.CheckPasswordAsync(user,loginDto.Password))
            {
                return CustomResponseDto<TokenDto>.Fail(404, "Email or Password is wrong");
            }
            var token = _tokenService.CreateToken(user);
            var userRefreshToken = await _userRefreshTokenRepository.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();
            if (userRefreshToken == null) {
            await _userRefreshTokenRepository.AddAsync(new UserRefreshToken { UserId = user.Id, Code=token.RefreshToken, Expiration=token.RefreshTokenExpiration});
            }
            else
            {
                userRefreshToken.Code=token.RefreshToken;
                userRefreshToken.Expiration=token.RefreshTokenExpiration;
            }
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<TokenDto>.Success(200,token);
        }

        public CustomResponseDto<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);
            if (client==null)
            {
                return CustomResponseDto<ClientTokenDto>.Fail(404, "ClientId or Client Secret not Found!");
            }
            var token = _tokenService.CreateTokenByClient(client);
            return CustomResponseDto<ClientTokenDto>.Success(200,token);
        }

        public async Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
          var existRefreshToken=await _userRefreshTokenRepository.Where(x=>x.Code==refreshToken).FirstOrDefaultAsync();
            if (existRefreshToken == null)
                return CustomResponseDto<TokenDto>.Fail(404, "Refresh Token Not Found!");
            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);
            if (user==null)
            {
                return CustomResponseDto<TokenDto>.Fail(404, "User Id not found!");
            }
            var token=_tokenService.CreateToken(user);
            existRefreshToken.Code = token.RefreshToken;
            existRefreshToken.Expiration = token.RefreshTokenExpiration;
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<TokenDto>.Success(200, token);
        }

        public async Task<CustomResponseDto<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken =  _userRefreshTokenRepository.Where(x => x.Code==refreshToken).FirstOrDefault();
            if (existRefreshToken==null)
            {
                return CustomResponseDto<NoDataDto>.Fail(404, "Refresh Token Not Found!");   
            }
            _userRefreshTokenRepository.Delete(existRefreshToken);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoDataDto>.Success(200);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Configuration;
using Types.Layer.Contracts;
using Types.Layer.Contracts.Interfaces;
using Types.Layer.Dtos;

namespace Orchestration.Layer.Orchestrations
{
    public class TokenOrchestration : ITokenService
    {
        private readonly UserManager<AppUserContract> _userManager;
        private readonly CustomTokenOption _tokenOption;
        public TokenOrchestration(UserManager<AppUserContract> userManager, IOptions<CustomTokenOption> options)
        {
            _userManager = userManager;
            _tokenOption = options.Value;
        }

        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }

        private IEnumerable<Claim> GetClaim(AppUserContract appUser,List<string> audiences)
        {
var userList=new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier,appUser.Id),
    new Claim(JwtRegisteredClaimNames.Email,appUser.Email),
    new Claim(ClaimTypes.Name,appUser.UserName),
    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
};
            userList.AddRange(audiences.Select(x=>new Claim(JwtRegisteredClaimNames.Aud,x)));
            return userList;
        }

        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();
            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            new Claim(JwtRegisteredClaimNames.Sub,client.Id.ToString());
return claims;
        }

        public TokenDto CreateToken(AppUserContract appUser)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            var securityKey = SignOrchestration.GetSymmetricSecurityKey(_tokenOption.SecurityKey);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken=new JwtSecurityToken(issuer:_tokenOption.Issuer,expires:accessTokenExpiration,notBefore:DateTime.Now,claims:GetClaim(appUser,_tokenOption.Audience), signingCredentials:signingCredentials);
            var handle = new JwtSecurityTokenHandler();
            var token=handle.WriteToken(jwtSecurityToken);
            var tokenDto = new TokenDto { AccessToken = token, RefreshToken=CreateRefreshToken(), AccessTokenExpiration=accessTokenExpiration, RefreshTokenExpiration=refreshTokenExpiration  };
            return tokenDto;
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
           
            var securityKey = SignOrchestration.GetSymmetricSecurityKey(_tokenOption.SecurityKey);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: _tokenOption.Issuer, expires: accessTokenExpiration, notBefore: DateTime.Now, claims: GetClaimsByClient(client), signingCredentials: signingCredentials);
            var handle = new JwtSecurityTokenHandler();
            var token = handle.WriteToken(jwtSecurityToken);
            var clientToken = new ClientTokenDto { AccessToken = token, AccessTokenExpiration = accessTokenExpiration };
            return clientToken;
        }
    }
}

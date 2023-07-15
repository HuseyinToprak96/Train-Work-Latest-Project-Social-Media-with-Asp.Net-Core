using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Configuration;
using Types.Layer.Dtos;

namespace Types.Layer.Contracts.Interfaces
{
    public interface ITokenService
    {
        TokenDto CreateToken(AppUserContract appUser);
        ClientTokenDto CreateTokenByClient(Client client);
    }
}

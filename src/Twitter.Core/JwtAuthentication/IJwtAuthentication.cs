using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.DTOs.Account;

namespace Twitter.Core.JwtAuthentication
{
    public interface IJwtAuthentication
    {
        Task<string> AuthenticationByEmail(LoginUserByEmailDto loginUserByEmailDto);
    }
}

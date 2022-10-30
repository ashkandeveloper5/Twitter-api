using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.DTOs.Account;
using Twitter.Core.Interfaces;
using Twitter.Core.Security;
using Twitter.Domain.Interfaces;
using Twitter.Domain.Models.UserRoles;
using System.Security;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Twitter.Core.JwtAuthentication
{
    public class JwtAuthentication : IJwtAuthentication
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        public JwtAuthentication(IConfiguration configuration, IAccountService accountService, IRoleService roleService)
        {
            _configuration = configuration;
            _accountService = accountService;
            _roleService = roleService;
        }
        public async Task<string> AuthenticationByEmail(LoginUserByEmailDto loginUserByEmailDto)
        {
            User user = _accountService.GetUserByEmail(loginUserByEmailDto.Email);
            if (user != null && user.Email == loginUserByEmailDto.Email && user.Password == PasswordEncoder.EncodePasswordMd5(loginUserByEmailDto.Password))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TwitterapiTwitterapiTwitterapiTwitterapi"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
                var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1111111111111111"));
                var encryptionCredentials = new EncryptingCredentials(encryptionKey, SecurityAlgorithms.Aes128KW,SecurityAlgorithms.Aes128CbcHmacSha256);
                var roles = _roleService.GetRolesUser(user.UserId);
                #region Claims
                var authClaims = new List<System.Security.Claims.Claim>
                {
                     new System.Security.Claims.Claim(ClaimTypes.NameIdentifier,user.UserId),
                     new System.Security.Claims.Claim(ClaimTypes.Name,user.Email),
                };
                foreach (var userRole in roles)
                {
                    authClaims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, userRole.RoleName));
                }
                #endregion
                var descriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(authClaims),
                    Audience = "Twitter",
                    Issuer = "TwitterServer",
                    IssuedAt=DateTime.Now,
                    Expires=DateTime.Now.AddDays(7),
                    NotBefore=DateTime.Now,
                    SigningCredentials=signingCredentials,
                    EncryptingCredentials=encryptionCredentials,
                    CompressionAlgorithm=CompressionAlgorithms.Deflate,
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(descriptor);


                return tokenHandler.WriteToken(securityToken);
            }
            return null;
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.DTOs.Account;
using Twitter.Core.Interfaces;
using Twitter.Domain.Interfaces;
using Twitter.Domain.Models.UserRoles;
using Twitter.Core.Security;
using System.ComponentModel.DataAnnotations;

namespace Twitter.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public User GetUserByEmail(string userEmail)
        {
            return _accountRepository.GetUserByEmail(userEmail);
        }

        public bool LoginUserByEmail(LoginUserByEmailDto loginUserByEmailDto)
        {
            return _accountRepository.IsExistEmail(loginUserByEmailDto.Email) ? true : false;
        }

        public bool RegisterUser(RegisterUserByEmailDto registerUserByEmailDto)
        {
            User user;
            try
            {
                user = new User()
                {
                    Email = FixEmail.ExceptBlanks(registerUserByEmailDto.Email),
                    FirstName = registerUserByEmailDto.FirstName,
                    Password = PasswordEncoder.EncodePasswordMd5(registerUserByEmailDto.Password),
                    CreateDate = DateTime.Now,
                };
            }
            catch (Exception)
            {
                return false;
            }
            return _accountRepository.RegisterUser(user);
        }
    }
}

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

        public bool CheckMatchEmailAndPasswordForLogin(string userEmail, string userPassword)
        {
            return _accountRepository.GetUserByEmail(userEmail).Password == userPassword ? true : false;
        }

        public bool EditPasswordUser(EditPasswordUserDto editPasswordUserDto, string userEmail)
        {
            string OldPassword = PasswordEncoder.EncodePasswordMd5(editPasswordUserDto.OldPassword);
            string Password = PasswordEncoder.EncodePasswordMd5(editPasswordUserDto.NewPassword);
            return _accountRepository.MatchPasswordForChange(userEmail, OldPassword) ? _accountRepository.EditPasswordUser(userEmail, Password) : false;
        }

        public User GetUserByEmail(string userEmail)
        {
            return _accountRepository.GetUserByEmail(userEmail);
        }

        public bool LoginUserByEmail(LoginUserByEmailDto loginUserByEmailDto)
        {
            return _accountRepository.IsExistEmail(loginUserByEmailDto.Email) ? true : false;
        }

        public bool RegisterUserByEmail(RegisterUserByEmailDto registerUserByEmailDto)
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
            return _accountRepository.RegisterUserByEmail(user);
        }
    }
}

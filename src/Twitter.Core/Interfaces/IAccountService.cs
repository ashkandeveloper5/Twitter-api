using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.DTOs.Account;
using Twitter.Domain.Models.UserRoles;

namespace Twitter.Core.Interfaces
{
    public interface IAccountService
    {
        #region User
        bool LoginUserByEmail(LoginUserByEmailDto loginUserByEmailDto);
        Task<bool> AddProfile(string userEmail,string path);
        bool DeleteAccountUserByEmail(string userEmail);
        Task<bool> CheckFullImage(string userEmail);
        Task<List<string>> GetImagesPath(string userEmail);
        bool DeleteUserByEmail(string userEmail);
        bool RegisterUserByEmail(RegisterUserByEmailDto registerUserByEmailDto);
        User GetUserByEmail(string userEmail);
        bool EditPasswordUser(EditPasswordUserDto editPasswordUserDto,string userEmail);
        bool CheckMatchEmailAndPasswordForLogin(string userEmail,string userPassword);
        #endregion
    }
}

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
        bool RegisterUser(RegisterUserByEmailDto registerUserByEmailDto);
        User GetUserByEmail(string userEmail);
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Domain.Models.UserRoles;

namespace Twitter.Domain.Interfaces
{
    public interface IAccountRepository
    {
        #region User
        User GetUserById(string userId);
        User GetUserByEmail(string userEmail);
        User GetUserByPhoneNumber(string userPhoneNumber);
        bool IsExistUser(User user);
        bool IsExistId(string userId);
        bool IsExistEmail(string userEmail);
        bool IsExistPhoneNumber(string userPhoneNumber);
        bool RegisterUser(User user);
        bool EditUser(User user);
        void SaveChanges();
        #endregion
    }
}

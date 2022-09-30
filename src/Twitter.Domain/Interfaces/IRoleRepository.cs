using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Domain.Models.UserRoles;

namespace Twitter.Domain.Interfaces
{
    public interface IRoleRepository
    {
        #region Role
        bool AddRole(Role role);
        bool AddPermission(Permission permission, string roleId);
        bool EditRole(Role role);
        bool DeleteRole(Role role);
        IList<Role> GetRolesUser(string userId);
        IList<Role> GetAllRoles();
        bool AddRoleToUser(User user, Role role);
        #endregion
        #region SaveChanges
        void SaveChanges();
        #endregion
    }
}

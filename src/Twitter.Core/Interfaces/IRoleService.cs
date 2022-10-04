using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.DTOs.Role;
using Twitter.Domain.Models.UserRoles;

namespace Twitter.Core.Interfaces
{
    public interface IRoleService
    {
        #region Role
        IList<Role> GetRolesUser(string userId);
        bool AddRole(AddRoleDto addRoleDto);
        bool EditRole(EditRoleDto editRoleDto);
        bool DeleteRole(string nameRole);
        #endregion
    }
}

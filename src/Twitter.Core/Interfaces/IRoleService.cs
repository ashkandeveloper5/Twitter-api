using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Domain.Models.UserRoles;

namespace Twitter.Core.Interfaces
{
    public interface IRoleService
    {
        #region Role
        IList<Role> GetRolesUser(string userId);
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.Interfaces;
using Twitter.Domain.Interfaces;
using Twitter.Domain.Models.UserRoles;

namespace Twitter.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository= roleRepository;
        }
        public IList<Role> GetRolesUser(string userId)
        {
            return _roleRepository.GetRolesUser(userId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.DTOs.Role;
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
            _roleRepository = roleRepository;
        }

        public bool AddRole(AddRoleDto addRoleDto)
        {
            return _roleRepository.AddRole(new Role { RoleName = addRoleDto.Name, RoleDescription = addRoleDto.Description });
        }

        public bool DeleteRole(string nameRole)
        {
            return _roleRepository.DeleteRole(_roleRepository.GetRoleByName(nameRole));
        }

        public bool EditRole(EditRoleDto editRoleDto)
        {
            return _roleRepository.EditRole(new Role { RoleName = editRoleDto.Name, RoleDescription = editRoleDto.Description });
        }

        public IList<Role> GetRolesUser(string userId)
        {
            return _roleRepository.GetRolesUser(userId);
        }
    }
}

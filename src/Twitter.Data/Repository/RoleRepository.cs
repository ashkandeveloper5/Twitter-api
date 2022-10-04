using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Context;
using Twitter.Domain.Interfaces;
using Twitter.Domain.Models.UserRoles;
using Microsoft.EntityFrameworkCore;
using Twitter.Core.Utilities;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Twitter.Data.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TwitterContext _context;
        public RoleRepository(TwitterContext context)
        {
            _context = context;
        }

        public bool AddPermission(Permission permission)
        {
            try
            {
                _context.Permissions.Add(permission);
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddPermissionToRole(List<string> permissionsId, string roleId)
        {
            _context.RolePermissions.Where(r => r.RoleId == roleId).ToList().ForEach(p => { _context.Remove(p); });
            Role role = _context.Roles.SingleOrDefault(r => r.RoleId == roleId);
            try
            {
                foreach (var item in permissionsId)
                {
                    _context.RolePermissions.Add(new RolePermission { PermissionId = item, RoleId = roleId, RP_Id = UniqueCode.generateID() });
                }
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddRole(Role role)
        {
            try
            {
                _context.Roles.Add(role);
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddRoleToUser(User user, Role role)
        {
            try
            {
                var getUser = _context.Users.Find(user);
                var getRole = _context.Roles.Find(role);
                _context.UserRoles.Add(new UserRole()
                {
                    UR_Id = UniqueCode.generateID(),
                    UserId = getUser.UserId,
                    RoleId = getRole.RoleId,
                });
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePermission(Permission permission)
        {
            var findPermission=_context.Permissions.SingleOrDefault(p=>p.PermissionId==permission.PermissionId);
            if (findPermission == null) return false;
            findPermission.IsDelete = true;
            SaveChanges();
            return true;
        }

        public bool DeleteRole(Role role)
        {
            try
            {
                _context.Roles.Find(role).IsDelete = true;
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditRole(Role role)
        {
            try
            {
                _context.Roles.Update(role);
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IList<Role> GetAllRoles()
        {
            return _context.Roles.Include(r => r.RolePermissions).ThenInclude(r => r.Permission).ToList();
        }

        public Role GetRoleByName(string nameRole)
        {
            return _context.Roles.SingleOrDefault(r=>r.RoleName.Contains(nameRole));
        }

        public IList<Role> GetRolesUser(string userId)
        {
            IList<Role> roles = new List<Role>();
            var roleUser = _context.UserRoles.Where(r => r.UserId == userId).ToList();
            foreach (var item in roleUser)
            {
                roles.Add(GetAllRoles().SingleOrDefault(r => r.RoleId == item.RoleId));
            }
            return roles;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

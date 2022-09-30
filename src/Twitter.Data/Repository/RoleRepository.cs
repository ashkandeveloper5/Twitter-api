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

namespace Twitter.Data.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TwitterContext _context;
        public RoleRepository(TwitterContext context)
        {
            _context = context;
        }

        public bool AddPermission(Permission permission, string roleId)
        {
            throw new NotImplementedException();
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
                var getRole=_context.Roles.Find(role);
                _context.UserRoles.Add(new UserRole()
                {
                    UR_Id = UniqueCode.generateID(),
                    UserId=getUser.UserId,
                    RoleId=getRole.RoleId,
                });
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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

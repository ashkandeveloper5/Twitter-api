using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Domain.Models.UserRoles
{
    public class Role
    {
        [Key]
        public string RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string PermissionId { get; set; }
        public bool IsDelete { get; set; }

        #region Relationship
        public IList<UserRole> UserRoles { get; set; }
        public IList<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Domain.Models.UserRoles
{
    public class Permission
    {
        [Key]
        public string PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string RoleId { get; set; }
        public bool IsDelete { get; set; }
        public string? ParentId { get; set; }

        #region Relationship
        [ForeignKey(nameof(ParentId))]
        public IList<Permission> Permissions { get; set; }
        public IList<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}

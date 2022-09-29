using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Domain.Models.UserRoles
{
    public class RolePermission
    {
        [Key]
        public string RP_Id { get; set; }
        public string RoleId { get; set; }
        public string PermissionId { get; set; }
        #region Relationship
        public Permission Permission { get; set; }
        public Role Role { get; set; }
        #endregion
    }
}

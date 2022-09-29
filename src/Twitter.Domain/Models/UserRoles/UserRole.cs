using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Domain.Models.UserRoles
{
    public class UserRole
    {
        [Key]
        public string UR_Id { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }

        #region Relationship
        public User Users { get; set; }
        public Role Roles { get; set; }
        #endregion
    }
}

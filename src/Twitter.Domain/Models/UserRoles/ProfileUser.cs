using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Domain.Models.UserRoles
{
    public class ProfileUser
    {
        [Key]
        public string ProfileId { get; set; }
        public string UserId { get; set; }
        public string Path { get; set; }

        #region Relationship
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        #endregion
    }
}

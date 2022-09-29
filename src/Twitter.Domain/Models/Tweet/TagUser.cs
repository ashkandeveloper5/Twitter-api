using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Domain.Models.UserRoles;

namespace Twitter.Domain.Models.Tweet
{
    public class TagUser
    {
        [Key]
        public string TU_Id { get; set; }
        public string TagId { get; set; }
        public string UserId { get; set; }

        #region Relationship
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [ForeignKey(nameof(TagId))]
        public Tag Tag { get; set; }
        #endregion
    }
}

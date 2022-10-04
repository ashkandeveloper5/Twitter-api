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
    public class Tag
    {
        [Key]
        public string TU_Id { get; set; }
        public string TweetId { get; set; }
        public string UserId { get; set; }
        public bool IsDelete { get; set; }

        #region Relationship
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [ForeignKey(nameof(TweetId))]
        public Tweet Tweet { get; set; }
        #endregion
    }
}

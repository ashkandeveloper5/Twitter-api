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
    public class Tweet
    {
        [Key]
        public string TweetId { get; set; }
        public string TweetTitle { get; set; }
        public string TweetText { get; set; }
        public string UserId { get; set; }
        public bool IsDelete { get; set; }
        public int Likes { get; set; } = 0;
        public int View { get; set; } = 0;

        #region Relationship
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public IList<TweetHashtag> TweetHashtags { get; set; }
        public IList<Tag> Tags { get; set; }
        #endregion
    }
}

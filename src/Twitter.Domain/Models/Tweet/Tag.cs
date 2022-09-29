using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Domain.Models.Tweet
{
    public class Tag
    {
        [Key]
        public string TagUserId { get; set; }
        public string TweetId { get; set; }
        public string UserId { get; set; }

        #region Relationship
        public IList<TagTweet> TagTweets { get; set; }
        public IList<TagUser> TagUsers { get; set; }
        #endregion
    }
}

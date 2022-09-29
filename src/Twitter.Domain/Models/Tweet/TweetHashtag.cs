using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Domain.Models.Tweet
{
    public class TweetHashtag
    {
        [Key]
        public string TH_Id { get; set; }
        public string TweetId { get; set; }
        public string HashtagId { get; set; }

        #region Relationship
        public Tweet Tweet { get; set; }
        public Hashtag Hashtag { get; set; }
        #endregion
    }
}

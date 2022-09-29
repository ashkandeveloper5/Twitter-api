using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Domain.Models.Tweet
{
    public class Hashtag
    {
        [Key]
        public string HashtagId { get; set; }
        public string Text { get; set; }

        #region Relationship
        public IList<TweetHashtag> TweetHashtags { get; set; }
        #endregion
    }
}

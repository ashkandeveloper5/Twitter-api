using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Domain.Models.Tweet
{
    public class TagTweet
    {
        [Key]
        public string TT_Id { get; set; }
        public string TagId { get; set; }
        public string TweetId { get; set; }

        #region Relationship
        [ForeignKey(nameof(TagId))]
        public Tag Tag { get; set; }
        [ForeignKey(nameof(TweetId))]
        public Tweet Tweet { get; set; }
        #endregion
    }
}

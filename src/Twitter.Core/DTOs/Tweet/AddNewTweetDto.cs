using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Core.DTOs.Tweet
{
    public class AddNewTweetDto
    {
        [Required]
        public string TweetTitle { get; set; }
        [Required]
        public string TweetText { get; set; }
        public string UserEmail { get; set; }
        public string? UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Core.DTOs.Tweet
{
    public class GetTweetsDto
    {
        public string TweetTitle { get; set; }
        public string TweetText { get; set; }
    }
}

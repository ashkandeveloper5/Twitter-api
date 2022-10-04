using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.DTOs.Tweet;
using Twitter.Domain.Models.Tweet;

namespace Twitter.Core.Interfaces
{
    public interface ITweetService
    {
        #region Tweet
        IList<GetTweetsDto> GetTweetsByHashtag(string hashtagName);
        IList<GetTweetsDto> GetAllTweets();
        IList<GetTweetsDto> GetAllTagUser(string userEmail);
        IList<GetTweetsDto> GetTweetsUser(string userEmail);
        bool AddNewTweet(AddNewTweetDto addNewTweetDto);
        bool DeleteTweet(DeleteTweetDto deleteTweetDto);
        bool EditTweet(EditTweetDto editTweetDto);

        #endregion
        #region Hashtag
        IList<Hashtag> GetAllHashtag();
        IList<Hashtag> SearchHashtag(string nameHashtag);
        #endregion
        #region Utilities
        IList<string> GetSplitTag(string text);
        IList<string> GetSplitHashtag(string text);
        #endregion
    }
}

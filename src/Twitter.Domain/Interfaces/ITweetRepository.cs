﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Domain.Models.Tweet;

namespace Twitter.Domain.Interfaces
{
    public interface ITweetRepository
    {
        #region SaveChanges
        void SaveChanges();
        #endregion
        #region Tweet
        Tweet GetTweetById(string tweetId);
        Tuple<Tweet,bool> AddReturnNewTweet(Tweet tweet);
        IList<Tweet> GetAllTweets();
        IList<Tweet> GetTweetsUser(string userEmail);
        bool AddNewTweet(Tweet tweet);
        bool DeleteTweet(Tweet tweet);
        bool EditTweet(Tweet tweet);
        #endregion
        #region Tag
        bool AddTag(Tag tag);
        IList<string> IsExistEmailTags(IList<string> userEmails);
        bool IsExistEmailTag(string userEmail);
        #endregion
        #region Hashtag
        bool AddHashtag(Hashtag hashtag);
        Tuple<Hashtag,bool> AddReturnHashtag(Hashtag hashtag);
        bool IsExistHashtag(string hashtag);
        string GetHashtagId(string hashtagTitle);
        bool AddTweetToHashtagsExist(string tweetId,string hashtagId);
        #endregion
    }
}
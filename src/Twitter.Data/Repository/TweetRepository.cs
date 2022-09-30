﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.Utilities;
using Twitter.Data.Context;
using Twitter.Domain.Interfaces;
using Twitter.Domain.Models.Tweet;
using Twitter.Domain.Models.UserRoles;

namespace Twitter.Data.Repository
{
    public class TweetRepository : ITweetRepository
    {
        private readonly TwitterContext _context;
        public TweetRepository(TwitterContext context)
        {
            _context = context;
        }

        public bool AddHashtag(Hashtag hashtag)
        {
            try
            {
                hashtag.HashtagId = UniqueCode.generateID();
                _context.Hashtags.Add(hashtag);
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddNewTweet(Tweet tweet)
        {
            try
            {
                tweet.TweetId = UniqueCode.generateID();
                _context.Tweets.Add(tweet);
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Tuple<Hashtag, bool> AddReturnHashtag(Hashtag hashtag)
        {
            try
            {
                hashtag.HashtagId = UniqueCode.generateID();
                _context.Hashtags.Add(hashtag);
                SaveChanges();
                return Tuple.Create(hashtag,true);
            }
            catch (Exception)
            {
                return Tuple.Create(hashtag, false);
            }
        }

        public Tuple<Tweet, bool> AddReturnNewTweet(Tweet tweet)
        {
            try
            {
                tweet.TweetId = UniqueCode.generateID();
                _context.Tweets.Add(tweet);
                SaveChanges();
                return Tuple.Create(tweet,true);
            }
            catch (Exception)
            {
                return Tuple.Create(tweet,false);
            }
        }

        public bool AddTag(Tag tag)
        {
            try
            {
                tag.TU_Id= UniqueCode.generateID();
                _context.Tags.Add(tag);
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddTweetToHashtagsExist(string tweetId, string hashtagId)
        {
            try
            {
                TweetHashtag tweetHashtag = new TweetHashtag()
                {
                    TH_Id = UniqueCode.generateID(),
                    TweetId = tweetId,
                    HashtagId = hashtagId,
                };
                _context.TweetHashtags.Add(tweetHashtag);
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteTweet(Tweet tweet)
        {
            try
            {
                Tweet getTweet = _context.Tweets.SingleOrDefault(t => t.TweetId == tweet.TweetId);
                getTweet.IsDelete = true;
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditTweet(Tweet tweet)
        {
            try
            {
                _context.Tweets.Update(tweet);
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IList<Tweet> GetAllTweets()
        {
            return _context.Tweets.ToList();
        }

        public string GetHashtagId(string hashtagTitle)
        {
            return _context.Hashtags.SingleOrDefault(h => h.Text == hashtagTitle).HashtagId;
        }

        public Tweet GetTweetById(string tweetId)
        {
            return _context.Tweets.Include(u=>u.User).SingleOrDefault(t => t.TweetId == tweetId);
        }

        public IList<Tweet> GetTweetsUser(string userEmail)
        {
            User user = _context.Users.SingleOrDefault(u => u.Email == userEmail);
            return _context.Tweets.Where(t => t.UserId == user.UserId).ToList();
        }

        public bool IsExistEmailTag(string userEmail)
        {
            return _context.Users.SingleOrDefault(u => u.Email == userEmail) != null ? true : false;
        }

        public IList<string> IsExistEmailTags(IList<string> userEmails)
        {
            for (int i = 0; i < userEmails.Count; i++)
            {
                if (_context.Users.SingleOrDefault(u => u.Email == userEmails[i].ToString())==null)
                    userEmails.RemoveAt(i);
            }
            return userEmails;
        }

        public bool IsExistHashtag(string hashtag)
        {
            return _context.Hashtags.SingleOrDefault(h => h.Text == hashtag)!=null ? true : false;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
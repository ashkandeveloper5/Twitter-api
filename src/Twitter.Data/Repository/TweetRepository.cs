using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
//using static System.Net.Mime.MediaTypeNames;

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
                return Tuple.Create(hashtag, true);
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
                return Tuple.Create(tweet, true);
            }
            catch (Exception)
            {
                return Tuple.Create(tweet, false);
            }
        }

        public bool AddTag(Tag tag)
        {
            try
            {
                tag.TU_Id = UniqueCode.generateID();
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
                _context.Hashtags.SingleOrDefault(h => h.HashtagId == hashtagId).Count += 1;
                _context.Hashtags.SingleOrDefault(h => h.HashtagId == hashtagId).Views += 1;
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
                tweet.View += 1;
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IList<Hashtag> GetAllHashtag()
        {
            _context.Hashtags.ToList().OrderBy(o => o.Count).ToList().ForEach(h => h.Views += 1);
            SaveChanges();
            return _context.Hashtags.ToList().OrderBy(o => o.Count).ToList();
        }

        public IList<Tweet> GetAllTagUser(string userEmail)
        {
            List<Tweet> tweetsTag = new List<Tweet>();
            var findUser = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            var tag = _context.Tags.Where(u => u.UserId == findUser.UserId).ToList();
            foreach (var item in tag)
            {
                tweetsTag.Add(_context.Tweets.SingleOrDefault(t => t.TweetId == item.TweetId));
            }
            foreach (var item in tweetsTag)
            {
                _context.Tweets.SingleOrDefault(t => t.TweetId == item.TweetId).View += 1;
            }
            SaveChanges();
            if (tweetsTag == null) return null;
            return tweetsTag;
        }

        public IList<Tweet> GetAllTweets()
        {
            _context.Tweets.ToList().ForEach(t => t.View += 1);
            SaveChanges();
            return _context.Tweets.ToList();
        }

        public string GetHashtagId(string hashtagTitle)
        {
            return _context.Hashtags.SingleOrDefault(h => h.Text == hashtagTitle).HashtagId;
        }

        public Tweet GetTweetById(string tweetId)
        {
            return _context.Tweets.Include(u => u.User).SingleOrDefault(t => t.TweetId == tweetId);
        }

        public IList<Tweet> GetTweetsByHashtag(string hashtagName)
        {
            var result = new List<Tweet>();
            var tweetHashtags = _context.TweetHashtags.ToList();
            var Hashtags = _context.Hashtags.Where(h => h.Text.Contains(hashtagName)).ToList();
            var tweetIds = new List<string>();
            for (int i = 0; i < tweetHashtags.Count; i++)
            {
                for (int j = 0; j < Hashtags.Count; j++)
                {
                    if (tweetHashtags[i].HashtagId.ToString() == Hashtags[j].HashtagId.ToString())
                    {
                        tweetIds.Add(tweetHashtags[i].TweetId);
                    }
                }
            }
            var tweets = _context.Tweets.ToList();
            for (int i = 0; i < tweets.Count; i++)
            {
                for (int j = 0; j < tweetIds.Count; j++)
                {
                    if (tweets[i].TweetId.ToString() == tweetIds[j].ToString())
                    {
                        result.Add(_context.Tweets.SingleOrDefault(t => t.TweetId == tweetIds[j]));
                    }
                }
            }

            //////////////////////////
            //TODO
            //Add View To Tweets

            SaveChanges();
            return result;
        }

        public IList<Tweet> GetTweetsUser(string userEmail)
        {
            User user = _context.Users.SingleOrDefault(u => u.Email == userEmail);
            _context.Tweets.Where(t => t.UserId == user.UserId).ToList().ForEach(v => v.View += 1);
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
                if (_context.Users.SingleOrDefault(u => u.Email == userEmails[i].ToString()) == null)
                    userEmails.RemoveAt(i);
            }
            return userEmails;
        }

        public bool IsExistHashtag(string hashtag)
        {
            return _context.Hashtags.SingleOrDefault(h => h.Text == hashtag) != null ? true : false;
        }

        public bool LikeTweet(Tweet tweet)
        {
            tweet.Likes += 1;
            tweet.View += 1;
            SaveChanges();
            return true;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IList<Hashtag> SearchHashtag(string nameHashtag)
        {
            return _context.Hashtags.Where(h => h.Text.Contains(nameHashtag)).ToList().OrderBy(o => o.Count).ToList();
        }

        public IList<Tweet> ShowTheTopTweets(int count)
        {
            var result = new List<Tweet>();
            var tweets = _context.Tweets.OrderBy(t => t.View).ToList();
            if (count > tweets.Count) count = tweets.Count;
            for (int i = 0; i < count; i++)
            {
                result.Add(tweets[i]);
            }
            return result;
        }

        public IList<Hashtag> ShowTheTopHashtags(int count)
        {
            var result = new List<Hashtag>();
            var hashtags = _context.Hashtags.OrderBy(t => t.Count).ToList();
            if (count > hashtags.Count) count = hashtags.Count;
            for (int i = 0; i < count; i++)
            {
                result.Add(hashtags[i]);
                AddViewToHashtag(hashtags[i].HashtagId);
            }
            return result;
        }

        public bool AddViewToTweet(string tweetId)
        {
            _context.Tweets.SingleOrDefault(t => t.TweetId == tweetId).View += 1;
            SaveChanges();
            return true;
        }

        public bool AddViewToHashtag(string hashtagId)
        {
            _context.Hashtags.SingleOrDefault(t => t.HashtagId == hashtagId).Views += 1;
            SaveChanges();
            return true;
        }
    }
}

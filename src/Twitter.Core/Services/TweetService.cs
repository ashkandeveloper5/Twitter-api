using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.DTOs.Tweet;
using Twitter.Core.Interfaces;
using Twitter.Domain.Interfaces;
using Twitter.Domain.Models.Tweet;

namespace Twitter.Core.Services
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository _tweetRepository;
        private readonly IAccountService _accountService;
        public TweetService(ITweetRepository tweetRepository, IAccountService accountService)
        {
            _tweetRepository = tweetRepository;
            _accountService = accountService;
        }

        public bool AddNewTweet(AddNewTweetDto addNewTweetDto)
        {
            string text = addNewTweetDto.TweetText;
            var Tags = GetSplitTag(text);
            var Hashtags = GetSplitHashtag(text);
            Tweet tweet = new Tweet()
            {
                UserId = addNewTweetDto.UserId,
                TweetText = addNewTweetDto.TweetText,
                TweetTitle = addNewTweetDto.TweetTitle,
            };
            var result = _tweetRepository.AddReturnNewTweet(tweet);
            if (!result.Item2) return false;
            if (Tags.Count != 0)
            {
                for (int i = 0; i < Tags.Count; i++)
                {
                    if (!_tweetRepository.AddTag(new Tag()
                    {
                        TweetId = result.Item1.TweetId,
                        UserId = _accountService.GetUserByEmail(Tags[i]).UserId,
                    })) return false;
                }
            }
            if (Hashtags.Count != 0)
            {
                for (int j = 0; j < Hashtags.Count; j++)
                {
                    //Check Is Exist Hashtag
                    if (_tweetRepository.IsExistHashtag(Hashtags[j]))
                    {
                        //Add New tweet to Hashtag Available
                        if (!_tweetRepository.AddTweetToHashtagsExist(result.Item1.TweetId, _tweetRepository.GetHashtagId(Hashtags[j])))
                            return false;
                    }
                    else
                    {
                        //Create New Hashtag
                        var resultHashtag = _tweetRepository.AddReturnHashtag(new Hashtag { Text = Hashtags[j], });
                        //check HashTag Is Added
                        if (!resultHashtag.Item2) return false;
                        //Add New tweet to Hashtag Available After Create new Hashtag
                        if (!_tweetRepository.AddTweetToHashtagsExist(result.Item1.TweetId, resultHashtag.Item1.HashtagId))
                            return false;
                    }
                }
            }
            return true;
            //End
        }

        public bool AddViewToHashtag(string hashtagId)
        {
            return _tweetRepository.AddViewToHashtag(hashtagId);
        }

        public bool AddViewToTweet(string tweetId)
        {
            return _tweetRepository.AddViewToTweet(tweetId);
        }

        public bool DeleteTweet(DeleteTweetDto deleteTweetDto)
        {
            return false;
        }

        public bool EditTweet(EditTweetDto editTweetDto)
        {
            return false;
        }

        public IList<Hashtag> GetAllHashtag()
        {
            return _tweetRepository.GetAllHashtag();
        }

        public IList<GetTweetsDto> GetAllTagUser(string userEmail)
        {
            IList<GetTweetsDto> result = new List<GetTweetsDto>();
            var tweets = _tweetRepository.GetAllTagUser(userEmail);
            foreach (var item in tweets)
            {
                if (item == null) continue;
                result.Add(new GetTweetsDto { TweetText = item.TweetText, TweetTitle = item.TweetTitle });
            }
            return result;
        }

        public IList<GetTweetsDto> GetAllTweets()
        {
            List<GetTweetsDto> result = new List<GetTweetsDto>();
            foreach (var item in _tweetRepository.GetAllTweets())
            {
                result.Add(new GetTweetsDto { TweetText = item.TweetText, TweetTitle = item.TweetTitle, TweetId = item.TweetId });
            }
            return result;
        }

        public IList<string> GetSplitHashtag(string text)
        {
            List<string> split = new List<string>();
            split.AddRange(text.Split(' ').ToList());
            List<string> Hashtags = new List<string>();
            for (int i = 0; i < split.Count; i++)
            {
                string word = split[i].ToString();
                if (word == "") continue;
                if (word[0].ToString() == "#")
                {
                    string result = "";
                    for (int j = 1; j < word.Length; j++)
                    {
                        result += word[j];
                    }
                    Hashtags.Add(result);
                }
            }
            Hashtags.Distinct().ToList();
            return Hashtags;
        }

        public IList<string> GetSplitTag(string text)
        {
            List<string> split = new List<string>();
            split.AddRange(text.Split(' ').ToList());
            List<string> Tag = new List<string>();
            for (int i = 0; i < split.Count; i++)
            {
                string word = split[i].ToString();
                if (word == "") continue;
                if (word[0].ToString() == "@")
                {
                    string result = "";
                    for (int j = 1; j < word.Length; j++)
                    {
                        result += word[j];
                    }
                    Tag.Add(result);
                }
            }
            Tag.Distinct().ToList();
            return _tweetRepository.IsExistEmailTags(Tag).Distinct().ToList();
        }

        public IList<GetTweetsDto> GetTweetsByHashtag(string hashtagName)
        {
            var result = new List<GetTweetsDto>();
            var tweets = _tweetRepository.GetTweetsByHashtag(hashtagName).ToList();
            foreach (var item in tweets)
            {
                result.Add(new GetTweetsDto { Like = item.Likes, TweetText = item.TweetText, TweetTitle = item.TweetTitle, TweetId = item.TweetId });
            }
            return result;
        }

        public IList<GetTweetsDto> GetTweetsUser(string userEmail)
        {
            List<GetTweetsDto> result = new List<GetTweetsDto>();
            foreach (var item in _tweetRepository.GetTweetsUser(userEmail))
            {
                result.Add(new GetTweetsDto { TweetText = item.TweetText, TweetTitle = item.TweetTitle });
            }
            return result;
        }

        public bool LikeTweet(string tweetId)
        {
            return _tweetRepository.LikeTweet(_tweetRepository.GetTweetById(tweetId));
        }

        public IList<Hashtag> SearchHashtag(string nameHashtag)
        {
            return _tweetRepository.SearchHashtag(nameHashtag);
        }

        public IList<GetHashtagDto> ShowTheTopHashtags(int count)
        {
            var result = new List<GetHashtagDto>();
            var hashtags = _tweetRepository.ShowTheTopHashtags(count);
            foreach (var item in hashtags)
            {
                result.Add(new GetHashtagDto { Name = item.Text, Count = item.Count, Views = item.Views, HahtagId = item.HashtagId });
            }
            return result;
        }

        public IList<GetTweetsDto> ShowTheTopTweets(int count)
        {
            var result = new List<GetTweetsDto>();
            var tweets = _tweetRepository.ShowTheTopTweets(count);
            foreach (var item in tweets)
            {
                result.Add(new GetTweetsDto { View = item.View, Like = item.Likes, TweetText = item.TweetText, TweetTitle = item.TweetTitle, TweetId = item.TweetId });
            }
            return result;
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twitter.Domain.Models.Tweet;

namespace Twitter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Public")]
    public class TweetController : ControllerBase
    {
        private readonly ITweetService _tweetService;
        private readonly IAccountService _accountService;
        public TweetController(ITweetService tweetService, IAccountService accountService)
        {
            _tweetService = tweetService;
            _accountService = accountService;
        }
        #region Tweets
        [HttpPost("AddTweet")]
        public IActionResult AddNewTweet([FromBody] AddNewTweetDto addNewTweetDto)
        {
            addNewTweetDto.UserId = _accountService.GetUserByEmail(User.Identity.Name).UserId;
            if (!ModelState.IsValid) return BadRequest();
            if (!_tweetService.AddNewTweet(addNewTweetDto)) return BadRequest();
            return Ok();
        }
        [HttpGet("GetAllTweets")]
        public ActionResult<List<GetTweetsDto>> GetAllTweets()
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(_tweetService.GetAllTweets().ToList<GetTweetsDto>());
        }
        #endregion
        #region Hashtag
        [HttpGet("GetTweetByHashtag")]
        public ActionResult<List<GetTweetsDto>> GetTweetByHashtag(string hashtagName)
        {
            return Ok(_tweetService.GetTweetsByHashtag(hashtagName).ToList());
        }

        [HttpGet("GetAllHashtags")]
        public ActionResult GetAllHashtags()
        {
            return Ok(_tweetService.GetAllHashtag().Select(h => new { h.Text, h.Count, h.Views }));
        }

        [HttpGet("SearchHashtag")]
        public ActionResult SearchHashtag([FromBody] string nameHashtag)
        {
            return Ok(_tweetService.SearchHashtag(nameHashtag));
        }
        #endregion
        #region PopularContent
        [HttpGet("PopularTweets")]
        public ActionResult<List<GetTweetsDto>> PopularTweets(int countForShow)
        {
            return Ok(_tweetService.ShowTheTopTweets(countForShow).ToList());
        }

        [HttpGet("PopularHashtags")]
        public ActionResult<List<GetHashtagDto>> PopularHashtags(int countForShow)
        {
            return Ok(_tweetService.ShowTheTopHashtags(countForShow).ToList());
        }
        #endregion
        #region Like
        [HttpPost("LikeTweet")]
        public ActionResult LikeTweet([FromBody] string tweetId)
        {
            return _tweetService.LikeTweet(tweetId) ? Ok() : BadRequest();
        }
        #endregion
    }
}

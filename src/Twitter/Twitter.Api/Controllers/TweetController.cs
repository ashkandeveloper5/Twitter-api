

using Twitter.Domain.Models.Tweet;

namespace Twitter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private readonly ITweetService _tweetService;
        private readonly IAccountService _accountService;
        public TweetController(ITweetService tweetService, IAccountService accountService)
        {
            _tweetService = tweetService;
            _accountService = accountService;
        }
        #region Hashtag
        [HttpGet("GetTweetByHashtag")]
        public ActionResult<List<Tweet>> GetTweetByHashtag([FromQuery] string hashtagName)
        {
            return Ok(_tweetService.GetTweetsByHashtag(hashtagName).ToList());
        }

        [HttpGet("GetAllHashtags")]
        public ActionResult GetAllHashtags()
        {
            return Ok(_tweetService.GetAllHashtag().Select(h=>new {h.Text,h.Count,h.Views}));
        }

        [HttpGet("SearchHashtag")]
        public ActionResult SearchHashtag([FromQuery] string nameHashtag)
        {
            return Ok(_tweetService.SearchHashtag(nameHashtag));
        }
        #endregion
        #region Tweet
        [HttpPost("AddTweet")]
        public IActionResult AddNewTweet([FromQuery] AddNewTweetDto addNewTweetDto)
        {
            addNewTweetDto.UserId = _accountService.GetUserByEmail(addNewTweetDto.UserEmail).UserId;
            if (!ModelState.IsValid) return BadRequest(addNewTweetDto);
            if (!_tweetService.AddNewTweet(addNewTweetDto)) return BadRequest(addNewTweetDto);
            return NoContent();
        }
        [HttpGet("GetAllTweets")]
        public ActionResult<List<GetTweetsDto>> GetAllTweets()
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(_tweetService.GetAllTweets().ToList<GetTweetsDto>());
        }
        #endregion
    }
}

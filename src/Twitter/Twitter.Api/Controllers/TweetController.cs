using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Twitter.Core.DTOs.Tweet;
using Twitter.Core.Interfaces;

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

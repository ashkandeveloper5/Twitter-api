using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twitter.Core.DTOs.Tweet;
using Twitter.Core.Interfaces;

namespace Twitter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITweetService _tweetService;
        private readonly IAccountService _accountService;
        public UserController(ITweetService tweetService, IAccountService accountService)
        {
            _tweetService = tweetService;
            _accountService = accountService;
        }

        #region Tweet
        [HttpGet("ShowTweets")]
        public ActionResult<IList<GetTweetsDto>> ShowTweets([FromQuery] string userEmail)
        {
            return Ok(_tweetService.GetTweetsUser(userEmail).ToList());
        }
        [HttpGet("ShowTagsTweet")]
        public ActionResult<IList<GetTweetsDto>> ShowTagsTweet([FromQuery] string userEmail)
        {
            return Ok(_tweetService.GetAllTagUser(userEmail));
        }
        #endregion
        #region User
        [HttpDelete("DeleteUser")]
        public ActionResult DeleteUser(string userEmail)
        {
            var result = _accountService.DeleteUserByEmail(userEmail);
            return result == true ? NoContent() : Problem();
        }
        #endregion
    }
}

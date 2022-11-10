

using Microsoft.AspNetCore.Authorization;

namespace Twitter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Public")]
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
        public ActionResult<IList<GetTweetsDto>> ShowTweets()
        {
            return Ok(_tweetService.GetTweetsUser(User.Identity.Name).ToList());
        }
        [HttpGet("ShowTagsTweet")]
        public ActionResult<IList<GetTweetsDto>> ShowTagsTweet()
        {
            return Ok(_tweetService.GetAllTagUser(User.Identity.Name));
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

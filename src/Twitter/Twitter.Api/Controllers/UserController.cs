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
        public UserController(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        [HttpGet("ShowTweets")]
        public ActionResult<IList<GetTweetsDto>> ShowTweets()
        {
            return Ok(_tweetService.GetTweetsUser(User.Identity.Name).ToList());
        }
    }
}

using System.Net.Mime;
using Twitter.Core.Utilities;
using Twitter.Domain.Models.UserRoles;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authorization;

namespace Twitter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
        private readonly IAccountService _accountService;
        private readonly IJwtAuthentication _jwtAuthentication;
        public AccountController(IAccountService accountService, IJwtAuthentication jwtAuthentication, FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _accountService = accountService;
            _jwtAuthentication = jwtAuthentication;
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
        }
        #region DownloadImage
        [HttpGet("DownloadImage")]
        [Authorize(Roles ="Public")]
        public ActionResult DownloadImage([FromQuery] string path)
        {
            if (System.IO.File.Exists("/UserImage/" + path)) return NotFound();
            var file = System.IO.File.ReadAllBytes(System.IO.Path.Combine(Directory.GetCurrentDirectory()) + "/wwwroot/UserImage/" + path);
            if (!_fileExtensionContentTypeProvider.TryGetContentType(path, out var ContentType))
            {
                ContentType = "application/octet-stream";
            }
            return File(file, ContentType, Path.GetFileName(path));
        }
        #endregion
        #region ShowImagesPath
        [HttpGet("ShowImagesPath")]
        [Authorize(Roles ="Public")]
        public ActionResult ShowImagesPath([FromQuery] string userEmail)
        {
            return Ok(_accountService.GetImagesPath(userEmail).Result);
        }
        #endregion
        #region AddImage
        [HttpPost("AddProfile")]
        [Authorize(Roles ="Public")]
        public ActionResult AddProfile([FromQuery] AddProfileDto addProfileDto)
        {
            if (addProfileDto.Profile != null)
            {
                string imagePath = "";
                string path = UniqueCode.generateID() + System.IO.Path.GetExtension(addProfileDto.Profile.FileName);
                if (!_accountService.CheckFullImage(addProfileDto.Email).Result) return BadRequest();
                if (!_accountService.AddProfile(addProfileDto.Email, path).Result) return BadRequest();
                imagePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserImage", path);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    addProfileDto.Profile.CopyTo(stream);
                }
            }
            return NoContent();
        }
        #endregion
        #region Login
        [HttpPost("Login")]
        public IActionResult Login([FromQuery] LoginUserByEmailDto loginUserDto)
        {
            if (User.Identity.IsAuthenticated) HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!ModelState.IsValid) return BadRequest(loginUserDto);

            //Method LoginUserByEmail is for checking the availability of email.
            if (!_accountService.LoginUserByEmail(loginUserDto)) return BadRequest(loginUserDto);

            //Check is match password and email for login
            if (!_accountService.CheckMatchEmailAndPasswordForLogin(loginUserDto.Email, PasswordEncoder.EncodePasswordMd5(loginUserDto.Password))) return BadRequest(loginUserDto);

            #region Jwt
            var result = _jwtAuthentication.AuthenticationByEmail(loginUserDto).Result;
            #endregion
            return result != null ? Ok(result) : Unauthorized();
        }
        #endregion
        #region Register
        [HttpPost("Register")]
        public IActionResult Register([FromQuery] RegisterUserByEmailDto registerUserDto)
        {
            if (!ModelState.IsValid) return BadRequest(registerUserDto);
            if (!_accountService.RegisterUserByEmail(registerUserDto)) return BadRequest(registerUserDto);
            return NoContent();
        }
        #endregion
        #region Logout
        [HttpPost("Logout")]
        public IActionResult Login()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return NoContent();
        }
        #endregion
        #region EditPassword
        [HttpPost("EditPassword")]
        [Authorize(Roles ="Public")]
        public IActionResult EditPassword([FromQuery] EditPasswordUserDto editPasswordUserDto)
        {
            if (!User.Identity.IsAuthenticated) return BadRequest(editPasswordUserDto);
            if (!ModelState.IsValid) return BadRequest(editPasswordUserDto);
            if (!_accountService.EditPasswordUser(editPasswordUserDto, editPasswordUserDto.Email)) return BadRequest(editPasswordUserDto);
            return NoContent();
        }
        #endregion
        #region DeleteAccount
        [HttpDelete("DeleteAccount")]
        [Authorize(Roles ="Public")]
        public ActionResult DeleteAccount(string userEmail)
        {
            var result = _accountService.DeleteAccountUserByEmail(userEmail);
            return result ? NoContent() : Problem();
        }
        #endregion
    }
}

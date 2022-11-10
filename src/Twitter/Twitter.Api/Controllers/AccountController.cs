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
        public ActionResult DownloadImage([FromBody] string path)
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
        public ActionResult ShowImagesPath()
        {
            return Ok(_accountService.GetImagesPath(User.Identity.Name).Result);
        }
        #endregion
        #region UploadProfile
        [HttpPost("UploadProfile")]
        [Authorize(Roles ="Public")]
        public ActionResult UploadProfile([FromBody] AddProfileDto addProfileDto)
        {
            if (addProfileDto.Profile != null)
            {
                string imagePath = "";
                string path = UniqueCode.generateID() + System.IO.Path.GetExtension(addProfileDto.Profile.FileName);
                if (!_accountService.CheckFullImage(User.Identity.Name).Result) return BadRequest();
                if (!_accountService.AddProfile(User.Identity.Name, path).Result) return BadRequest();
                imagePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserImage", path);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    addProfileDto.Profile.CopyTo(stream);
                }
            }
            return Ok();
        }
        #endregion
        #region Login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginUserByEmailDto loginUserDto)
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
        public IActionResult Register([FromBody] RegisterUserByEmailDto registerUserDto)
        {
            if (!ModelState.IsValid) return BadRequest(registerUserDto);
            if (!_accountService.RegisterUserByEmail(registerUserDto)) return BadRequest(registerUserDto);
            return Ok();
        }
        #endregion
        #region Logout
        [HttpPost("Logout")]
        [Authorize]
        public IActionResult Login()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
        #endregion
        #region EditPassword
        [HttpPut("EditPassword")]
        [Authorize(Roles ="Public")]
        public IActionResult EditPassword([FromBody] EditPasswordUserDto editPasswordUserDto)
        {
            if (!User.Identity.IsAuthenticated) return BadRequest(editPasswordUserDto);
            if (!ModelState.IsValid) return BadRequest(editPasswordUserDto);
            if (!_accountService.EditPasswordUser(editPasswordUserDto, User.Identity.Name)) return BadRequest(editPasswordUserDto);
            return Ok();
        }
        #endregion
        #region DeleteAccount
        [HttpDelete("DeleteAccount")]
        [Authorize(Roles ="Public")]
        public ActionResult DeleteAccount()
        {
            var result = _accountService.DeleteAccountUserByEmail(User.Identity.Name);
            return result ? Ok() : Problem();
        }
        #endregion
    }
}

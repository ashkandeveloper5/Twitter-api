using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Twitter.Core.DTOs.Account;
using Twitter.Core.Interfaces;
using Twitter.Domain.Models.UserRoles;
using Twitter.Core.Security;
using Twitter.Core.JwtAuthentication;

namespace Twitter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJwtAuthentication _jwtAuthentication;
        public AccountController(IAccountService accountService,IJwtAuthentication jwtAuthentication)
        {
            _accountService = accountService;
            _jwtAuthentication = jwtAuthentication;
        }
        #region DeleteAccount
        [HttpDelete("DeleteAccount")]
        public ActionResult DeleteAccount(string userEmail)
        {
            var result =_accountService.DeleteAccountUserByEmail(userEmail);
            return result ? NoContent() : Problem();
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
        public IActionResult EditPassword([FromQuery] EditPasswordUserDto editPasswordUserDto)
        {
            if (!User.Identity.IsAuthenticated) return BadRequest(editPasswordUserDto);
            if (!ModelState.IsValid) return BadRequest(editPasswordUserDto);
            if (!_accountService.EditPasswordUser(editPasswordUserDto, User.Identity.Name)) return BadRequest(editPasswordUserDto);
            return NoContent();
        }
        #endregion
    }
}

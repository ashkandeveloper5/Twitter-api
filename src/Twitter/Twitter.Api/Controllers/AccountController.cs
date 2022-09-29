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

namespace Twitter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
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

            //Get user
            User user = _accountService.GetUserByEmail(loginUserDto.Email);

            #region Claims
            //Claims
            var claims = new List<System.Security.Claims.Claim>
                {
                new System.Security.Claims.Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new System.Security.Claims.Claim(ClaimTypes.Email,user.Email),
                new System.Security.Claims.Claim(ClaimTypes.Name,user.Email),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = true
            };
            HttpContext.SignInAsync(principal, properties);
            #endregion
            return NoContent();
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

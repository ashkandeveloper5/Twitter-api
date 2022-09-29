using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Twitter.Core.DTOs.Account;
using Twitter.Core.Interfaces;
using Twitter.Domain.Models.UserRoles;

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
        [HttpGet]
        public IActionResult Login(LoginUserByEmailDto loginUserDto)
        {
            if (ModelState.IsValid) return BadRequest(loginUserDto);
            //Method LoginUserByEmail is for checking the availability of email.
            if (!_accountService.LoginUserByEmail(loginUserDto)) return BadRequest(loginUserDto);

            #region Claims
            //Claims
            User user = _accountService.GetUserByEmail(loginUserDto.Email);
            var claims = new List<System.Security.Claims.Claim>
                {
                new System.Security.Claims.Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new System.Security.Claims.Claim(ClaimTypes.Email,user.Email),
                new System.Security.Claims.Claim(ClaimTypes.Name,user.FirstName),
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
        [HttpGet]
        public IActionResult Register(RegisterUserByEmailDto registerUserDto)
        {
            if(!ModelState.IsValid)return BadRequest(registerUserDto);
            if (!_accountService.RegisterUser(registerUserDto)) return BadRequest(registerUserDto);
            return NoContent();
        }
        #endregion
        #region Logout
        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return NoContent();
        }
        #endregion
    }
}

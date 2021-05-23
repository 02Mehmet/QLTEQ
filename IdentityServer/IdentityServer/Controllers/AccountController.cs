using IdentitySample.Models.AccountViewModels;
using IdentityServer.Data;
using JWTMicroNetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountController(
            ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var msg = "";
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var resultClaim = await _userManager.AddClaimAsync(user, new Claim("UserId", user.Id.ToString()));

                    if (resultClaim.Succeeded)
                    {
                        var resultT2 = await _userManager.AddClaimAsync(user, new Claim("UserName", user.UserName));
                        if (resultT2.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            _logger.LogInformation(3, "User created a new account with password.");
                            msg = "User created a new account with password.";
                        }
                    }
                }
                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return Ok(msg);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var msg = "";
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    msg = "User logged in.";
                    return Ok(msg);
                }
                if (result.RequiresTwoFactor)
                {

                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    msg = "User account locked out.";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    msg = "Invalid login attempt.";
                }
            }

            // If we got this far, something failed, redisplay form
            return Ok(msg);
        }

        [HttpPost("token")]
        [AllowAnonymous]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> GetToken([FromForm] LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                if (appUser != null)
                {
                    var token = await GenerateJwtTokenAsync(model.Email, appUser);
                    if (!string.IsNullOrEmpty(token))
                    {
                        return Ok(token);
                    }
                }
            }

            return Ok("INVALID_LOGIN_ATTEMPT");
        }
        private async Task<string> GenerateJwtTokenAsync(string email, ApplicationUser user)
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string claimString = _contextAccessor.HttpContext.Session.GetString("IdentityClaims");
            if (claimString != null)
            {
                List<ClaimModel> sessionClaims = JsonConvert.DeserializeObject<List<ClaimModel>>(claimString);
                identity.AddClaims(sessionClaims.Select(sc => new Claim(sc.ClaimType, sc.ClaimValue)));
            }

            // get options
            var jwtAppSettingOptions = _configuration.GetSection("JwtIssuerOptions");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAppSettingOptions["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var now = DateTime.Now;
            var expires = now.AddDays(Convert.ToDouble(jwtAppSettingOptions["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                jwtAppSettingOptions["JwtIssuer"],
                jwtAppSettingOptions["JwtIssuer"],
                identity.Claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

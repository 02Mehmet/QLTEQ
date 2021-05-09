using LibWebAPI.Business.Abstract;
using LibWebAPI.Business.Concrete;
using LibWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IRegisterService _registerService;
        private ITokenService _tokenService;

        public AccountController()
        {
            _registerService = new RegisterManager();
            _tokenService = new TokenManager();
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterVM register)
        {
            string result = _registerService.Register(register);

            return Ok(result);
        }

        [HttpPost("token")]
        [AllowAnonymous]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult GetToken([FromForm] TokenVM model)
        {
            string result = _tokenService.GetToken(model);

            return Ok(result);
        }
    }
}

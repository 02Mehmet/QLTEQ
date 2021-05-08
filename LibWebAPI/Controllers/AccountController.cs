using LibWebAPI.Models;
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
        public ActionResult Register([FromBody] RegisterVM register)
        {
            var client = new RestClient("https://localhost:5001/api/authentication/register");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"Email\": \"aysenurbugdayligil@gmail.com\",\r\n    \"Password\": \"Aysenur--1996\",\r\n    \"ConfirmPassword\": \"Aysenur--1996\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return null;
        }
    }
}

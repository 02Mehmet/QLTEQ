using LibWebAPI.Abstract;
using LibWebAPI.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibWebAPI.Concrete
{
    public class TokenRepository : ITokenRepository
    {
        public string GetToken(TokenVM tokenVM)
        {
            var client = new RestClient("https://localhost:44389/api/Authentication/token");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Cookie", ".AspNetCore.Identity.Application=CfDJ8D_xAUvhPIJOvC-tYFBgkeHG4ZC3RzQUST8CToT2z-RxwVhKtxy8M0ndZnqHvy2XJcswrL28GuVGcGhHXhHIXboNUTHQRI1Xu3oSEsIvxRIfWM2cCdAWT5djpYF4s2z902kfVP_mE-X3dzsEgsOhoz3c1tLF3j7gZy2vW1ACZCsWjhqUFSnvBbjnUXEYkP0DGtcjv3mysvxrucoA34RPL7bXFq_vY13TSAkz5567Igi_Ov1GyDBhUT-vua_vDb0sdWHWKGnGVhOSn-k2BW9bG5x0GCjZqd92GqAnAyoqc3E-mIPZ_CefrueL0IfdvEPTUw; .AspNetCore.Session=CfDJ8D%2FxAUvhPIJOvC%2BtYFBgkeFv9CTqyricnd7sf57kdwzALxQQ18eX9PDskhxAaWmh8PuYVMey18v3KP0eC8Ra%2FgInHq6cLo%2Bye4B5GJRBGkBaIjfttx707cUKnYeMO4OqKLoIUMv3fsK%2BTzyyOpmz3nWCnF7xHoPsbSzh8DMzGglf");
            request.AddParameter("email", tokenVM.Email);
            request.AddParameter("password", tokenVM.Password);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
    }
}

using JWTMicroNetCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JWTMicroNetCore.Services
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        private IHttpContextAccessor _contextAccessor;
        public ClaimsTransformer(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity identity = (ClaimsIdentity)principal.Identity;
            string claimString = _contextAccessor.HttpContext.Session.GetString("IdentityClaims");
            if (claimString != null)
            {
                List<ClaimModel> sessionClaims = JsonConvert.DeserializeObject<List<ClaimModel>>(claimString);
                identity.AddClaims(sessionClaims.Select(sc => new Claim(sc.ClaimType, sc.ClaimValue)));
            }

            return Task.FromResult(principal);
        }
    }    
}

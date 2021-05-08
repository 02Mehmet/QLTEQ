using JWTMicroNetCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JWTMicroNetCore.Services
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser>
    {
        private IHttpContextAccessor contextAccessor;

        public ApplicationSignInManager(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<ApplicationUser> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            this.contextAccessor = contextAccessor;
        }

        public override async Task<ClaimsPrincipal> CreateUserPrincipalAsync(ApplicationUser user)
        {
            var principal = await base.CreateUserPrincipalAsync(user);
            ClaimsIdentity identity = (ClaimsIdentity)principal.Identity;

            // storing claims in session and removing them. These claims will be added by Transformer
            List<ClaimModel> sessionClaims = new List<ClaimModel>();
            List<Claim> identityClaims = identity.Claims.ToList();
            foreach (var claim in identityClaims)
            {
                sessionClaims.Add(new ClaimModel() { ClaimType = claim.Type, ClaimValue = claim.Value });
                identity.RemoveClaim(claim);
            }

            this.contextAccessor.HttpContext.Session.SetString("IdentityClaims", JsonConvert.SerializeObject(sessionClaims));

            return principal;
        }
    }

}

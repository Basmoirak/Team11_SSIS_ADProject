using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Team11_SSIS_ADProject.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var result = await System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>()
                .PasswordSignInAsync(context.UserName, context.Password, false, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    break;
                case SignInStatus.LockedOut:
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                case SignInStatus.RequiresVerification:
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                case SignInStatus.Failure:
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                default:
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
        }
    }
}
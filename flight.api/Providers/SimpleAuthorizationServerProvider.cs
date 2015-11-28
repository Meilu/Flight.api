using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using flight.BLL.Services;
using flight.library.Attributes;
using Microsoft.Owin.Security.OAuth;

namespace flight.api.Providers
{
    [UnityIoCTransientLifetimed]
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider, ISimpleAuthorizationServerProvider
    {
        private readonly IApiUserService _userService;

        public SimpleAuthorizationServerProvider(IApiUserService userService)
        {
            _userService = userService;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            
            // try logging in the api user
            var loginResult = _userService.LoginUser(context.UserName, context.Password);


            if (loginResult != UserLoginResult.Success)
            {
                context.SetError("invalid_grant", "Cant login bla");
                return;
            }

            var apiUser = _userService.GetUser(context.UserName).FirstOrDefault();

            if (apiUser == null)
                return;

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("Username", context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, apiUser.Role.ToString()));

            context.Validated(identity);

        }
    }
}
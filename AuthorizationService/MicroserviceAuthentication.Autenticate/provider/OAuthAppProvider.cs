using Authentication.Core;
using Authentication.Services.Intefaces;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.Web
{
	public class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        private readonly IAuthenticationService _service;

        public OAuthAppProvider(IAuthenticationService service)
        {
            _service = service;
		}

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var username = context.UserName;
                var password = context.Password;
                var user = _service.Authorize(username, password);
                if (user != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim("UserID", user.Id)
                    };

                    ClaimsIdentity oAutIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                    context.Validated(new AuthenticationTicket(oAutIdentity, new AuthenticationProperties() { }));
                }
                else
                {
                    context.SetError("invalid_grant", "Error");
                }
            });
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            var dataFile = "d:\\accesstoken.txt";
            var data = new TokenIdentity() { Token = context.AccessToken, UserId = context.Identity.Claims.Last().Value };
            File.WriteAllText(@dataFile, JsonConvert.SerializeObject(data));
            return base.TokenEndpointResponse(context);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }
    }
}
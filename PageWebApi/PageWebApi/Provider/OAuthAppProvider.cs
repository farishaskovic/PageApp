using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using PageWebApi.Controllers;
using PageWebApi.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace PageWebApi.Provider
{
    public class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        [EnableCors(origins: "http:/localhost:8080", headers: "*", methods: "*")]

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var username = context.UserName;
                var password = context.Password;
                var userService = new KorisnikController();
                Korisnik user = userService.Login(username, password);
                if (user != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Ime),
                        new Claim("UserID", user.KorisnikId.ToString())
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
        [EnableCors(origins: "http:/localhost:8080", headers: "*", methods: "*")]

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
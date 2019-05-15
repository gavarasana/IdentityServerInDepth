using System;
using System.IdentityModel.Tokens;
using System.Threading.Tasks;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Ravi.Learn.Books.Startup))]

namespace Ravi.Learn.Books
{
    public  class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

            var bearerOptions = new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:50762",
                RequiredScopes = new[] { "BooksApi" },
                ValidationMode = ValidationMode.Local

            };

            app.UseIdentityServerBearerTokenAuthentication(bearerOptions);

        }
    }
}

using IdentityServer3.AccessTokenValidation;
using Owin;
using System.IdentityModel.Tokens;

namespace Ravi.Learn.Books.App_Start
{
    public partial class Startup
    {
        

        public void ConfigureAuth(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

            var bearerOptions = new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = 
            };
            app.UseIdentityServerBearerTokenAuthentication(bearerOptions);

        }
}
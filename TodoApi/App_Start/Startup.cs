using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.ActiveDirectory;
using Owin;
using System.Configuration;

[assembly: OwinStartup(typeof(AADx.TodoApi.Startup))]

namespace AADx.TodoApi
{
    public partial class Startup
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            logger.Debug("Registration Name: " + ConfigurationManager.AppSettings["Registration"]);
            var tvps = new TokenValidationParameters
            {
                //ValidateAudience = true,
                ValidAudience = ConfigurationManager.AppSettings["ida:Audience"],

                //ValidateIssuer = true,
                //ValidIssuer = issuer,

                //ValidateLifetime = true,
                //ClockSkew = TimeSpan.FromMinutes(5)
            };

            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                new WindowsAzureActiveDirectoryBearerAuthenticationOptions
                {
                    TokenValidationParameters = tvps,
                    //Audience = ConfigurationManager.AppSettings["ida:Audience"],
                    Tenant = ConfigurationManager.AppSettings["ida:Tenant"]
                });
        }
    }
}
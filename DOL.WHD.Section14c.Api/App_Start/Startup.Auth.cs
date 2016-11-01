using System;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using DOL.WHD.Section14c.Api.Providers;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.DataAccess.Identity;
using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace DOL.WHD.Section14c.Api
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["AccessTokenExpireTimeSpanMinutes"])),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = ctx =>
                    {
                        var ret = Task.Run(() =>
                        {
                            var claim = ctx.Identity.FindFirst("SecurityStamp");
                            if (claim == null) return;

                            var userManager = new ApplicationUserManager(new ApplicationUserStore(new ApplicationDbContext()));
                            var user = userManager.FindById(ctx.Identity.GetUserId());

                            // invalidate session, if SecurityStamp has changed
                            if (user?.SecurityStamp != null && user.SecurityStamp != claim.Value)
                            {
                                ctx.RejectIdentity();
                            }
                        });
                        return ret;
                    }
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["AccessTokenExpireTimeSpanMinutes"])),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = false
            };

            app.Use(async (context, next) =>
            {
                if (context.Request.QueryString.HasValue)
                {
                    if (string.IsNullOrWhiteSpace(context.Request.Headers.Get("Authorization")))
                    {
                        var queryString = HttpUtility.ParseQueryString(context.Request.QueryString.Value);
                        string token = queryString.Get("access_token");

                        if (!string.IsNullOrWhiteSpace(token))
                        {
                            context.Request.Headers.Add("Authorization", new[] { $"bearer {token}" });
                        }
                    }
                }

                await next.Invoke();
            });

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

        }
    }
}

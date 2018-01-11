using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using DOL.WHD.Section14c.DataAccess.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using DOL.WHD.Section14c.Common;

namespace DOL.WHD.Section14c.Api.Providers
{
    /// <summary>
    /// OAuth provider
    /// </summary>
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="publicClientId"></param>
        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            var roleManager = context.OwinContext.Get<ApplicationRoleManager>();

            var user = await userManager.Users.Include("Roles.Role").Include("Organizations").FirstOrDefaultAsync(x => x.UserName == context.UserName);
            if (user != null)
            {
                var passwordExpired = false;
                var passwordExpirationDays = AppSettings.Get<int>("PasswordExpirationDays");
                if (passwordExpirationDays > 0)
                {
                    passwordExpired = user.LastPasswordChangedDate.AddDays(passwordExpirationDays) < DateTime.Now;
                } 
                var validCredentials = await userManager.FindAsync(context.UserName, context.Password);
                if (await userManager.IsLockedOutAsync(user.Id))
                {
                    // account locked
                    // use invalid user name or password message to avoid disclosing that a valid username was input
                    var message = string.Format(
                        "Your account has been locked out for {0} minutes due to multiple failed login attempts.",
                        AppSettings.Get<int>("DefaultAccountLockoutTimeSpan"));
                    context.SetError("locked_out", message);
                    return;
                }
                if (!user.EmailConfirmed)
                {
                    // email not confirmed
                    // use invalid user name or password message to avoid disclosing that a valid username was input
                    context.SetError("invalid_grant", App_GlobalResources.LocalizedText.InvalidUserNameorPassword);
                }
                else if (validCredentials == null)
                {
                    // invalid credentials
                    // increment failed login count
                    if (await userManager.GetLockoutEnabledAsync(user.Id))
                    {
                        await userManager.AccessFailedAsync(user.Id);
                    }

                    context.SetError("invalid_grant", App_GlobalResources.LocalizedText.InvalidUserNameorPassword);
                }
                else if (passwordExpired)
                {
                    // password expired
                    context.SetError("invalid_grant", "Password expired");
                }
                else
                {
                    // successful login
                    ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, roleManager,
                        OAuthDefaults.AuthenticationType);

                    ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager, roleManager,
                        CookieAuthenticationDefaults.AuthenticationType);

                    AuthenticationProperties properties = CreateProperties(user.Email);

                    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                    context.Validated(ticket);
                    context.Request.Context.Authentication.SignIn(cookiesIdentity);

                    // reset failed attempts count
                    await userManager.ResetAccessFailedCountAsync(user.Id);
                }
            }
            else
            {
                // invalid username
                context.SetError("invalid_grant", App_GlobalResources.LocalizedText.InvalidUserNameorPassword);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static AuthenticationProperties CreateProperties(string email)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "email", email }
            };
            return new AuthenticationProperties(data);
        }
    }
}
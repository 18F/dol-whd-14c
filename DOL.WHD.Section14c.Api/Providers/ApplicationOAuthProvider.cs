using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Domain.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

namespace DOL.WHD.Section14c.Api.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindByNameAsync(context.UserName);
            if (user != null)
            {
                var passwordExpired = false;
                var passwordExpirationDays = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordExpirationDays"]);
                if (passwordExpirationDays > 0)
                {
                    passwordExpired = user.LastPasswordChangedDate.AddDays(passwordExpirationDays) < DateTime.Now;
                } 
                var validCredentials = await userManager.FindAsync(context.UserName, context.Password);
                if (await userManager.IsLockedOutAsync(user.Id))
                {
                    // account locked
                    // use invalid user name or password message to avoid disclosing that a valid username was input
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                }
                else if (validCredentials == null)
                {
                    // invalid credentials
                    // increment failed login count
                    if (await userManager.GetLockoutEnabledAsync(user.Id))
                    {
                        await userManager.AccessFailedAsync(user.Id);
                    }

                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                }
                else if (passwordExpired)
                {
                    // password expired
                    context.SetError("invalid_grant", "Password expired");
                }
                else
                {
                    // successful login
                    ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
                        OAuthDefaults.AuthenticationType);
                    ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                        CookieAuthenticationDefaults.AuthenticationType);

                    AuthenticationProperties properties = CreateProperties(user.UserName);
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
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

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

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}
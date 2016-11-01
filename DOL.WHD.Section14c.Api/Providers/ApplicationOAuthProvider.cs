using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using DOL.WHD.Section14c.DataAccess.Identity;
using DOL.WHD.Section14c.Domain.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Identity;

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
            var roleManager = context.OwinContext.Get<ApplicationRoleManager>();

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
                    context.SetError("invalid_grant", App_GlobalResources.LocalizedText.InvalidUserNameorPassword);
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
                    ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
                        OAuthDefaults.AuthenticationType);

                    ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                        CookieAuthenticationDefaults.AuthenticationType);

                    AuthenticationProperties properties = CreateProperties(user.Email);


                    var userRoles = user.Roles.Select(x => x.RoleId).ToList();

                    
                    if (userRoles.Count == 0)
                    {
                        // If the user is not in a role, they are an external and can complete an application.
                        oAuthIdentity.AddClaim(new Claim(ApplicationClaimTypes.ClaimPrefix + "Application", true.ToString()));
                    }
                    else
                    {
                        // Add Add Application Feature claims based on role.
                        var roles = roleManager.Roles.Where(x => userRoles.Contains(x.Id)).ToList();
                        var features = roles.SelectMany(x => x.RoleFeatures.Select(f => f.Feature));
                        foreach (var feature in features)
                        {
                            oAuthIdentity.AddClaim(new Claim(feature.Key, true.ToString()));
                        }
                    }

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
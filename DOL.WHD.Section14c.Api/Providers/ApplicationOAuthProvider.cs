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
using DOL.WHD.Section14c.Common.Extensions;
using System.IO;
using NLog;
using System.Linq;

namespace DOL.WHD.Section14c.Api.Providers
{
    /// <summary>
    /// OAuth provider
    /// </summary>
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
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
            LogEventInfo eventInfo = new LogEventInfo();
            try
            {
                var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
                var roleManager = context.OwinContext.Get<ApplicationRoleManager>();
                eventInfo.Properties["CorrelationId"] = Guid.NewGuid().ToString();
                eventInfo.LoggerName = "LoginAttempts";                
                eventInfo.Properties["IsServiceSideLog"] = 1;
                // Ensure User Email is not case sensitive
                var userName = context.UserName.TrimAndToLowerCase();
                // Handle LINQ to Entities TrimAndToLowerCase() method cannot be translated into a store expression byr using ToLower() and Trim() directly
                var user = await userManager.Users.Include("Roles.Role").Include("Organizations").FirstOrDefaultAsync(x => x.UserName.ToLower().Trim() == userName);
                if (user != null)
                {
                    eventInfo.Properties["UserId"] = user.Id;
                    eventInfo.Properties["UserName"] = user.UserName;
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
                        var message = string.Format(App_GlobalResources.LocalizedText.LoginFailureMessageAccountLockedOut, AppSettings.Get<int>("DefaultAccountLockoutTimeSpan"));
                        context.SetError("locked_out", message);
                        // Get locked out email template
                        var accountLockedOutEmailTemplatePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/AccountLockedOutEmailTemplate.txt");
                        var accountLockedOutEmailTemplateString = File.ReadAllText(accountLockedOutEmailTemplatePath);
                        //Send Account Lock Out Email
                        await userManager.SendEmailAsync(user.Id, AppSettings.Get<string>("AccountLockedoutEmailSubject"), accountLockedOutEmailTemplateString);
                        throw new UnauthorizedAccessException(string.Format("{0}: {1}", App_GlobalResources.LocalizedText.LoginFailureMessage, message));
                    }
                    if (!user.EmailConfirmed)
                    {
                        // email not confirmed
                        // use invalid user name or password message to avoid disclosing that a valid username was input
                        context.SetError("invalid_grant", App_GlobalResources.LocalizedText.LoginFailureMessageEmailNotConfirmed);
                        var message = string.Format("{0}: {1}", App_GlobalResources.LocalizedText.LoginFailureMessage, App_GlobalResources.LocalizedText.LoginFailureMessageEmailNotConfirmed);
                        eventInfo.Message = message;
                        throw new UnauthorizedAccessException(message);
                    }
                    if (validCredentials == null)
                    {
                        // invalid credentials
                        // increment failed login count
                        int loginAttempted = 0;
                        if (await userManager.GetLockoutEnabledAsync(user.Id))
                        {
                            loginAttempted = await userManager.GetAccessFailedCountAsync(user.Id);
                            await userManager.AccessFailedAsync(user.Id);                            
                        }
                        context.SetError("invalid_grant", App_GlobalResources.LocalizedText.InvalidUserNameorPassword);
                        throw new UnauthorizedAccessException(string.Format("{0}: {1} Login Attempted: {2}", App_GlobalResources.LocalizedText.LoginFailureMessage, App_GlobalResources.LocalizedText.InvalidUserNameorPassword, loginAttempted +1));
                    }
                    if (passwordExpired)
                    {
                        // password expired
                        context.SetError("invalid_grant", App_GlobalResources.LocalizedText.LoginFailureMessagePasswordExpired);
                        var message = string.Format("{0}: {1}", App_GlobalResources.LocalizedText.LoginFailureMessage, App_GlobalResources.LocalizedText.LoginFailureMessagePasswordExpired);
                        eventInfo.Message = message;
                        throw new UnauthorizedAccessException(message);
                    }

                    var data = await context.Request.ReadFormAsync();
                    var code = data.Get("code");
                    // Send authentication code if code is empty
                    if (await userManager.GetTwoFactorEnabledAsync(user.Id) && string.IsNullOrEmpty(code))
                    {
                        await userManager.UpdateSecurityStampAsync(user.Id);
                        var token = await userManager.GenerateTwoFactorTokenAsync(user.Id, "EmailCode");
                        await userManager.NotifyTwoFactorTokenAsync(user.Id, "EmailCode", token);
                        context.SetError("need_code", App_GlobalResources.LocalizedText.MissingUser2FACode);
                        var message = string.Format("{0}: {1}", "Send 2FA Code", App_GlobalResources.LocalizedText.MissingUser2FACode);
                        eventInfo.Level = LogLevel.Info;
                        eventInfo.Message = message;
                        return;
                    }
                    // Verify authentication code
                    if (await userManager.GetTwoFactorEnabledAsync(user.Id) && !await userManager.VerifyTwoFactorTokenAsync(user.Id, "EmailCode", code))
                    {
                        context.SetError("invalid_code", App_GlobalResources.LocalizedText.LoginFailureEmailCodeIncorrect);
                        var message = string.Format("{0}: {1}", App_GlobalResources.LocalizedText.LoginFailureMessage, App_GlobalResources.LocalizedText.LoginFailureEmailCodeIncorrect);
                        eventInfo.Message = message;
                        throw new UnauthorizedAccessException(App_GlobalResources.LocalizedText.LoginFailureEmailCodeIncorrect);
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
                        
                        // logging of successful attempts
                        eventInfo.Level = LogLevel.Info;
                        eventInfo.Message = App_GlobalResources.LocalizedText.LoginSuccessMessage;
                    }
                }
                else
                {
                    // invalid username
                    context.SetError("invalid_grant", App_GlobalResources.LocalizedText.InvalidUserNameorPassword);
                    throw new UnauthorizedAccessException(App_GlobalResources.LocalizedText.InvalidUserNameorPassword);
                }
            }
            catch(Exception ex)
            {
                // logging of unsuccessful attempts
                eventInfo.Message = ex.Message;
                eventInfo.Exception = ex;
                eventInfo.Level = LogLevel.Error;
            }
            finally
            {
                // Write log to database
                _logger.Log(eventInfo);
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
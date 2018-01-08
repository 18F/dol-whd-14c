using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using DOL.WHD.Section14c.Api.Filters;
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess.Identity;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RestSharp;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using DOL.WHD.Section14c.Common;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Log.LogHelper;
using System.Runtime.Serialization;
using System.Text;

namespace DOL.WHD.Section14c.Api.Controllers
{
    /// <summary>
    /// Account API controller
    /// </summary>
    [AuthorizeHttps]
    [RoutePrefix("api/Account")]
    public class AccountController : BaseApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        /// <summary>
        /// Gets the user manager for the controller
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        /// <summary>
        /// Gets the role manager for the controller
        /// </summary>
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        /// <summary>
        /// Creates a User Account
        /// </summary>
        /// <param name="model"></param>
        /// <returns>HTTP status code</returns>
        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        [DOL.WHD.Section14c.Log.ActionFilters.LoggingFilter]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest("Model state is not valid");
                }

                // Add User
                var now = DateTime.UtcNow;
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email, EmailConfirmed = false };
                user.Organizations.Add(new OrganizationMembership { EIN = model.EIN, IsAdmin = true, CreatedAt = now, LastModifiedAt = now, CreatedBy_Id = user.Id, LastModifiedBy_Id = user.Id });

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                // Add to application role
                result = await UserManager.AddToRoleAsync(user.Id, Roles.Applicant);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                // Send Verification Email
                var nounce = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                var queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString["userId"] = user.Id;
                queryString["code"] = nounce;

                //TODO: Support Urls with existing querystring
                var callbackUrl = $@"{model.EmailVerificationUrl}?{queryString}";

                await UserManager.SendEmailAsync(user.Id, "Confirm your account for the Department of Labor Section 14(c) Online Certificate Application", "Thank you for registering for Department of Labor Section 14(c) Certificate Application. Please confirm your account by clicking this link or copying and pasting it into your browser: " + callbackUrl);
            }
            catch (Exception e)
            {
                // Log Error message to database
                BadRequest(e.Message);
            }
            return Ok();
        }

        /// <summary>
        /// Returns user information for provided access_token
        /// </summary>
        /// <returns>User information, including; Email Address, Organizations, Roles, Application Claims</returns>
        // GET api/Account/UserInfo
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            var userId = ((ClaimsIdentity)User.Identity).GetUserId();
            var user = UserManager.Users.Include("Roles.Role").Include("Organizations").SingleOrDefault(s => s.Id == userId);
            return new UserInfoViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Organizations = user.Organizations,
                Roles = user.Roles.Select(r => new RoleViewModel {Id = r.RoleId, Name = r.Role.Name}),
                ApplicationClaims = user.Roles.SelectMany(y => y.Role.RoleFeatures)
                    .Where(u => u.Feature.Key.StartsWith(ApplicationClaimTypes.ClaimPrefix))
                    .Select(i => i.Feature.Key)
            };
        }

        /// <summary>
        /// Sends reset password email if account exists
        /// </summary>
        /// <param name="model">ResetPasswordViewModel</param>
        /// <returns>Http status code, for information security it will return success even if account is not found</returns>
        // POST api/Account/ResetPassword
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return Ok();
                }

                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                var queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString["userId"] = user.Id;
                queryString["code"] = code;

                //TODO: Support Urls with existing querystring
                var callbackUrl = $@"{model.PasswordResetUrl}?{queryString}";

                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Password Reset Link: " + callbackUrl);
            }
            catch (Exception e)
            {
                // Log Error message to database
                BadRequest(e.Message);
            }
            return Ok();
        }

        /// <summary>
        /// Verifies the reset password token and resets users password.  Updates email address as confirmed if not previously confirmed.
        /// </summary>
        /// <param name="model">VerifyResetPasswordViewModel</param>
        /// <returns>Http status code</returns>
        // POST api/Account/VerifyResetPassword
        [AllowAnonymous]
        [Route("VerifyResetPassword")]
        public async Task<IHttpActionResult> VerifyResetPassword(VerifyResetPasswordViewModel model)
        {
            var result = await UserManager.ResetPasswordAsync(model.UserId, model.Nounce, model.NewPassword);
            if (!result.Succeeded)
            {
                BadRequest("Unable to reset password.");
            }

            // Check if user is Confirmed, if not confirm them through password reset email verification
            var user = await UserManager.FindByIdAsync(model.UserId);
            if (!user.EmailConfirmed)
            {
                user.EmailConfirmed = true;
                await UserManager.UpdateAsync(user);
            }

            return Ok();
        }

        /// <summary>
        /// Changes password by username or bearer token
        /// </summary>
        /// <param name="model">ChangePasswordViewModel</param>
        /// <returns>Http status code</returns>
        // POST api/Account/ChangePassword
        [AllowAnonymous]
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                BadRequest("Model state is not valid");
            }
            if (!User.Identity.IsAuthenticated && string.IsNullOrEmpty(model.Email))
            {
                BadRequest("No username or bearer token provided");
            }

            string userId;
            ApplicationUser user;
            if (User.Identity.IsAuthenticated)
            {
                userId = User.Identity.GetUserId();
                user = await UserManager.FindByIdAsync(userId);
            }
            else
            {
                user = await UserManager.FindByEmailAsync(model.Email);
                if (await UserManager.IsLockedOutAsync(user.Id))
                {
                    BadRequest(App_GlobalResources.LocalizedText.InvalidUserNameorPassword);
                }

                var validCredentials = await UserManager.FindAsync(user.UserName, model.OldPassword);
                if (validCredentials == null)
                {
                    // increment failed login attempt
                    if (await UserManager.GetLockoutEnabledAsync(user.Id))
                    {
                        await UserManager.AccessFailedAsync(user.Id);
                    }
                    BadRequest(App_GlobalResources.LocalizedText.InvalidUserNameorPassword);
                }

                userId = user.Id;
            }
            IdentityResult result = await UserManager.ChangePasswordAsync(userId, model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        /// <summary>
        /// Verifies code sent during registration and updates user as email confirmed.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST api/Account/VerifyEmail
        [AllowAnonymous]
        [Route("VerifyEmail")]
        public async Task<IHttpActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            try { 
                var result = await UserManager.ConfirmEmailAsync(model.UserId, model.Nounce);
                if (!result.Succeeded)
                {
                    BadRequest("Unable to verify email");
                }
            }
            catch (Exception e)
            {
                // Log Error message to database
                BadRequest(e.Message);
            }
            return Ok();
        }


        /// <summary>
        /// Updates security stamp so token can be validated
        /// </summary>
        /// <returns></returns>
        // POST api/Account/Logout
        [Route("LogOut")]
        public async Task<IHttpActionResult> Logout()
        {
            string currentUserId = User.Identity.GetUserId();
            await UserManager.UpdateSecurityStampAsync(currentUserId);
            return Ok();
        }

        /// <summary>
        /// Password Complexity Check
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("PasswordComplexityCheck")]
        public IHttpActionResult PasswordComplexityCheck()
        {
            var responseMessage = Request.CreateResponse(HttpStatusCode.OK);
            var password = Request.Content.ReadAsStringAsync().Result;
            var passwordComplexityScore = AppSettings.Get<int>("PasswordComplexityScore");
            var zxcvbnResult = Zxcvbn.Zxcvbn.MatchPassword(password);
            if(zxcvbnResult.Score < passwordComplexityScore)
            {
                responseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
                responseMessage.Content = new StringContent("Password does not meet complexity requirements.");
            }

            return ResponseMessage(responseMessage);
        }

        /// <summary>
        /// OPTIONS endpoint for CORS
        /// </summary>
        [AllowAnonymous]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                InternalServerError("result is null");
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    BadRequest("No ModelState");
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        #endregion
    }
}

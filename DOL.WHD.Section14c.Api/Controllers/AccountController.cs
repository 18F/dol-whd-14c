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
        /// Get Employers by user
        /// </summary>
        /// <returns></returns>
        [Route("User/Employer")]
        public IHttpActionResult GetUserEmployer()
        {
            var userId = ((ClaimsIdentity)User.Identity).GetUserId();
            var user =  UserManager.Users.SingleOrDefault(s => s.Id == userId);
            var userEmployers = user.Organizations;
            return Ok(userEmployers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationMembership"></param>
        /// <returns></returns>
        [Route("User/SetEmployer")]
        public async Task<IHttpActionResult> SetUserEmployer(OrganizationMembership organizationMembership)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = ((ClaimsIdentity)User.Identity).GetUserId();
            var user = UserManager.Users.SingleOrDefault(s => s.Id == userId);
           
            user.Organizations.Add(organizationMembership);

            IdentityResult result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
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

        #region Account Management

        /// <summary>
        /// Returns collection of user accounts
        /// </summary>
        // GET api/Account
        [AuthorizeClaims(ApplicationClaimTypes.GetAccounts)]
        [HttpGet]
        public async Task<IEnumerable<UserInfoViewModel>> GetAccounts()
        {
            return await UserManager.Users.Select(x => new UserInfoViewModel
            {
                UserId = x.Id,
                Email = x.Email,
                Organizations = x.Organizations,
                Roles = x.Roles.Select(r => new RoleViewModel { Id = r.RoleId, Name = r.Role.Name }),
                ApplicationClaims = x.Roles.SelectMany(y => y.Role.RoleFeatures)
                    .Where(u => u.Feature.Key.StartsWith(ApplicationClaimTypes.ClaimPrefix))
                    .Select(i => i.Feature.Key)
            }).ToListAsync();
        }

        /// <summary>
        /// Returns user account by Id
        /// </summary>
        /// <param name="userId">User Id</param>
        // POST api/Account/{userId}
        [AuthorizeClaims(ApplicationClaimTypes.GetAccounts)]
        [HttpGet]
        [Route("{userId}")]
        public IHttpActionResult GetSingleAccount(string userId)
        {
            var user = UserManager.Users.Include("Roles.Role").SingleOrDefault(x => x.Id == userId);
            if (user == null)
            {
                BadRequest("User not found.");
            }
            return Ok(new AccountDetailsViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Organizations = user.Organizations,
                EmailConfirmed = user.EmailConfirmed,
                LastPasswordChangedDate = user.LastPasswordChangedDate,
                LockoutEndDateUtc = user.LockoutEndDateUtc,
                Roles = user.Roles.Select(r => new RoleViewModel { Id = r.RoleId, Name = r.Role.Name }),
                ApplicationClaims = user.Roles.SelectMany(y => y.Role.RoleFeatures)
                    .Where(u => u.Feature.Key.StartsWith(ApplicationClaimTypes.ClaimPrefix))
                    .Select(i => i.Feature.Key)});
        }

        /// <summary>
        /// Returns all available roles in system
        /// </summary>
        // GET api/Account/Roles
        [AuthorizeClaims(ApplicationClaimTypes.GetRoles)]
        [HttpGet]
        [Route("Roles")]
        public async Task<IEnumerable<RoleViewModel>> GetRoles()
        {
            return await RoleManager.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).OrderBy(x => x.Name).ToListAsync();
        }

        /// <summary>
        /// Creates User Account
        /// </summary>
        /// <param name="model">UserInfoViewModel</param>
        /// <returns>Http status code</returns>
        // POST api/Account
        [AuthorizeClaims(ApplicationClaimTypes.CreateAccount)]
        [HttpPost]
        public async Task<IHttpActionResult> CreateAccount(UserInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                BadRequest("Model state is not valid");
            }

            // Add User
            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            // Add to Roles
            foreach (var role in model.Roles)
            {
                var newUser = UserManager.FindByNameAsync(model.Email);
                var addResult = await UserManager.AddToRoleAsync(newUser.Result.Id, role.Name);
                if (!addResult.Succeeded)
                {
                    return GetErrorResult(result);
                }
            }

            return Ok(result);
        }

        /// <summary>
        /// Updates User Account
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="model">UserInfoViewModel</param>
        /// <returns>Http status code</returns>
        // POST api/Account/{userId}
        [AuthorizeClaims(ApplicationClaimTypes.ModifyAccount)]
        [HttpPost]
        [Route("{userId}")]
        public IHttpActionResult ModifyAccount(string userId, UserInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                BadRequest("Model state is not valid");
            }

            var user = UserManager.Users.Include("Roles.Role").SingleOrDefault(x => x.Id == userId);
            if (user == null)
            {
                BadRequest("User not found.");
            }

            // Modify User
            if (user.Email != model.Email || user.UserName != model.Email)
            {
                user.Email = model.Email;
                user.UserName = model.Email;
                var userUpdated = UserManager.Update(user);

                if (!userUpdated.Succeeded)
                {
                    return GetErrorResult(userUpdated);
                }
            }

            // Add Roles
            foreach (var role in model.Roles)
            {
                if (user.Roles.All(x => x.Role.Name != role.Name))
                {
                    var addResult = UserManager.AddToRole(user.Id, role.Name);
                    if (!addResult.Succeeded)
                    {
                        return GetErrorResult(addResult);
                    }
                }
            }

            // Remove
            foreach (var role in user.Roles.ToList())
            {
                if (model.Roles.All(x => x.Name != role.Role.Name))
                {
                    var removeResult = UserManager.RemoveFromRole(user.Id, role.Role.Name);
                    if (!removeResult.Succeeded)
                    {
                        return GetErrorResult(removeResult);
                    }
                }
            }

            return Ok();
        }
        #endregion

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

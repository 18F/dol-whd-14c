using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using DOL.WHD.Section14c.Api.Filters;
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.DataAccess.Identity;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Log.LogHelper;
using DOL.WHD.Section14c.Common;
using System.Text;
using DOL.WHD.Section14c.Common.Extensions;

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
        private readonly IEmployerService _employerService;
        private readonly IOrganizationService _organizationService;
        private readonly IIdentityService _identityService;

        /// <summary>
        /// Gets the user manager for the controller
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set { _userManager = value; }
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
        /// Default constructor for injecting dependent services
        /// </summary>
        /// <param name="employerService">
        /// The Employer service this controller should use 
        /// </param>
        /// <param name="organizationService">
        /// The organization service this controller should use
        /// </param>
        /// <summary>
        /// Default constructor for injecting dependent services
        /// </summary>
        /// <param name="identityService">
        public AccountController(IEmployerService employerService, IOrganizationService organizationService, IIdentityService identityService)
        {
            _employerService = employerService;
            _organizationService = organizationService;
            _identityService = identityService;
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
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email, EmailConfirmed = false, TwoFactorEnabled= AppSettings.Get<bool>("UserTwoFactorEnabledByDefault"), FirstName = model.FirstName, LastName = model.LastName, CreatedAt = now, LastModifiedAt = now };

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

                // Support Urls with existing querystring
                var callbackUrl = $@"{model.EmailVerificationUrl}?{queryString}";

                await UserManager.SendEmailAsync(user.Id, "Confirm your account for the Department of Labor Section 14(c) Online Certificate Application", "Thank you for registering for the Department of Labor Section 14( c ) Certificate Application. Please confirm your account by clicking this link or copying and pasting it into your browser: " + callbackUrl);
            }
            catch(Exception e)
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
                Roles = user.Roles.Select(r => new RoleViewModel { Id = r.RoleId, Name = r.Role.Name }),
                ApplicationClaims = user.Roles.SelectMany(y => y.Role.RoleFeatures)
                    .Where(u => u.Feature.Key.StartsWith(ApplicationClaimTypes.ClaimPrefix))
                    .Select(i => i.Feature.Key)
            };
        }

        /// <summary>
        /// Set user employer
        /// </summary> 
        /// <param name="organizationMembership">
        /// Organization Membership
        /// </param>
        /// <returns>HTTP status code and message</returns>
        [Route("User/SetEmployer")]
        public async Task<IHttpActionResult> SetUserEmployer(OrganizationMembership organizationMembership)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var responseMessage = Request.CreateResponse(HttpStatusCode.OK);

            // determine the uniqueness of the employer  
            var employer = _employerService.FindExistingEmployer(organizationMembership.Employer);
            if (employer == null)
            {
                var userIdentity = ((ClaimsIdentity)User.Identity);
                var userId = userIdentity.GetUserId();
                var user = UserManager.Users.SingleOrDefault(s => s.Id == userId);
                // set user organization
                user.Organizations.Add(organizationMembership);
                IdentityResult result = await UserManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
            }
            else
            {
                // Employer exists
                var orgMembership = _organizationService.GetOrganizationMembershipByEmployer(employer);
                responseMessage.StatusCode = HttpStatusCode.Found;
                responseMessage.Content = new StringContent(string.Format("{0} {1}", orgMembership?.CreatedBy?.FirstName, orgMembership?.CreatedBy?.LastName));
            }

            return ResponseMessage(responseMessage);
        }

        /// <summary>
        /// Create or update Employer Application
        /// </summary>
        /// <param name="employerId">Employer Id</param>
        /// <returns>Application Id and Status</returns>
        /// GET api/Account/User/createEmployerApplication
        [HttpGet]
        [Route("User/createEmployerApplication")]
        public async Task<IHttpActionResult> CreateOrUpdateEmployerApplication(string employerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check user user permission
            var userInfo = GetUserInfo();
            var hasPermission = _identityService.HasAddPermission(userInfo, employerId);
            if (!hasPermission)
            {
                Unauthorized("Unauthorized");
            }

            var responseMessage = Request.CreateResponse(HttpStatusCode.OK);

            var userIdentity = ((ClaimsIdentity)User.Identity);
            var userId = userIdentity.GetUserId();
            var user = UserManager.Users.Include("Roles.Role").Include("Organizations").SingleOrDefault(s => s.Id == userId);

            // Updated existing Application Id and Status
            var organization = user.Organizations.FirstOrDefault(x => x.Employer_Id == employerId && string.IsNullOrEmpty(x.ApplicationId));
            var applcationId = Guid.NewGuid().ToString();
            if (organization != null)
            {
                organization.ApplicationId = applcationId;
                organization.ApplicationStatusId = StatusIds.InProgress;
                responseMessage.Content = new StringContent(string.Format("{{\"ApplicationId\": \"{0}\", \"ApplicationStatus\": \"{1}\" }}", applcationId, StatusIds.InProgress), Encoding.UTF8, "application/json");
                IdentityResult result = await UserManager.UpdateAsync(user);
            }
            else
            {
                // Create new Application if no application is in progress.
                // Application is considered to be in progress if today's date is in the same "calendar" year as the date
                // application was created and hasn't been submitted yet.
                // In other words, New application can be created for this employer if there is no application in
                // progress with the same year for application creation date & today's date
                var notSubmittedApplication = user.Organizations.LastOrDefault(x => x.Employer_Id == employerId && x.ApplicationStatusId != StatusIds.Submitted);
                if (notSubmittedApplication == null || notSubmittedApplication.CreatedAt.Year < DateTime.Now.Year)
                {
                    var org = user.Organizations.FirstOrDefault(x => x.Employer_Id == employerId);
                    var newOrganization = new OrganizationMembership()
                    {
                        EIN = org.EIN,
                        Employer_Id = org.Employer_Id,
                        IsPointOfContact = org.IsPointOfContact,
                        ApplicationId = applcationId,
                        ApplicationStatusId = StatusIds.InProgress
                    };

                    user.Organizations.Add(newOrganization);
                    responseMessage.Content = new StringContent(string.Format("{{\"ApplicationId\": \"{0}\", \"ApplicationStatus\": \"{1}\" }}", newOrganization.ApplicationId, StatusIds.InProgress), Encoding.UTF8, "application/json");
                    IdentityResult result = await UserManager.UpdateAsync(user);
                }
                else
                {
                    responseMessage = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    responseMessage.Content = new StringContent("Can not create new application");
                }
            }

            return ResponseMessage(responseMessage);
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
                var user = await UserManager.FindByNameAsync(model.Email.TrimAndToLowerCase());
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
            var result = await UserManager.ResetPasswordAsync(model.UserId.TrimAndToLowerCase(), model.Nounce, model.NewPassword);
            if (!result.Succeeded)
            {
                BadRequest("Unable to reset password.");
            }

            // Check if user is Confirmed, if not confirm them through password reset email verification
            var user = await UserManager.FindByIdAsync(model.UserId.TrimAndToLowerCase());
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
                user = await UserManager.FindByEmailAsync(model.Email.TrimAndToLowerCase());
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
            var result = await UserManager.ConfirmEmailAsync(model.UserId.TrimAndToLowerCase(), model.Nounce);
            if (!result.Succeeded)
            {
                BadRequest("Unable to verify email");
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
        /// Get User Accounts
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
            var user = UserManager.Users.Include("Roles.Role").SingleOrDefault(x => x.Id.TrimAndToLowerCase() == userId.TrimAndToLowerCase());
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
                    .Select(i => i.Feature.Key)
            });
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
        /// Password Complexity Check
        /// </summary>
        // POST api/Account
        [AllowAnonymous]
        [HttpPost]
        [Route("PasswordComplexityCheck")]
        public IHttpActionResult PasswordComplexityCheck()
        {
            var responseMessage = Request.CreateResponse(HttpStatusCode.OK);
            var password = Request.Content.ReadAsStringAsync().Result;
            var passwordComplexityScore = AppSettings.Get<int>("PasswordComplexityScore");
            var zxcvbnResult = Zxcvbn.Zxcvbn.MatchPassword(password);
            string message = App_GlobalResources.LocalizedText.PasswordComplexityCheckSuccessMessage;
            // Check poassword Complexity Score
            if (zxcvbnResult.Score < passwordComplexityScore)
            {
                // Set Failed status code 
                responseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
                message = App_GlobalResources.LocalizedText.PasswordComplexityCheckFailedMessage;
            }
            // set reposnse message
            responseMessage.Content = new StringContent(string.Format("{{\"score\": \"{0}\", \"message\": \"{1}\" }}", zxcvbnResult.Score.ToString(), message), Encoding.UTF8, "application/json");

            return ResponseMessage(responseMessage);
        }

        /// <summary>
        /// Resend authentication code
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("SendCode")]
        public async Task<IHttpActionResult> SendAuthenticationCode(string email)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest("Unable to send code.");
                }

                var user = UserManager.FindByEmail(email);
                if (user == null)
                {
                    BadRequest("User not found.");
                }

                // Generate the token and send it
                var code = await UserManager.GenerateTwoFactorTokenAsync(user.Id, "EmailCode");
                await UserManager.NotifyTwoFactorTokenAsync(user.Id, "EmailCode", code);
            }
            catch (Exception e)
            {
                // Log Error message to database
                BadRequest(e.Message);
            }
            return Ok();
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

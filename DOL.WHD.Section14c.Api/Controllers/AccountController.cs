using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DOL.WHD.Section14c.Api.Filters;
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess.Identity;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RestSharp;

namespace DOL.WHD.Section14c.Api.Controllers
{
    [AuthorizeHttps]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
    
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        // GET api/Account/UserInfo
        [Route("UserInfo")]
        public async Task<UserInfoViewModel> GetUserInfo()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            return new UserInfoViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Organizations = user.Organizations
            };
        }

        // POST api/Account/ChangePassword
        [AllowAnonymous]
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!User.Identity.IsAuthenticated && string.IsNullOrEmpty(model.Email))
            {
                return BadRequest("No username or bearer token provided");
            }

            string userId;
            if (User.Identity.IsAuthenticated)
            {
                userId = User.Identity.GetUserId();
            }
            else
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (await UserManager.IsLockedOutAsync(user.Id))
                {
                    return BadRequest(App_GlobalResources.LocalizedText.InvalidUserNameorPassword);
                }

                var validCredentials = await UserManager.FindAsync(user.UserName, model.OldPassword);
                if (validCredentials == null)
                {
                    // increment failed login attempt
                    if (await UserManager.GetLockoutEnabledAsync(user.Id))
                    {
                        await UserManager.AccessFailedAsync(user.Id);
                    }
                    return BadRequest(App_GlobalResources.LocalizedText.InvalidUserNameorPassword);
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

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate Recaptcha
            var reCaptchaVerfiyUrl = ConfigurationManager.AppSettings["ReCaptchaVerfiyUrl"];
            var reCaptchaSecretKey = ConfigurationManager.AppSettings["ReCaptchaSecretKey"];
            if (!string.IsNullOrEmpty(reCaptchaVerfiyUrl) && !string.IsNullOrEmpty(reCaptchaSecretKey))
            {
                var remoteIpAddress = Request.GetOwinContext().Request.RemoteIpAddress;
                var reCaptchaService = new ReCaptchaService(new RestClient(reCaptchaVerfiyUrl));

                var validationResults = reCaptchaService.ValidateResponse(reCaptchaSecretKey, model.ReCaptchaResponse, remoteIpAddress);
                if (validationResults != ReCaptchaValidationResult.Disabled && validationResults != ReCaptchaValidationResult.Success)
                {
                    ModelState.AddModelError("ReCaptchaResponse", new Exception("Unable to validate reCaptcha Response"));
                    return BadRequest(ModelState);
                }
            }

            // Add User
            var now = DateTime.UtcNow;
            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
            user.Organizations.Add(new OrganizationMembership { EIN = model.EIN, IsAdmin = true, CreatedAt = now, LastModifiedAt = now, CreatedBy_Id = user.Id, LastModifiedBy_Id = user.Id });

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [AllowAnonymous]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

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

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
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
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        #endregion
    }
}

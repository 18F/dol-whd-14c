namespace DOL.WHD.Section14c.Domain.Models.Identity
{
    public static class ApplicationClaimTypes
    {
        public const string ClaimPrefix = "DOL.WHD.Section14c.";

        // Applicant
        public const string SubmitApplication = ClaimPrefix + "Application.Submit";

        // User Management
        public const string GetAccounts = ClaimPrefix + "UserManagement.GetAccounts";
        public const string CreateAccount = ClaimPrefix + "UserManagement.CreateAccount";
        public const string ModifyAccount = ClaimPrefix + "UserManagement.ModifyAccount";
        public const string GetRoles = ClaimPrefix + "UserManagement.GetRoles";

        // Application Management
        public const string ViewAllApplications = ClaimPrefix + "Application.ViewAll";

        public const string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    }
}

using System;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using Extensions;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DOL.WHD.Section14c.DataAccess.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // Responses
            // NOTE: Do not edit or remove values. If you need to change a value, set its IsActive flag to false and add a new value. This protects data integrity

            // ApplicationType
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.ApplicationType.Initial, QuestionKey = "ApplicationType", Display = "Initial Application", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.ApplicationType.Renewal, QuestionKey = "ApplicationType", Display = "Renewal Application", IsActive = true });

            // Establishment Type
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.EstablishmentType.WorkCenter, QuestionKey = "EstablishmentType", Display = "Community Rehabilitation (Work Center)", ShortDisplay = "CRP", SubDisplay = "A facility that primarily provides vocational rehabilitiation services and employment for people with disabilities.", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.EstablishmentType.PatientWorkers, QuestionKey = "EstablishmentType", Display = "Hospital/Residential Care Facility (Patient Workers)", ShortDisplay = "Hospital", SubDisplay = "A facility (public or private, non-profit or for-profit) that primarily provides residential care for individuals with disabilities.", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.EstablishmentType.SWEP, QuestionKey = "EstablishmentType", Display = "School Work Experience Program (SWEP)", ShortDisplay = "SWEP", SubDisplay = "A school-operated program in which students with disabilities may be placed in jobs with private industry within the community.", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.EstablishmentType.BusinessEstablishment, QuestionKey = "EstablishmentType", Display = "Business Establishment", ShortDisplay = "Business", SubDisplay = "Any employer other than a community rehabilitation program, hospital/residential care facility, or SWEP", IsActive = true });

            // Employer Status
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.EmployerStatus.Public, QuestionKey = "EmployerStatus", Display = "Public (State or Local Government)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.EmployerStatus.PrivateForProfit, QuestionKey = "EmployerStatus", Display = "Private, For Profit", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.EmployerStatus.PrivateNotForProfit, QuestionKey = "EmployerStatus", Display = "Private, Not For Profit", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.EmployerStatus.Other, QuestionKey = "EmployerStatus", Display = "Other, please describe:", OtherValueKey = "employerStatusOther", IsActive = true });

            // SCA
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.SCA.Yes, QuestionKey = "SCA", Display = "Yes", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.SCA.No, QuestionKey = "SCA", Display = "No", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.SCA.NoButIntendTo, QuestionKey = "SCA", Display = "No, but intend to within the next two years", IsActive = true });

            // EO13658
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.EO13658.Yes, QuestionKey = "EO13658", Display = "Yes", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.EO13658.No, QuestionKey = "EO13658", Display = "No", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.EO13658.NoButIntendTo, QuestionKey = "EO13658", Display = "No, but intend to within the next two years", IsActive = true });

            // ProvidingFacilitiesDeductionType
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.ProvidingFacilitiesDeductionType.Transportation, QuestionKey = "ProvidingFacilitiesDeductionType", Display = "Transportation", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.ProvidingFacilitiesDeductionType.Rent, QuestionKey = "ProvidingFacilitiesDeductionType", Display = "Rent", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.ProvidingFacilitiesDeductionType.Meals, QuestionKey = "ProvidingFacilitiesDeductionType", Display = "Meals", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.ProvidingFacilitiesDeductionType.Other, QuestionKey = "ProvidingFacilitiesDeductionType", Display = "Other, please specify:", OtherValueKey = "providingFacilitiesDeductionTypeOther", IsActive = true });

            // PayType
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.PayType.Hourly, QuestionKey = "PayType", Display = "Hourly", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.PayType.PieceRate, QuestionKey = "PayType", Display = "Piece Rate", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.PayType.Both, QuestionKey = "PayType", Display = "Both", IsActive = true });

            // PrevailingWageMethod
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.PrevailingWageMethod.PrevailingWageSurvey, QuestionKey = "PrevailingWageMethod", Display = "Prevailing Wage Survey", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.PrevailingWageMethod.AlternateWageData, QuestionKey = "PrevailingWageMethod", Display = "Alternate Wage Data", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.PrevailingWageMethod.SCAWageDetermination, QuestionKey = "PrevailingWageMethod", Display = "SCA Wage Determination", IsActive = true });

            // WorkSiteType
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.WorkSiteType.MainEstablishment, QuestionKey = "WorkSiteType", Display = "Main Establishment (ME)", SubDisplay = "The primary location of the employer that files this application on behalf of all its associated work sites. Note: there can only be one Main Establishment", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.WorkSiteType.BranchEstablishment, QuestionKey = "WorkSiteType", Display = "Branch establishment (BE)", SubDisplay = "A branch establishment is a physically separate work site that is part of the same organization as the main establishment", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.WorkSiteType.OffSiteWorkLocation, QuestionKey = "WorkSiteType", Display = "Off-site Work Location (OL)", SubDisplay = "An off-site work location is a work site typically on the premises of a separate establishment, where workers with disabilities...", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.WorkSiteType.SWEP, QuestionKey = "WorkSiteType", Display = "School Work Experience Program (SWEP)", SubDisplay = "A school-operated program in which students with disabilities may be placed in jobs with private industry within the community...", IsActive = true });

            // PrimaryDisability
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.PrimaryDisability.IntellectualDevelopmental, QuestionKey = "PrimaryDisability", Display = "Intellectual/Developmental Disability (IDD)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.PrimaryDisability.Psychiatric, QuestionKey = "PrimaryDisability", Display = "Psychiatric Disability (PD)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.PrimaryDisability.Visual, QuestionKey = "PrimaryDisability", Display = "Visual Impairment (VI)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.PrimaryDisability.Hearing, QuestionKey = "PrimaryDisability", Display = "Hearing Impairment (HI)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.PrimaryDisability.SubstanceAbuse, QuestionKey = "PrimaryDisability", Display = "Substance Abuse (SA)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.PrimaryDisability.Neuromuscular, QuestionKey = "PrimaryDisability", Display = "Neuromuscular Disability (NM)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.PrimaryDisability.AgeRelated, QuestionKey = "PrimaryDisability", Display = "Age Related Disability (AR)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.PrimaryDisability.Other, QuestionKey = "PrimaryDisability", Display = "Other, please specify:", IsActive = true, OtherValueKey = "primaryDisabilityOther" });

            //WIOAWorkerVerified
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.WIOAWorkerVerified.Yes, QuestionKey = "WIOAWorkerVerified", Display = "Yes", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.WIOAWorkerVerified.No, QuestionKey = "WIOAWorkerVerified", Display = "No", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = ResponseIds.WIOAWorkerVerified.NotRequired, QuestionKey = "WIOAWorkerVerified", Display = "Not Required", IsActive = true });
            
            // Seed External Roles
            context.SeedRole(Roles.Applicant);
            context.SeedRole(Roles.ApplicantAdministrator);

            // Seed Internal Roles
            context.SeedRole(Roles.SystemAdministrator);
            context.SeedRole(Roles.CertificationTeamManager);
            context.SeedRole(Roles.CertificationTeamMember);
            context.SeedRole(Roles.WageAndHourInvestigator);
            context.SeedRole(Roles.WageAndHourFieldManager);
            context.SeedRole(Roles.PolicyTeamMember);

            // Seed Admin
            var adminUserName = "14c-admin@dol.gov";
            if (!context.Users.Any(x => x.UserName == adminUserName))
            {
                context.Users.AddOrUpdate(new ApplicationUser { Id = System.Guid.Empty.ToString(), Email = adminUserName, UserName = adminUserName, LockoutEnabled = true, EmailConfirmed = true });
                context.SaveChanges();

                // Seed Password, defaults to expired and must be changed at first login.
                context.SeedPassword(adminUserName, "GC!xL91oznYvg&6WEqJJp!6KvRJD0p");

                var adminUser = context.Users.Single(x => x.UserName == adminUserName);
                adminUser.LastPasswordChangedDate = DateTime.MinValue;

                // Add to Role
                context.AddUserToRole(adminUserName, Roles.SystemAdministrator);
            }

            // Add Features
            context.AddFeature(ApplicationClaimTypes.GetAccounts, "Get list of Application Accounts");
            context.AddFeature(ApplicationClaimTypes.CreateAccount, "Create Application Accounts");
            context.AddFeature(ApplicationClaimTypes.ModifyAccount, "Change Application Accounts");
            context.AddFeature(ApplicationClaimTypes.SubmitApplication, "Submit Application");
            context.AddFeature(ApplicationClaimTypes.GetRoles, "Get list of Application Roles");
            context.AddFeature(ApplicationClaimTypes.ViewAdminUI, "Access to the admin UI");
            context.AddFeature(ApplicationClaimTypes.ViewAllApplications, "View All Submitted Applications");
            context.AddFeature(ApplicationClaimTypes.ChangeApplicationStatus, "Change the Status of a Submitted Application");

            context.SaveChanges();

            // Map Features to Roles
            context.AddRoleFeature(Roles.Applicant, ApplicationClaimTypes.SubmitApplication);

            context.AddRoleFeature(Roles.SystemAdministrator, ApplicationClaimTypes.GetAccounts);
            context.AddRoleFeature(Roles.SystemAdministrator, ApplicationClaimTypes.CreateAccount);
            context.AddRoleFeature(Roles.SystemAdministrator, ApplicationClaimTypes.ModifyAccount);
            context.AddRoleFeature(Roles.SystemAdministrator, ApplicationClaimTypes.GetRoles);
            context.AddRoleFeature(Roles.SystemAdministrator, ApplicationClaimTypes.ViewAdminUI);
            context.AddRoleFeature(Roles.SystemAdministrator, ApplicationClaimTypes.ViewAllApplications);
            context.AddRoleFeature(Roles.SystemAdministrator, ApplicationClaimTypes.ChangeApplicationStatus);

            // seed application statuses
            context.ApplicationStatuses.AddOrUpdate(new Status { Id = StatusIds.Pending, Name = "Pending", IsActive = true });
            context.ApplicationStatuses.AddOrUpdate(new Status { Id = StatusIds.Issued, Name = "Issued", IsActive = true });
            context.ApplicationStatuses.AddOrUpdate(new Status { Id = StatusIds.Withdrawn, Name = "Withdrawn", IsActive = true });
            context.ApplicationStatuses.AddOrUpdate(new Status { Id = StatusIds.Amending, Name = "Amending", IsActive = true });
            context.ApplicationStatuses.AddOrUpdate(new Status { Id = StatusIds.Denied, Name = "Denied", IsActive = true });
            context.ApplicationStatuses.AddOrUpdate(new Status { Id = StatusIds.Revoked, Name = "Revoked", IsActive = true });
            context.ApplicationStatuses.AddOrUpdate(new Status { Id = StatusIds.Expired, Name = "Expired", IsActive = true });
        }

    }
}

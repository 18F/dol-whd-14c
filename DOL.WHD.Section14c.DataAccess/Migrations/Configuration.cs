using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DOL.WHD.Section14c.DataAccess.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DOL.WHD.Section14c.DataAccess.ApplicationDbContext context)
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
            context.Responses.AddOrUpdate(new Response { Id = 1, QuestionKey = "ApplicationType", Display = "Initial Application", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 2, QuestionKey = "ApplicationType", Display = "Renewal Application", IsActive = true });

            // Establishment Type
            context.Responses.AddOrUpdate(new Response { Id = 3, QuestionKey = "EstablishmentType", Display = "Community Rehabilitation (Work Center)", SubDisplay = "A facility that primarily provides vocational rehabilitiation services and employment for people with disabilities.", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 4, QuestionKey = "EstablishmentType", Display = "Hospital/Residential Care Facility (Patient Workers)", SubDisplay = "A facility (public or private, non-profit or for-profit) that primarily provides residential care for individuals with disabilities.", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 5, QuestionKey = "EstablishmentType", Display = "School Work Experience Program (SWEP)", SubDisplay = "A school-operated program in which students with disabilities may be placed in jobs with private industry within the community.", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 6, QuestionKey = "EstablishmentType", Display = "Business Establishment", SubDisplay = "Any employer other than a community rehabilitation program, hospital/residential care facility, or SWEP", IsActive = true });

            // Employer Status
            context.Responses.AddOrUpdate(new Response { Id = 7, QuestionKey = "EmployerStatus", Display = "Public (State or Local Government)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 8, QuestionKey = "EmployerStatus", Display = "Private, For Profit", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 9, QuestionKey = "EmployerStatus", Display = "Private, Not For Profit", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 10, QuestionKey = "EmployerStatus", Display = "Other, please describe:", OtherValueKey = "EmployerStatusOther", IsActive = true });

            // SCA
            context.Responses.AddOrUpdate(new Response { Id = 11, QuestionKey = "SCA", Display = "Yes", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 12, QuestionKey = "SCA", Display = "No", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 13, QuestionKey = "SCA", Display = "No, but intend to within the next two years", IsActive = true });

            // EO13658
            context.Responses.AddOrUpdate(new Response { Id = 14, QuestionKey = "EO13658", Display = "Yes", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 15, QuestionKey = "EO13658", Display = "No", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 16, QuestionKey = "EO13658", Display = "No, but intend to within the next two years", IsActive = true });

            // ProvidingFacilitiesDeductionType
            context.Responses.AddOrUpdate(new Response { Id = 17, QuestionKey = "ProvidingFacilitiesDeductionType", Display = "Transportation", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 18, QuestionKey = "ProvidingFacilitiesDeductionType", Display = "Rent", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 19, QuestionKey = "ProvidingFacilitiesDeductionType", Display = "Meals", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 20, QuestionKey = "ProvidingFacilitiesDeductionType", Display = "Other, please specify:", OtherValueKey = "ProvidingFacilitiesDeductionTypeOther", IsActive = true });

            // PayType
            context.Responses.AddOrUpdate(new Response { Id = 21, QuestionKey = "PayType", Display = "Hourly", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 22, QuestionKey = "PayType", Display = "Piece Rate", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 23, QuestionKey = "PayType", Display = "Both", IsActive = true });

            // PrevailingWageMethod
            context.Responses.AddOrUpdate(new Response { Id = 24, QuestionKey = "PrevailingWageMethod", Display = "Prevailing Wage Survey", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 25, QuestionKey = "PrevailingWageMethod", Display = "Alternate Wage Data", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 26, QuestionKey = "PrevailingWageMethod", Display = "SCA Wage Determination", IsActive = true });

            // WorkSiteType
            context.Responses.AddOrUpdate(new Response { Id = 27, QuestionKey = "WorkSiteType", Display = "Main Establishment (ME)", SubDisplay = "The primary location of the employer that files this application on behalf of all its associated work sites. Note: there can only be one Main Establishment", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 28, QuestionKey = "WorkSiteType", Display = "Branch establishment (BE)", SubDisplay = "A branch establishment is a physically separate work site that is part of the same organization as the main establishment", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 29, QuestionKey = "WorkSiteType", Display = "Off-site Work Location (OL)", SubDisplay = "An off-site work location is a work site typically on the premises of a separate establishment, where workers with disabilities...", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 30, QuestionKey = "WorkSiteType", Display = "School Work Experience Program (SWEP)", SubDisplay = "A school-operated program in which students with disabilities may be placed in jobs with private industry within the community...", IsActive = true });

            // PrimaryDisability
            context.Responses.AddOrUpdate(new Response { Id = 31, QuestionKey = "PrimaryDisability", Display = "Intellectual/Developmental Disability (IDD)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 32, QuestionKey = "PrimaryDisability", Display = "Psychiatric Disability (PD)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 33, QuestionKey = "PrimaryDisability", Display = "Visual Impairment (VI)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 34, QuestionKey = "PrimaryDisability", Display = "Hearing Impairment (HI)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 35, QuestionKey = "PrimaryDisability", Display = "Substance Abuse (SA)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 36, QuestionKey = "PrimaryDisability", Display = "Neuromuscular Disability (NM)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 37, QuestionKey = "PrimaryDisability", Display = "Age Related Disability (AR)", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 38, QuestionKey = "PrimaryDisability", Display = "Other, please specify:", IsActive = true });

            //WIOAWorkerVerified
            context.Responses.AddOrUpdate(new Response { Id = 39, QuestionKey = "WIOAWorkerVerified", Display = "Yes", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 40, QuestionKey = "WIOAWorkerVerified", Display = "No", IsActive = true });
            context.Responses.AddOrUpdate(new Response { Id = 41, QuestionKey = "WIOAWorkerVerified", Display = "Not Required", IsActive = true });

        }
    }
}

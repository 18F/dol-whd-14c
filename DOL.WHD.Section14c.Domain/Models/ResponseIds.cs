namespace DOL.WHD.Section14c.Domain.Models
{
    public static class ResponseIds
    {
        public static class ApplicationType
        {
            public const int Initial = 1;
            public const int Renewal = 2;
        }

        public static class EstablishmentType
        {
            public const int WorkCenter = 3;
            public const int PatientWorkers = 4;
            public const int SWEP = 5;
            public const int BusinessEstablishment = 6;
        }

        public static class EmployerStatus
        {
            public const int Public = 7;
            public const int PrivateForProfit = 8;
            public const int PrivateNotForProfit = 9;
            public const int Other = 10;
        }

        public static class SCA
        {
            public const int Yes = 11;
            public const int No = 12;
            public const int NoButIntendTo = 13;
        }

        public static class EO13658
        {
            public const int Yes = 14;
            public const int No = 15;
            public const int NoButIntendTo = 16;
        }

        public static class ProvidingFacilitiesDeductionType
        {
            public const int Transportation = 17;
            public const int Rent = 18;
            public const int Meals = 19;
            public const int Other = 20;
        }

        public static class PayType
        {
            public const int Hourly = 21;
            public const int PieceRate = 22;
            public const int Both = 23;
        }

        public static class PrevailingWageMethod
        {
            public const int PrevailingWageSurvey = 24;
            public const int AlternateWageData = 25;
            public const int SCAWageDetermination = 26;
        }

        public static class WorkSiteType
        {
            public const int MainEstablishment = 27;
            public const int BranchEstablishment = 28;
            public const int OffSiteWorkLocation = 29;
            public const int SWEP = 30;
        }

        public static class PrimaryDisability
        {
            public const int IntellectualDevelopmental = 31;
            public const int Psychiatric = 32;
            public const int Visual = 33;
            public const int Hearing = 34;
            public const int SubstanceAbuse = 35;
            public const int Neuromuscular = 36;
            public const int AgeRelated = 37;
            public const int Other = 38;
        }

        public static class WIOAWorkerVerified
        {
            public const int Yes = 39;
            public const int No = 40;
            public const int NotRequired = 41;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Domain.Models
{
    public static class UserActionIds
    {
        public const int NewRegistration = 1; //"NewRegistation";
        public const int Updated         = 2; //"Updated";
        public const int Login           = 3; //"UserLogin";
        public const int Logout          = 4; //"UserLogout";
        public const int Disable         = 5; //"Disabled";
        public const int Enable          = 6; //"Enabled";
        public const int Delete          = 7; //"Deleted";
        public const int UnDelete        = 8; //"UnDeleted";
    }

    public static class ActionTypes
    {
        public const string Admin = "Admin";
        public const string NonAdmin = "Non-Admin";
    }


}

using System;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WorkSiteWorkSiteType
    {
        public Guid WorkSiteId { get; set; }
        public WorkSite WorkSite { get; set; }

        public int WorkSiteTypeId { get; set; }
        public Response WorkSiteType { get; set; }
    }
}

using DOL.WHD.Section14c.Domain.Models;
using System;
using DOL.WHD.Section14c.Domain.Models.Identity;

namespace DOL.WHD.Section14c.DataAccess
{
    public interface IAuditedEntity
    {
        string CreatedBy_Id { get; set; }
        ApplicationUser CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }

        string LastModifiedBy_Id { get; set; }
        ApplicationUser LastModifiedBy { get; set; }
        DateTime LastModifiedAt { get; set; }
    }
}

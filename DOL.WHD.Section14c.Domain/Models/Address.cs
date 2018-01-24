using DOL.WHD.Section14c.Common.Extensions;
using System;

namespace DOL.WHD.Section14c.Domain.Models
{
    public class Address : BaseEntity
    {
        public Address()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        
        public string StreetAddress { get; set; }
        
        public string City { get; set; }

        public string State { get; set; }
        
        public string ZipCode { get; set; }
        
        public string County { get; set; }

    }
}

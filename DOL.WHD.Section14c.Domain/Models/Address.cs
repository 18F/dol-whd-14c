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

        private string _streetAddress;
        public string StreetAddress {
            get { return this._streetAddress; }
            set { this._streetAddress = value.TrimAndToLowerCase(); }
        }

        private string _city;
        public string City {
            get { return this._city; }
            set { this._city = value.TrimAndToLowerCase(); }
        }
        
        public string State { get; set; }

        private string _zipCode;
        public string ZipCode {
            get { return this._zipCode; }
            set { this._zipCode = value.TrimAndToLowerCase(); }
        }

        private string _county;
        public string County {
            get { return this._county; }
            set { this._county = value.TrimAndToLowerCase(); }
        }
        
    }
}

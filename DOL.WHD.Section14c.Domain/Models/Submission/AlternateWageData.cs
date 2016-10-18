using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class AlternateWageData
    {
        public string AlternateWorkDescription { get; set; }
        public string AlternateDataSourceUsed { get; set; }
        public double PrevailingWageProvidedBySource { get; set; }
        public DateTime DataRetrieved { get; set; }
    }
}

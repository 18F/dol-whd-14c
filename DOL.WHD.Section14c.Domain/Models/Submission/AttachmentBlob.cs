using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class AttachmentBlob: BaseEntity
    {
        public AttachmentBlob()
        {
            if (string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Name of the file as uploaded by the end user
        /// </summary>
        public string Blob { get; set; }
    }
}

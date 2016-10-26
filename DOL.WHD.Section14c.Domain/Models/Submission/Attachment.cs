using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class Attachment : BaseEntity
    {
        public Attachment()
        {
            if (Id == Guid.Empty)
                Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the file as uploaded by the end user
        /// </summary>
        [Required]
        [StringLength(255)]
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Path where the file is stored in the File Repository
        /// </summary>
        [Required]
        [IgnoreDataMember]
        [StringLength(255)]
        public string RepositoryFilePath { get; set; }

        [Required]
        public long FileSize { get; set; }

        [Required]
        [StringLength(255)]
        public string MimeType { get; set; }

        [IgnoreDataMember]
        public bool Deleted { get; set; }

    }
}

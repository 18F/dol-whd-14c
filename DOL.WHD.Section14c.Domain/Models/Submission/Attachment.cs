using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class Attachment : BaseEntity
    {
        public Attachment()
        {
            if (string.IsNullOrEmpty( Id) )
                Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Name of the file as uploaded by the end user
        /// </summary>
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Path where the file is stored in the File Repository
        /// </summary>
        [IgnoreDataMember]
        public string RepositoryFilePath { get; set; }

        public long FileSize { get; set; }

        public string MimeType { get; set; }

        public string ApplicationId { get; set; }

        [IgnoreDataMember]
        public bool Deleted { get; set; }

    }
}

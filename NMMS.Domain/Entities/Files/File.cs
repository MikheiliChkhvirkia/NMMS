using NMMS.Common.Domain.Entities;
using NMMS.Domain.Entities.DistributorEntity;

namespace NMMS.Domain.Entities.Files
{
    public partial class File : BaseEntity<Guid>
    {
        /// <summary>
        /// A name of the file.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A sufix of the file.
        /// </summary>
        public int? FileNameSufixNumber { get; set; }
        /// <summary>
        /// An extension of the file.
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// A physical path of the file.
        /// </summary>
        public string PhysicalPath { get; set; }
        /// <summary>
        /// A MIME type of the file.
        /// </summary>
        public string MimeType { get; set; }
        /// <summary>
        /// A size of the file in bytes.
        /// </summary>
        public long LengthInBytes { get; set; }
        /// <summary>
        /// A date and time the file was created.
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// A date and time the file was deleted.
        /// </summary>
        public DateTime? DateDeleted { get; set; }

        public virtual Distributor Distributor { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Vin.Core.Model
{
    /// <summary>
    /// Base Entity with ID, created and updated dates
    /// </summary>
    public abstract partial class BaseIdEntity : BaseEntity
    {
        public BaseIdEntity()
        {
            CreatedDate = DateTime.UtcNow;
            UpdatedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        [Key]
        public int ID { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

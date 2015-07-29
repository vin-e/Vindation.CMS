using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vin.Core.Model.MultiTenancy;

namespace Vin.Core.Model
{
    /// <summary>
    /// Bases Entity with ID, Tenant, created and updated dates
    /// </summary>
    public abstract class BaseTenantIdEntity : BaseEntity
    {
        public BaseTenantIdEntity()
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

        /// <summary>
        /// Gets or sets the TenantID
        /// </summary>
        public int Tenant_ID { get; set; }
        [ForeignKey("Tenant_ID")]
        public virtual Tenant Tenant { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Vin.Core.Model.MultiTenancy
{
    /// <summary>
    /// Tenant Identifier
    /// </summary>
    public class TenantIdentifier : BaseTenantIdEntity
    {
        /// <summary>
        /// Gets or sets the Name of the tenant identifier
        /// </summary>
        [MaxLength(255)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets if is the default identifier
        /// </summary>
        public virtual bool IsDefault { get; set; }
    }
}

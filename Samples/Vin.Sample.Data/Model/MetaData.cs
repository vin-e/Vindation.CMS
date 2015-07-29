using System.ComponentModel.DataAnnotations;
using Vin.Core.Model;

namespace Vin.Sample.Data.Model
{
    public class MetaData : BaseTenantIdEntity
    {
        /// <summary>
        /// Gets or sets the entity type by name
        /// </summary>
        [MaxLength(100)]
        public virtual string EntityType { get; set; }

        /// <summary>
        /// Gets or sets the id from the entity
        /// </summary>
        public virtual int EntityID { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        [MaxLength(255)]
        public virtual string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        [MaxLength(255)]
        public virtual string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        [MaxLength(100)]
        public virtual string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets the Permalink title
        /// </summary>
        public virtual string Permalink { get; set; }

        /// <summary>
        /// Gets or sets if this is the home page
        /// </summary>
        public virtual bool IsHomePage { get; set; }
    }
}

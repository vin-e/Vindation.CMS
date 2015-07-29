using System.ComponentModel.DataAnnotations;
using Vin.Core.Model;

namespace Vin.Sample.Data.Model.Marketing
{
    public class Contact : BaseTenantIdEntity
    {
        /// <summary>
        /// Gets or sets name
        /// </summary>
        [Required, MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets email
        /// </summary>
        [Required, MaxLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets phone
        /// </summary>
        [MaxLength(100)]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets message
        /// </summary>
        public string Message { get; set; }
    }
}

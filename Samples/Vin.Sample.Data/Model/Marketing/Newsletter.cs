using System.ComponentModel.DataAnnotations;
using Vin.Core.Model;

namespace Vin.Sample.Data.Model.Marketing
{
    public class Newsletter : BaseTenantIdEntity
    {
        /// <summary>
        /// Gets or sets email address
        /// </summary>
        [Required, MaxLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets OptIn value
        /// </summary>
        public bool OptIn { get; set; }
    }
}

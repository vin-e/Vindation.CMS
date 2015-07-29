
using System.ComponentModel.DataAnnotations;
namespace Vin.Core.Model.Locations
{
    public class State : BaseIdEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [MaxLength(255), Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation
        /// </summary>
        [MaxLength(10), Required]
        public string Abbreviation { get; set; }
    }
}

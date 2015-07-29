using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vin.Core.Model;

namespace Vin.Sample.Data.Model.Menus
{
    public class Menu : BaseTenantIdEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [MaxLength(255)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the menu items
        /// </summary>
        public virtual ICollection<MenuItem> Items { get; set; }
    }
}

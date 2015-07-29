using Vin.Core.Model;

namespace Vin.Sample.Data.Model.Menus
{
    public class MenuItem : BaseIdEntity
    {
        /// <summary>
        /// Gets or sets the metadata information for the link
        /// </summary>
        public virtual MetaData Information { get; set; }

        /// <summary>
        /// Gets or sets the parent menu item of this item
        /// </summary>
        public virtual MenuItem ParentMenu { get; set; }

        /// <summary>
        /// Gets or sets the menu identifier
        /// </summary>
        public virtual int Menu_ID { get; set; }

        /// <summary>
        /// Gets or sets the menu
        /// </summary>
        public virtual Menu Menu { get; set; }
    }
}

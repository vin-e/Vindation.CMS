using System.ComponentModel.DataAnnotations;

namespace Vin.Core.Model.Configuration
{
    public partial class Setting : BaseTenantIdEntity
    {
        public Setting() { }

        public Setting(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        [MaxLength(500)]
        public string Value { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

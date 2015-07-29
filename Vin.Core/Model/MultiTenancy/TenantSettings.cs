using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vin.Core.Configuration;

namespace Vin.Core.Model.MultiTenancy
{
    public class TenantSettings : ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether tenant is enbaled
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the default theme
        /// </summary>
        public string DefaultTheme { get; set; }
    }
}

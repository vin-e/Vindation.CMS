using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vin.Sample.Web.Models
{
    public class MenuItemViewModel
    {
        public string Name { get; set; }
        public string PermaLink { get; set; }

        public List<MenuItemViewModel> Items { get; set; }
    }
}
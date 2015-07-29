using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vin.Sample.Web.Areas.Admin.Models
{
    public class LeadsDataViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Message { get; set; }

        public string CreatedDate { get; set; }
    }
}
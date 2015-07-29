using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vin.Sample.Web.Areas.Admin.Models
{
    public class NewsletterDataViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string CreatedDate { get; set; }

        public bool OptIn { get; set; }
    }
}
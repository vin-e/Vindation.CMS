using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vin.Sample.Web.Areas.Admin.Models
{
    public class PageDataViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        public string HtmlBody { get; set; }

        public string TextBody { get; set; }

        public bool IsPublished { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatedDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vin.Sample.Web.Areas.Admin.Models
{
    public class EventDataViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public bool IsPublished { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
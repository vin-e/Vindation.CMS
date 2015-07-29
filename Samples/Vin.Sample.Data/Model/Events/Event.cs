using System;
using System.ComponentModel.DataAnnotations;
using Vin.Core.Model;

namespace Vin.Sample.Data.Model.Events
{
    public class Event : BaseTenantIdEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the start date
        /// </summary>
        public DateTime StartDate { get; set; } 
        
        /// <summary>
        /// Gets or sets the end date
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the address
        /// </summary>
        [MaxLength(255)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city
        /// </summary>
        [MaxLength(100)]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state
        /// </summary>
        [MaxLength(100)]
        public string State { get; set; }
        
        /// <summary>
        /// Gets or sets the zip code
        /// </summary>
        [MaxLength(25)]
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the details 
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets if event is published
        /// </summary>
        public bool IsPublished { get; set; }
    }
}

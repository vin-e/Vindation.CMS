using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vin.Sample.Data.Model.Events;

namespace Vin.Sample.Web.Models
{
    public class EventsViewModel
    {
        public ICollection<EventViewModel> Events { get; set; }

        public int TotalEvents { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }

    public class EventViewModel
    {
        public EventViewModel() { }
        public EventViewModel(Event e) 
        {
            Id = e.ID;
            Name = e.Name;
            StartDate = e.StartDate;
            EndDate = e.EndDate;
            Details = e.Details;
            Address = e.Address;
            City = e.City;
            State = e.State;
            ZipCode = e.ZipCode;
        }

        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
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
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the zip code
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the details 
        /// </summary>
        public string Details { get; set; }
    }
}
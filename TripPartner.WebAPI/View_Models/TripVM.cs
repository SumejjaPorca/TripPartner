using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using TripPartner.WebAPI.Domain_Models;

namespace TripPartner.WebAPI.View_Models
{
    public class TripVM
    {

        [DisplayName("Trip Id")]
        public int Id { get; set; }
        [DisplayName("Trip Started")]
        public DateTime DateStarted { get; set; }

        [DisplayName("Trip ended")]
        public DateTime DateEnded { get; set; }
        [DisplayName("Creator Id")]
        public string CreatorId { get; set; }
        [DisplayName("Creator Username")]
        public string CreatorUsername { get; set; }
        
        [DisplayName("Is cancelled")]
        public bool IsCancelled { get; set; }
        [DisplayName("Destination")]
        public LocationVM Destination { get; set; }
        [DisplayName("Origin")]
        public LocationVM Origin { get; set; }
    }
}
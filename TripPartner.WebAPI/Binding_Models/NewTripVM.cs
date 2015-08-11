using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripPartner.WebAPI.Binding_Models
{
    public class NewTripVM
    {
        [Required]
        [DisplayName("Trip Started")]
        public DateTime DateStarted { get; set; }
        [Required]
        [DisplayName("Trip ended")]
        public DateTime DateEnded { get; set; }
        public string CreatorId { get; set; }
        [Required]
        [DisplayName("Destination")]
        public NewLocVM Destination { get; set; }
        [Required]
        [DisplayName("Origin")]
        public NewLocVM Origin { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripPartner.WebAPI.Binding_Models
{
    public class NewLocVM
    {
        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }
        [Required]
        [DisplayName("Latitude")]
        public double Lat { get; set; }
        [Required]
        [DisplayName("Longitude")]
        public double Long { get; set; }
    }
}
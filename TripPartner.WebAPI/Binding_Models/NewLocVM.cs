using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TripPartner.WebAPI.Binding_Models
{
    public class NewLocVM
    {
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Latitude")]
        public double Lat { get; set; }
        [DisplayName("Longitude")]
        public double Long { get; set; }
    }
}
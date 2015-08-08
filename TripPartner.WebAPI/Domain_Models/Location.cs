using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Spatial;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripPartner.WebAPI.Domain_Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        public System.Spatial.GeographyPoint LatLng { get; set; }
        [InverseProperty("Origin")] 
        public virtual ICollection<Trip> TripsFrom { get; set; } //this location
        [InverseProperty("Destination")] 
        public virtual ICollection<Trip> TripsTo { get; set; } //this location

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TripPartner.WebAPI.Models;

namespace TripPartner.WebAPI.Domain_Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Trip started")]
        public DateTime DateStarted { get; set; }

        [DisplayName("Trip ended")]
        public DateTime DateEnded { get; set; }
        public string CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public ApplicationUser Creator { get; set; }
        public int DestinationId { get; set; }
         [ForeignKey("DestinationId")]
         public Location Destination { get; set; }
         public int OriginId { get; set; }
         [ForeignKey("OriginId")]
         public Location Origin { get; set; }
        public virtual ICollection<Story> Stories { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using TripPartner.WebAPI.Models;

namespace TripPartner.WebAPI.Binding_Models
{
    public class StoryVM
    {

        [DisplayName("Story Id")]
        public int Id { get; set; }
        [DisplayName("Story made")]
        public DateTime DateMade { get; set; }

        [DisplayName("Last edited")]
        public DateTime LastEdit { get; set; }
        [DisplayName("Story date")]
        public DateTime Date { get; set; }
        [DisplayName("Text")]
        public string Text { get; set; }
        [DisplayName("Creator Id")]
        public string CreatorId { get; set; }
        [DisplayName("Creator Username")]
        public string CreatorUsername { get; set; }
        [DisplayName("Trip")]
        public TripVM Trip { get; set; }
        [DisplayName("Rating")]
        public double Rating { get; set; }
        [DisplayName("Rates")]
        public int Rates { get; set; }
    }
}
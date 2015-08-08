using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using TripPartner.WebAPI.Models;

namespace TripPartner.WebAPI.View_Models
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
        [DisplayName("Creator")]
        public ApplicationUser Creator { get; set; }
        [DisplayName("Trip")]
        public TripVM Trip { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripPartner.WebAPI.Binding_Models
{
    public class NewStoryVM
    {
        [DisplayName("Title")]
        [Required]
        public string Title { get; set; }
        [DisplayName("Story made")]
        public DateTime DateMade { get; set; }

        [DisplayName("Last edited")]
        public DateTime LastEdit { get; set; }
        [DisplayName("Story date")]
        public DateTime Date { get; set; }

        [Required]
        [DisplayName("Text")]
        public string Text { get; set; }

        [DisplayName("Creator Id")]
        public string CreatorId { get; set; }



        [Required]
        [DisplayName("TripId")]
        public int TripId { get; set; }
    }
}
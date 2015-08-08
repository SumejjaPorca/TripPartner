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
    public class Story
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Story made")]
        public DateTime DateMade { get; set; }

        [DisplayName("Last edited")]
        public DateTime LastEdit { get; set; }
        [DisplayName("Story date")]
        public DateTime Date { get; set; }
        [DisplayName("Text")]
        public string Text { get; set; }
        public string CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public ApplicationUser Creator { get; set; }
        public int TripId { get; set; }
        [ForeignKey("TripId")]
        public Trip Trip { get; set; }

    }
}
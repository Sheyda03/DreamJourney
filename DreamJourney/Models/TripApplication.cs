using DreamJourney.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DreamJourney.Data.Models
{
    public class TripApplication : BaseModel
    {
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(TripId))]
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public int PeopleCount { get; set; }

        [Required]
        public ApplicationStatus Status { get; set; }
    }
}

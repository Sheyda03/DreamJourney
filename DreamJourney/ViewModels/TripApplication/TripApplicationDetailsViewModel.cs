using DreamJourney.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DreamJourney.ViewModels.TripApplication
{
    public class TripApplicationDetailsViewModel : BaseViewModel
    {
        public int UserId { get; set; }

        public string UserFirstLastName { get; set; }

        public string UserEmail { get; set; }
        public string TripTitle { get; set; } = null!;
        public string TripDestinations { get; set; } = null!;
        public int TripId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Моля въведи брой хора")]
        public int PeopleCount { get; set; }
        public ApplicationStatus Status { get; set; }
    }
}

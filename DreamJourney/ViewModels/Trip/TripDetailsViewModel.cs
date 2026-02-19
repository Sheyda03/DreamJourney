using DreamJourney.ViewModels.TripApplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DreamJourney.ViewModels.Trip
{
    public class TripDetailsViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int Days { get; set; }
        public string Destinations { get; set; }
        public double Price { get; set; }
        [DisplayName("От")]
        public string FromPlace { get; set; }
        public int Seats { get; set; }
        public int AvailableSeats { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsTripCreator { get; set; }
        public bool IsTraveller { get; set; }
        public int? LoggedUserId { get; set; }
        public bool HasApplied { get; set; }
         public int CreatorUserId { get; set; }

        [DisplayName("Организатор")]
        public string UserFirstLastName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string DepartmentName { get; set; }  
        public List<string> Features { get; set; }
        public List<TripListViewModel> SimilarTrips { get; set; }
        public List<TripApplicationDetailsViewModel> TripApplications { get; set; }
    }
}

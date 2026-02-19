using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DreamJourney.ViewModels.Trip
{
    public class TripListViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public int Days { get; set; }
        public string Destinations { get; set; }
        public string FromPlace { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }

        public string UserFirstLastNames { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string DepartmentName { get; set; }
    }
}

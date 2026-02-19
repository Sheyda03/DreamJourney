using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DreamJourney.Data.Models
{
    public class Trip : BaseModel
    {
        public Trip()
        {
            this.TripApplications = new List<TripApplication>();
        }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0, 365)]
        public int Days { get; set; }

        [Required]
        [MaxLength(200)]
        public string Destinations { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [MaxLength(100)]
        public string FromPlace { get; set; }

        [Required]
        [Range(0, 1000)]
        public int Seats { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        public User User { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public ICollection<TripFeature> TripFeatures { get; set; } = new List<TripFeature>();
        public ICollection<TripApplication> TripApplications { get; set; }
    }
}

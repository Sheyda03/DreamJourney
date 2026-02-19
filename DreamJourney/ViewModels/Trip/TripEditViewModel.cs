using DreamJourney.ViewModels.Category;
using DreamJourney.ViewModels.Department;
using DreamJourney.ViewModels.Feature;
using DreamJourney.ViewModels.SubCategory;
using System.ComponentModel.DataAnnotations;

namespace DreamJourney.ViewModels.Trip
{
    public class TripEditViewModel : BaseViewModel
    {
        [Required]
        [MaxLength(100)]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Отпътуване")]
        public DateTime Date { get; set; }

        [Required]
        [Range(1, 365)]
        [Display(Name = "Брой дни")]
        public int Days { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Дестинации")]
        public string Destinations { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "От")]
        public string FromPlace { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public double Price { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Брой места")]
        public int Seats { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "URL на снимка")]
        public string ImageUrl { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        public List<DepartmentViewModel> Departments { get; set; } = new();
        public List<CategoryViewModel> Categories { get; set; } = new();
        public List<SubCategoryViewModel> SubCategories { get; set; } = new();

        public List<FeatureGroupViewModel> FeatureGroups { get; set; } = new();

        public List<int> SelectedFeatureIds { get; set; } = new();

        public string? SingleSelectedFeatureIds { get; set; }
    }
}

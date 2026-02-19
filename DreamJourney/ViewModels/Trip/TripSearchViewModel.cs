using DreamJourney.ViewModels.Category;
using DreamJourney.ViewModels.Department;
using DreamJourney.ViewModels.SubCategory;

namespace DreamJourney.ViewModels.Trip
{
    public class TripSearchViewModel
    {
        public string SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int? DepartmentId { get; set; }
        public List<DepartmentViewModel> Departments { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<SubCategoryViewModel> SubCategories { get; set; }
        public List<TripListViewModel> Results { get; set; }
    }
}

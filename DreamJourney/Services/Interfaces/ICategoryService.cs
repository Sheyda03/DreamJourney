using DreamJourney.Data.Models;

namespace DreamJourney.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<List<SubCategory>> GetSubCategoriesByCategoryAsync(int categoryId);
        Task<List<SubCategory>> GetAllSubCategoriesAsync();
        Task<List<Department>> GetDepartmentsAsync();
        Task<List<Feature>> GetFeaturesAsync();
        Task<List<Category>> GetCategoriesByDepartmentAsync(int departmentId);
    }
}

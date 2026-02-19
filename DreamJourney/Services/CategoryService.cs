using DreamJourney.Data;
using DreamJourney.Data.Models;
using DreamJourney.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DreamJourney.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DreamJourneyDbContext _context;

        public CategoryService(DreamJourneyDbContext context)
        {
            _context = context;
        }

        public Task<List<Category>> GetCategoriesAsync()
            => _context.Categories.ToListAsync();

        public Task<List<SubCategory>> GetSubCategoriesByCategoryAsync(int categoryId)
            => _context.SubCategories.Where(s => s.CategoryId == categoryId).ToListAsync();
        public Task<List<SubCategory>> GetAllSubCategoriesAsync()
            => _context.SubCategories.ToListAsync();
        public Task<List<Department>> GetDepartmentsAsync()
            => _context.Departments.ToListAsync();

        public Task<List<Feature>> GetFeaturesAsync()
            => _context.Features.ToListAsync();
        public async Task<List<Category>> GetCategoriesByDepartmentAsync(int departmentId)
        {
            return await _context.Categories
                .Where(c => c.DepartmentId == departmentId)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}

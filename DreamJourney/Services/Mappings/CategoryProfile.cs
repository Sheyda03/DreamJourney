using AutoMapper;
using DreamJourney.Data.Models;
using DreamJourney.ViewModels.Category;

namespace DreamJourney.Services.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<Category, CategoryViewModel>();
        }        
    }
}

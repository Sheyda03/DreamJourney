using AutoMapper;
using DreamJourney.Data.Models;
using DreamJourney.ViewModels.SubCategory;

namespace DreamJourney.Services.Mappings
{
    public class SubCategoryProfile : Profile
    {
        public SubCategoryProfile()
        {
            CreateMap<SubCategory, SubCategoryViewModel>();
        }
    }
}

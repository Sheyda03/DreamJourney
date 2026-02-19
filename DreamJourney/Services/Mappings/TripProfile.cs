using AutoMapper;
using DreamJourney.Data.Models;
using DreamJourney.Data.Models.Enums;
using DreamJourney.ViewModels.Trip;

namespace DreamJourney.Services.Mappings
{
    public class TripProfile : Profile
    {
        public TripProfile()
        {            
            CreateMap<TripEditViewModel, Trip>()
                .ForMember(t => t.Id, opt => opt.Ignore())
                .ForMember(t => t.TripFeatures, opt => opt.Ignore())
                .ForMember(t => t.TripApplications, opt => opt.Ignore());

            
            CreateMap<Trip, TripEditViewModel>()
                .ForMember(dest => dest.SelectedFeatureIds,
                    opt => opt.MapFrom(src =>
                        src.TripFeatures.Select(tf => tf.FeatureId)));

            
            CreateMap<Trip, TripListViewModel>()
                .ForMember(dest => dest.UserFirstLastNames,
                    opt => opt.MapFrom(src =>
                        src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src =>
                        src.Category.Name))
                .ForMember(dest => dest.SubCategoryName,
                    opt => opt.MapFrom(src =>
                        src.SubCategory.Name))
                .ForMember(dest => dest.DepartmentName,
                    opt => opt.MapFrom(src =>
                        src.Department.Name));

           
            CreateMap<Trip, TripDetailsViewModel>()
                .ForMember(dest => dest.UserFirstLastName,
                    opt => opt.MapFrom(src =>
                        src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.CreatorUserId,
                    opt => opt.MapFrom(src =>
                        src.UserId))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src =>
                        src.SubCategory.Category.Name))
                .ForMember(dest => dest.SubCategoryName,
                    opt => opt.MapFrom(src =>
                        src.SubCategory.Name))
                .ForMember(dest => dest.DepartmentName,
                    opt => opt.MapFrom(src =>
                        src.Department.Name))
                .ForMember(dest => dest.Features,
                    opt => opt.MapFrom(src =>
                        src.TripFeatures.Select(tf => tf.Feature.Name)))
                .ForMember(dest => dest.AvailableSeats,
                    opt => opt.MapFrom(src =>
                        src.Seats - src.TripApplications
                            .Where(a => a.Status == ApplicationStatus.Accepted || a.Status == ApplicationStatus.Completed)
                            .Sum(a => a.PeopleCount)));
        }
    }
}

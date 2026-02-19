using AutoMapper;
using DreamJourney.Data.Models;
using DreamJourney.ViewModels.TripApplication;

public class TripApplicationProfile : Profile
{
    public TripApplicationProfile()
    {
        CreateMap<TripApplication, TripApplicationDetailsViewModel>()
            .ForMember(dest => dest.UserFirstLastName,
                opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
            .ForMember(dest => dest.UserEmail,
                opt => opt.MapFrom(src => src.User.Email))
            .ForMember(d => d.TripTitle,
                opt => opt.MapFrom(s => s.Trip.Title))
            .ForMember(d => d.TripDestinations,
                opt => opt.MapFrom(s => s.Trip.Destinations)); 
    }
}

using AutoMapper;
using DreamJourney.Data.Models;
using DreamJourney.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJourney.Services.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterViewModel, User>();
        }
    }
}

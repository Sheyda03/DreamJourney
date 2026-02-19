using AutoMapper;
using DreamJourney.Data.Models;
using DreamJourney.ViewModels.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJourney.Services.Mappings
{
    public class FeatureProfile : Profile
    {
        public FeatureProfile()
        {
            CreateMap<Feature, FeatureViewModel>();
        }
    }
}

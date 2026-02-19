using DreamJourney.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DreamJourney.Data.Models
{
    public class Feature : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int FeatureCategoryId { get; set; }
        public FeatureCategory FeatureCategory { get; set; }

        public ICollection<TripFeature> TripFeatures { get; set; } = new List<TripFeature>();
    }
}



using System;
using System.Collections.Generic;
using System.Text;

namespace DreamJourney.Data.Models
{
    public class TripFeature
    {
        public int TripId { get; set; }
        public Trip Trip { get; set; }

        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
    }

}

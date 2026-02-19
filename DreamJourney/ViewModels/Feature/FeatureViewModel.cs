using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJourney.ViewModels.Feature
{
    public class FeatureViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FeatureCategoryId { get; set; }
        public string FeatureCategoryName { get; set; }
    }
}

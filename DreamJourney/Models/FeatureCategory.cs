using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJourney.Data.Models
{
    public class FeatureCategory : BaseModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public bool SingleSelection { get; set; }

        public ICollection<Feature> Features { get; set; } = new List<Feature>();
    }

}

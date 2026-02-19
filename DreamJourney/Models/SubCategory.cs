using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DreamJourney.Data.Models
{
    public class SubCategory : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}

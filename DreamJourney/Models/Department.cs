using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DreamJourney.Data.Models
{
    public class Department : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}


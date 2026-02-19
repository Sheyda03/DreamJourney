using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DreamJourney.Data.Models
{
    public class Category : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}

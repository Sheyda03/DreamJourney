using System;
using System.ComponentModel.DataAnnotations;

namespace DreamJourney.Data.Models
{
    public class BaseModel
    {
        [Required]
        [Key]
        public int Id { get; set; }
    }
}

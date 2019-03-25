using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityProject.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Rating")]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string RatingValue { get; set; }

        [StringLength(400, MinimumLength = 1)]
        public string Description { get; set; }

        [Required]
        public int CreatedOn { get; set; }

        public virtual ICollection<Game> Game { get; set; }
    }
}
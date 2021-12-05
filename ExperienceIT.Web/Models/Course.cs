using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExperienceIT.Web.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Course Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Course Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Tutor Name")]
        public string TutorName { get; set; }

        [Required]
        [Display(Name = "Course Rating")]
        public int CourseRating { get; set; }

        [Required]
        public double Duration { get; set; }

        [Display(Name = "Resources and Links")]
        public string Resources { get; set; }
    }
}

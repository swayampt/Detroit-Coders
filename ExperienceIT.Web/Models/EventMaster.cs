using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExperienceIT.Web.Models
{
    public class EventMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Event Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Event Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Starting Date")]
        public DateTime StartingDate { get; set; }

        [Required]
        [Display(Name = "Ending Date")]
        public DateTime EndingDate { get; set; }

        [Required]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        [Required]
        [Display(Name = "Venue")]
        public string Venue { get; set; }

        [Required]
        [Display(Name = "Duration")]
        public string Duration { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExperienceIT.Web.Models
{
    public class ProgramMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Program Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Program Description")]
        public string Description { get; set; }

        [Display(Name = "Program Url")]
        public string ProgramUrl { get; set; }

    }
}

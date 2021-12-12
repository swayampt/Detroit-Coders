using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExperienceIT.Web.Models
{
    public class OrganizerMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Sponsoring Company")]
        public string Name { get; set; }
        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(30)]
        public string State { get; set; }

        [StringLength(15)]
        public string Zipcode { get; set; }

        [StringLength(50)]
        public string Country { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExperienceIT.Web.Models
{
    public class ProgramOrganizerMapper
    {
        [Key]
        public int Id { get; set; }

        public int ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        public virtual ProgramMaster ProgramMaster { get; set; }

        public int OrganizerId { get; set; }
        [ForeignKey("OrganizerId")]
        public virtual OrganizerMaster OrganizerMaster { get; set; }

        
    }
}

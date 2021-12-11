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

        public int OrganaizerId { get; set; }
        [ForeignKey("OrganaizerId")]
        public virtual OrganizerMaster OrganaizerMaster { get; set; }

        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public virtual StudentMaster StudentMaster { get; set; }
    }
}

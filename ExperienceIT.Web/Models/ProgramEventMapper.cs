using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExperienceIT.Web.Models
{
    public class ProgramEventMapper
    {
        [Key]
        public int Id { get; set; }

        public int ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        public virtual ProgramMaster ProgramMaster { get; set; }

        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public virtual EventMaster EventMaster { get; set; }
        

    }
}

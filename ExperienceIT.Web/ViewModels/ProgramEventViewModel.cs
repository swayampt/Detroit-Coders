using ExperienceIT.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExperienceIT.Web.ViewModels
{
    public class ProgramEventViewModel
    {
        public ProgramEventViewModel()
        {
            Event = new EventMaster();
        }
        public EventMaster Event { get; set; }
        public string ProgramName { get; set; }
        public int ProgramId { get; set; }
        public List<ProgramMaster> ProgramList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExperienceIT.Web.Models;

namespace ExperienceIT.Web.ViewModels
{
    public class ProgramOrganizerViewModel
    {
        public ProgramOrganizerViewModel()
        {
            Organizer = new OrganizerMaster();
        }

        public ProgramMaster Program { get; set; }

        public OrganizerMaster Organizer { get; set; }
        public string ProgramName { get; set; }
        public int ProgramId { get; set; }
        public int OrganizationId { get; set; }
        public List<OrganizerMaster> Organizations { get; set; }
        public List<OrganizerMaster> ProgramOrganizations { get; set; }
        public List<ProgramMaster> ProgramList { get; set; }
        public List<ProgramOrganizerMapper> ProgramOrganizerMappers { get; set; }


    }
}


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
            Event = new EventMaster();
        }

        public ProgramMaster Program { get; set; }

        public OrganizerMaster Organizer { get; set; }
        public  EventMaster Event { get; set; }
        public string ProgramName { get; set; }
        public int ProgramId { get; set; }
        public int OrganizationId { get; set; }
        public List<OrganizerMaster> Organizations { get; set; }
        public List<OrganizerMaster> ProgramOrganizations { get; set; }
        public int EventId { get; set; }
        public List<EventMaster> Events { get; set; }
        public List<EventMaster> ProgramEvents { get; set; }
        public List<ProgramMaster> ProgramList { get; set; }
        public List<ProgramEventMapper> EventMappers { get; set; }
        public List<ProgramOrganizerMapper> ProgramOrganizerMappers { get; set; }


    }
}


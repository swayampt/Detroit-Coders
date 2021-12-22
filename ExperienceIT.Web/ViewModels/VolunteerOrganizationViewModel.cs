using ExperienceIT.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExperienceIT.Web.ViewModels
{
    public class VolunteerOrganizationViewModel
    {
        public IEnumerable<VolunteerMaster> volunteers { get; set; }
        public List<OrganizerMaster> organizationList { get; set; }
    }
}

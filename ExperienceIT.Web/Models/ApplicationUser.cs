using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExperienceIT.Web.Models
{
    public class ApplicationUser: IdentityUser
    {

        public string Name { get; set; }
        public string StreetAddress { get; set; }
        //public string PhoneNumber { get; set; }removed phone number because it's already exist.
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }


    }
}

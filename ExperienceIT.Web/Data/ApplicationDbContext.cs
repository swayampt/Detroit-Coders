using ExperienceIT.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceIT.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<VolunteerMaster> VolunteerMaster { get; set; }
        public DbSet<StudentMaster> StudentMaster { get; set; }
        public DbSet<OrganizerMaster> OrganizerMaster { get; set; }
        public DbSet<EventMaster> EventMaster { get; set; }
        public DbSet<ProgramMaster> ProgramMaster { get; set; }
        public DbSet<ProgramEventMapper> ProgramEventMapper { get; set; }
        public DbSet<ProgramEventStudentMapper> ProgramEventStudentMapper { get; set; }
        public DbSet<ProgramEventVolunteerMapper> ProgramEventVolunteerMapper { get; set; }
        public DbSet<ProgramOrganizerMapper> ProgramOrganizerMapper { get; set; }
        public DbSet<VolunteerEventMapper> VolunteerEventMapper { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        

        
    }
}

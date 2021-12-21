// Author: Swayam Tripathy
// Date: 12/14/2021
// Description: DbInitializer.cs file is created to seed roles data and also creates default application admin user.

using ExperienceIT.Web.Models;
using ExperienceIT.Web.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ExperienceIT.Web.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // Constructor of DbInitializer
        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // Overriding Initiliaze() method of IDbInitializer
        public async void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            if (_db.Roles.Any(r => r.Name == Utility.SD.ManagerUser)) return;

            // If roles do not exist in DB, then create.
            _roleManager.CreateAsync(new IdentityRole(SD.ManagerUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.VolunteerUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.StudentUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.GuestUser)).GetAwaiter().GetResult();

            // Data seeding for Admin User
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "ExpITGroup2@gmail.com",
                Email = "ExpITGroup2@gmail.com",
                FirstName = "Admin",
                LastName = "Group2",
                EmailConfirmed = true,
                PhoneNumber = "123-456-7890",
                StreetAddress = "123 Main St",
                City = "Detroit",
                State = "MI",
                Zipcode = "48266"
            }, "!23First").GetAwaiter().GetResult();

            IdentityUser user = await _db.Users.FirstOrDefaultAsync(u => u.Email == "ExpITGroup2@gmail.com");

            await _userManager.AddToRoleAsync(user, SD.ManagerUser);
        }
    }
}
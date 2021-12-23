// Author: ExperienceIT Group2
// Date: 12/22/2021
// Discription: Index.cshtml.cs file is used to load and update user profile data.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ExperienceIT.Web.Data;
using ExperienceIT.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExperienceIT.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public List<OrganizerMaster> Organizations { get; private set; }

        public class InputModel
        {
            public string Username { get; set; }

            [Phone]
            public string PhoneNumber { get; set; }

            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            public string StreetAddress { get; set; }

            public string City { get; set; }

            public string State { get; set; }

            public string Zipcode { get; set; }

            public string Skills { get; set; }

            public int YearsOfExperience { get; set; }

            public string WorkPlace { get; set; }
        }

        // Load application user (admin & student) and volunteer data
        private async Task LoadAsync(ApplicationUser user)
        {
            Input = new InputModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                StreetAddress = user.StreetAddress,
                City = user.City,
                State = user.State,
                Zipcode = user.Zipcode
            };

            // Load volunteer specific data
            if (User.IsInRole("Volunteer"))
            {
                var userId = user.Id;
                var volunteer = await _context.VolunteerMaster
                .Where(x => x.UserId == userId).FirstOrDefaultAsync();

                if (volunteer == null)
                {
                    _logger.LogError($"Unable to load volunteer with user ID '{_userManager.GetUserId(User)}'.");
                    return;
                }

                Input.Skills = volunteer.Skills;
                Input.YearsOfExperience = volunteer.YearsOfExperience;
                Input.WorkPlace = volunteer.CurrentOrganization;
            }
        }

        // Load Edit Profile page
        public async Task<IActionResult> OnGetAsync()
        {
            var user = (ApplicationUser)await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Organizations = _context.OrganizerMaster.ToList();

            await LoadAsync(user);

            return Page();
        }

        // SAVE/UPDATE user profile
        public async Task<IActionResult> OnPostAsync()
        {
            var user = (ApplicationUser)await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if ((!ModelState.IsValid))
            {
                await LoadAsync(user);
                return Page();
            }

            if (Organizations == null)
            {
                Organizations = _context.OrganizerMaster.ToList();
            }

            // Update volunteer data in volunteer master table
            if (User.IsInRole("Volunteer"))
            {
                var volunteer = await _context.VolunteerMaster.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();

                if (volunteer == null)
                {
                    _logger.LogError($"Unable to load volunteer with user ID '{_userManager.GetUserId(User)}'.");
                    return NotFound($"Unable to load volunteer data with user ID '{_userManager.GetUserId(User)}'.");
                }

                volunteer.Phone = Input.PhoneNumber;
                volunteer.FirstName = Input.FirstName;
                volunteer.LastName = Input.LastName;
                volunteer.Address = Input.StreetAddress;
                volunteer.City = Input.City;
                volunteer.State = Input.State;
                volunteer.Zipcode = Input.Zipcode;

                if (!string.IsNullOrEmpty(Input.Skills))
                {
                    volunteer.Skills = Input.Skills;
                }

                if (!string.IsNullOrEmpty(Input.YearsOfExperience.ToString()))
                {
                    volunteer.YearsOfExperience = Input.YearsOfExperience;
                }

                if (!string.IsNullOrEmpty(Input.WorkPlace))
                {
                    volunteer.CurrentOrganization = Input.WorkPlace;
                }

                _context.Update(volunteer).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            } // End - volunteer update


            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }

            if (Input.StreetAddress != user.StreetAddress)
            {
                user.StreetAddress = Input.StreetAddress;
            }

            if (Input.City != user.City)
            {
                user.City = Input.City;
            }

            if (Input.State != user.State)
            {
                user.State = Input.State;
            }

            if (Input.Zipcode != user.Zipcode)
            {
                user.Zipcode = Input.Zipcode;
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Updated profile data of user with ID '{_userManager.GetUserId(User)}'.");
            }

            await _signInManager.RefreshSignInAsync(user);

            StatusMessage = "Your profile has been updated";

            return RedirectToPage();
        }
    }
}
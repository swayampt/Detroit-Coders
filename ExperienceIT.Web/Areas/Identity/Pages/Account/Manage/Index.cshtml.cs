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

namespace ExperienceIT.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            public string FirstName { get; set; }
            public string Email { get; set; }
            public string LastName { get; set; }
            public string StreetAddress { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zipcode { get; set; }
            public string Skills { get; set; }
            public int YearsOfExperience { get; set; }
            public string WorkPlace { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            var userId = user.Id;
            var volunteer = await _context.VolunteerMaster
                .Where(x => x.UserId == userId).FirstOrDefaultAsync();

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                LastName = ((ApplicationUser)user).FirstName,
                FirstName = ((ApplicationUser)user).LastName,
                Skills = volunteer.Skills,
                YearsOfExperience = volunteer.YearsOfExperience,
                WorkPlace = volunteer.CurrentOrganization,
                StreetAddress = volunteer.Address,
                City = volunteer.City,
                State = volunteer.State,
                Zipcode = volunteer.Zipcode,
                Email = userName
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            //Update Volunteer Master 

            var volunteer = await _context.VolunteerMaster.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();

            volunteer.Phone = Input.PhoneNumber;
            volunteer.Skills = Input.Skills;
            volunteer.YearsOfExperience = Input.YearsOfExperience;
            volunteer.CurrentOrganization = Input.WorkPlace;
            volunteer.Address = Input.StreetAddress;
            volunteer.City = Input.City;
            volunteer.State = Input.State;
            volunteer.Zipcode = Input.Zipcode;

            _context.Update(volunteer).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}

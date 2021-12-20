using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ExperienceIT.Web.Data;
using ExperienceIT.Web.Models;
using ExperienceIT.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace ExperienceIT.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _context = context;
         }

        [BindProperty]
        public InputModel Input { get; set; }

        public List<OrganizerMaster> Organizations { get; private set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            public string StreetAddress { get; set; }
            public string PhoneNumber { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zipcode { get; set; }
            public string Skills { get; set; }
            public int YearsOfExperience { get; set; }
            public string WorkPlace { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            Organizations = _context.OrganizerMaster.ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var IsVolunteer = !string.IsNullOrEmpty(Request.Form["Skills"].ToString()) ? true : false ;
            string role = IsVolunteer ? Request.Form["rdUserRole"].ToString() : "";
            string skills = IsVolunteer ? Request.Form["Skills"].ToString() : "";
            int yearsOfExperience = IsVolunteer ? Convert.ToInt32(Request.Form["YearsOfExperience"].ToString()) : 0;
            string workPlace = Request.Form["WorkPlace"].ToString();

            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                { 
                  UserName = Input.Email, 
                  Email = Input.Email, 
                  FirstName = Input.FirstName,
                  LastName = Input.LastName,
                  PhoneNumber = Input.PhoneNumber,
                  StreetAddress = Input.StreetAddress,
                  City = Input.City,
                  State = Input.State,
                  Zipcode = Input.Zipcode,                  
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (role == SD.VolunteerUser)
                    {
                        await _userManager.AddToRoleAsync(user, SD.VolunteerUser);

                        //Create an entry into the VolunteerMaster


                        var volunteer = new VolunteerMaster()
                        {
                            LastName = user.LastName,
                            FirstName = user.FirstName,
                            UserId = user.Id,
                            Skills = skills ,
                            YearsOfExperience = yearsOfExperience,
                            AgeStatus = 0,
                            CurrentOrganization = workPlace,
                            Availability = true,
                            Phone = user.PhoneNumber,
                            Address = user.StreetAddress,
                            City = user.City,
                            State = user.State,
                            Zipcode = user.Zipcode,
                            Country = "USA"
                        };

                        _context.Add(volunteer);
                        await _context.SaveChangesAsync();
                        
                    }
                    else
                    {
                        if (role == SD.StudentUser)
                        {
                            await _userManager.AddToRoleAsync(user, SD.StudentUser);
                        }
                        else
                        {
                            if (role == SD.ManagerUser)
                            {
                                await _userManager.AddToRoleAsync(user, SD.ManagerUser);
                            }
                            else
                            {
                                await _userManager.AddToRoleAsync(user, SD.GuestUser);
                                await _signInManager.SignInAsync(user, isPersistent: false);
                                return LocalRedirect(returnUrl);
                            }
                        }
                    }
                                        

                    _logger.LogInformation("User created a new account with password.");

                    /*var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {*/
                    await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    /*}*/
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

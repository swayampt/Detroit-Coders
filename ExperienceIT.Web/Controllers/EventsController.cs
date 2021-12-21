using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExperienceIT.Web.Data;
using ExperienceIT.Web.Models;
using ExperienceIT.Web.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;

namespace ExperienceIT.Web.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public EventsController(ApplicationDbContext context, 
            IEmailSender emailSender, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
        }

       

            // GET: EventMasters
        public async Task<IActionResult> Index(int? programId)//passing program id to redirect into respective page.
        {
            var events = await _context.EventMaster.ToListAsync();
            var programEventMapper= new List<ProgramEventMapper>();
            if (programId == null || programId == 0 )
            {
                programEventMapper = await _context.ProgramEventMapper
                .Include(x => x.ProgramMaster)
                .Include(x => x.EventMaster).ToListAsync();

            } else
            {
                programEventMapper = await _context.ProgramEventMapper
                    .Where (x=>x.ProgramId==programId)
               .Include(x => x.ProgramMaster)
               .Include(x => x.EventMaster).ToListAsync();
            }
            

            if (User.Identity.IsAuthenticated)
            {
                if (!User.IsInRole("ApplicationAdmin"))
                {
                    var user = await _userManager.GetUserAsync(User);
                    var userId = user.Id;
                    var volunteer = await _context.VolunteerMaster
                        .Where(x => x.UserId == userId).FirstOrDefaultAsync();
                    var volMapper = await _context.ProgramEventVolunteerMapper
                                    .Where(x => x.VolunteerId == volunteer.Id).ToListAsync();
                    ViewBag.VEM = volMapper;
                }
            }

            //For every record in the programeventmapper you will have to create an
            //instance of the programeventviewmodel and provide the values for all the
            //properties - Event, ProgramId, ProgramName and ProgramList.

            //List<ProgramEventViewModel> result = new List<ProgramEventViewModel>();

            //foreach(var x in programEventMapper)
            //{
            //    var viewModel = new ProgramEventViewModel()
            //    {
            //        Event = new EventMaster()
            //        {
            //            Id = x.EventMaster.Id,
            //            Name = x.EventMaster.Name,
            //            Description = x.EventMaster.Description,
            //            StartingDate = x.EventMaster.StartingDate,
            //            EndingDate = x.EventMaster.EndingDate,
            //            EnrollmentDate = x.EventMaster.EnrollmentDate,
            //            Venue = x.EventMaster.Venue,
            //            Duration = x.EventMaster.Duration
            //        },
            //        ProgramName = x.ProgramMaster.Name,
            //        ProgramId = x.ProgramMaster.Id
            //    };
            //    result.Add(viewModel);
            //}

            var model = programEventMapper.Select(x => new ProgramEventViewModel()
            {
                Event = new EventMaster()
                {
                    Id = x.EventMaster.Id,
                    Name = x.EventMaster.Name,
                    Description = x.EventMaster.Description,
                    StartingDate = x.EventMaster.StartingDate,
                    EndingDate = x.EventMaster.EndingDate,
                    EnrollmentDate = x.EventMaster.EnrollmentDate,
                    Venue = x.EventMaster.Venue,
                    Duration = x.EventMaster.Duration
                },
                ProgramName = x.ProgramMaster.Name,
                ProgramId = x.ProgramMaster.Id
            }).OrderBy(x => x.ProgramName).ToList();

            return View(model);
        }
        //Getting the event fot the specific program
        public async Task<IActionResult> EventByProgramId(int programId)
        {
            var events = await _context.EventMaster.ToListAsync();

            if (User.Identity.IsAuthenticated)
            {
                if (!User.IsInRole("ApplicationAdmin"))
                {
                    var user = await _userManager.GetUserAsync(User);
                    var userId = user.Id;
                    var volunteer = await _context.VolunteerMaster
                        .Where(x => x.UserId == userId).FirstOrDefaultAsync();
                    var volMapper = await _context.ProgramEventVolunteerMapper
                                    .Where(x => x.VolunteerId == volunteer.Id).ToListAsync();
                    ViewBag.VEM = volMapper;
                }
            }
            var programEventMapper = await _context.ProgramEventMapper
                .Where(x => x.ProgramId == programId)
                .Include(x => x.ProgramMaster)
                .Include(x => x.EventMaster).ToListAsync();

            var model = programEventMapper.Select(x => new ProgramEventViewModel()
            {
                Event = new EventMaster()
                {
                    Id = x.EventMaster.Id,
                    Name = x.EventMaster.Name,
                    Description = x.EventMaster.Description,
                    StartingDate = x.EventMaster.StartingDate,
                    EndingDate = x.EventMaster.EndingDate,
                    EnrollmentDate = x.EventMaster.EnrollmentDate,
                    Venue = x.EventMaster.Venue,
                    Duration = x.EventMaster.Duration
                },
                ProgramName = x.ProgramMaster.Name,
                ProgramId = x.ProgramMaster.Id
            }).OrderBy(x => x.ProgramName).ToList();

            return View("Index", model);
        }

        public async Task<string> Register(int eventId, int progId, int flag)
        {
            //Get logged in volunteer details

            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var message = string.Empty;

            var volunteer = await _context.VolunteerMaster
                .Where(x => x.UserId == userId).FirstOrDefaultAsync();

            var volunteerId = volunteer.Id;

            if (flag == 1) //Register
            {
                var volunteerEventProgramMapper = new ProgramEventVolunteerMapper()
                {
                    ProgramId = progId,
                    EventId = eventId,
                    VolunteerId = volunteerId
                };

                await _context.ProgramEventVolunteerMapper.AddAsync(volunteerEventProgramMapper);
                message = "You were successfully registered for this event.";
            }
            else //UnRegister
            {
                var mapper = await _context.ProgramEventVolunteerMapper.
                    Where(x => x.VolunteerId == volunteerId && x.EventId == eventId && x.ProgramId == progId)
                    .FirstOrDefaultAsync();

                _context.ProgramEventVolunteerMapper.Remove(mapper);
                message = "You were successfully un-registered from this event.";
            }

            await _context.SaveChangesAsync();


            // Send Email

            //await _emailSender.SendEmailAsync(User.Identity.Name, "Volunteer Registration", "You are registered");


            return message;
            
        }

        // GET: EventMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventMaster = await _context.EventMaster
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventMaster == null)
            {
                return NotFound();
            }

            return View(eventMaster);
        }

        // GET: EventMasters/Create
        public IActionResult Create()
        {
            ProgramEventViewModel model = new ProgramEventViewModel()
            {
                Event = new EventMaster(),
                ProgramList = _context.ProgramMaster.ToList()
            };

            return View(model);
        }

        // POST: EventMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProgramEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model.Event);
                await _context.SaveChangesAsync();

                // Update the ProgramEventMapper

                var mapper = new ProgramEventMapper()
                {
                    EventId = model.Event.Id,
                    ProgramId = model.ProgramId
                };

                _context.Add(mapper);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: EventMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventMaster = await _context.EventMaster.FindAsync(id);
            if (eventMaster == null)
            {
                return NotFound();
            }
            return View(eventMaster);
        }

        // POST: EventMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartingDate,EndingDate,EnrollmentDate,Venue,Duration")] EventMaster eventMaster)
        {
            if (id != eventMaster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventMasterExists(eventMaster.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(eventMaster);
        }

        // GET: EventMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventMaster = await _context.EventMaster
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventMaster == null)
            {
                return NotFound();
            }

            return View(eventMaster);
        }

        // POST: EventMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventMaster = await _context.EventMaster.FindAsync(id);
            _context.EventMaster.Remove(eventMaster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventMasterExists(int id)
        {
            return _context.EventMaster.Any(e => e.Id == id);
        }

        //public async Task<IActionResult> Register()
        //{
        //    string userName = User.Identity.Name;
           
        //    if(User.IsInRole("Volunteer"))
        //    {
        //        //Put entry into ProgramEventVolunteerMapper
        //    }
        //    else if (User.IsInRole("Student"))
        //    {
        //        //Put entry into ProgramEventStudentMapper
        //    }

        //    return RedirectToAction(nameof(Index));
        //}

    }
}

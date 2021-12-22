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

namespace ExperienceIT.Web.Controllers
{
    public class VolunteersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VolunteersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VolunteerMasters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VolunteerMaster.Include(v => v.User);
            VolunteerOrganizationViewModel viewModel = new VolunteerOrganizationViewModel()
            {
                volunteers = await _context.VolunteerMaster.ToListAsync(),
                organizationList = await _context.OrganizerMaster.ToListAsync()
            };
            return View(viewModel);
        }

        // GET: VolunteerMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteerMaster = await _context.VolunteerMaster
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volunteerMaster == null)
            {
                return NotFound();
            }

            return View(volunteerMaster);
        }

        // GET: VolunteerMasters/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: VolunteerMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Phone,LinkedIn,Skills,YearsOfExperience,CurrentOrganization,Address,City,State,Zipcode,Country,AgeStatus,Aboutme,Availability,UserId")] VolunteerMaster volunteerMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volunteerMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", volunteerMaster.UserId);
            return View(volunteerMaster);
        }

        // GET: VolunteerMasters/Edit/5
        public async Task<IActionResult> Edit(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var volunteerMaster = await _context.VolunteerMaster.Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (volunteerMaster == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", volunteerMaster.UserId);
            return View(volunteerMaster);
        }

        // POST: VolunteerMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Phone,LinkedIn,Skills,YearsOfExperience,CurrentOrganization,Address,City,State,Zipcode,Country,AgeStatus,Aboutme,Availability,UserId")] VolunteerMaster volunteerMaster)
        {
            if (id != volunteerMaster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volunteerMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunteerMasterExists(volunteerMaster.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", volunteerMaster.UserId);
            return View(volunteerMaster);
        }

        // GET: VolunteerMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteerMaster = await _context.VolunteerMaster
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volunteerMaster == null)
            {
                return NotFound();
            }

            return View(volunteerMaster);
        }

        // POST: VolunteerMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteerMaster = await _context.VolunteerMaster.FindAsync(id);
            _context.VolunteerMaster.Remove(volunteerMaster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolunteerMasterExists(int id)
        {
            return _context.VolunteerMaster.Any(e => e.Id == id);
        }
    }
}
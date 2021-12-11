using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExperienceIT.Web.Data;
using ExperienceIT.Web.Models;

namespace ExperienceIT.Web.Controllers
{
    public class VolunteerEventMappersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VolunteerEventMappersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VolunteerEventMappers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VolunteerEventMapper.Include(v => v.EventMaster).Include(v => v.VolunteerMaster);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: VolunteerEventMappers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteerEventMapper = await _context.VolunteerEventMapper
                .Include(v => v.EventMaster)
                .Include(v => v.VolunteerMaster)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volunteerEventMapper == null)
            {
                return NotFound();
            }

            return View(volunteerEventMapper);
        }

        // GET: VolunteerEventMappers/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.EventMaster, "Id", "Description");
            ViewData["VolunteerId"] = new SelectList(_context.VolunteerMaster, "Id", "FirstName");
            return View();
        }

        // POST: VolunteerEventMappers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventId,VolunteerId")] VolunteerEventMapper volunteerEventMapper)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volunteerEventMapper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.EventMaster, "Id", "Description", volunteerEventMapper.EventId);
            ViewData["VolunteerId"] = new SelectList(_context.VolunteerMaster, "Id", "FirstName", volunteerEventMapper.VolunteerId);
            return View(volunteerEventMapper);
        }

        // GET: VolunteerEventMappers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteerEventMapper = await _context.VolunteerEventMapper.FindAsync(id);
            if (volunteerEventMapper == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.EventMaster, "Id", "Description", volunteerEventMapper.EventId);
            ViewData["VolunteerId"] = new SelectList(_context.VolunteerMaster, "Id", "FirstName", volunteerEventMapper.VolunteerId);
            return View(volunteerEventMapper);
        }

        // POST: VolunteerEventMappers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventId,VolunteerId")] VolunteerEventMapper volunteerEventMapper)
        {
            if (id != volunteerEventMapper.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volunteerEventMapper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunteerEventMapperExists(volunteerEventMapper.Id))
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
            ViewData["EventId"] = new SelectList(_context.EventMaster, "Id", "Description", volunteerEventMapper.EventId);
            ViewData["VolunteerId"] = new SelectList(_context.VolunteerMaster, "Id", "FirstName", volunteerEventMapper.VolunteerId);
            return View(volunteerEventMapper);
        }

        // GET: VolunteerEventMappers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteerEventMapper = await _context.VolunteerEventMapper
                .Include(v => v.EventMaster)
                .Include(v => v.VolunteerMaster)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volunteerEventMapper == null)
            {
                return NotFound();
            }

            return View(volunteerEventMapper);
        }

        // POST: VolunteerEventMappers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteerEventMapper = await _context.VolunteerEventMapper.FindAsync(id);
            _context.VolunteerEventMapper.Remove(volunteerEventMapper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolunteerEventMapperExists(int id)
        {
            return _context.VolunteerEventMapper.Any(e => e.Id == id);
        }
    }
}

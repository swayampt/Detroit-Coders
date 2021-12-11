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
    public class EventMastersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventMastersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventMasters
        public async Task<IActionResult> Index()
        {
            return View(await _context.EventMaster.ToListAsync());
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
            return View();
        }

        // POST: EventMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartingDate,EndingDate,EnrollmentDate,Venue,Duration")] EventMaster eventMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventMaster);
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
    }
}

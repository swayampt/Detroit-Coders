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
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventMasters
        public async Task<IActionResult> Index()
        {
            var events = await _context.EventMaster.ToListAsync();
            
            var programEventMapper = await _context.ProgramEventMapper
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
            }).ToList();

            return View(model);
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
    }
}

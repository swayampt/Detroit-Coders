﻿using System;
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
    public class OrganizersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganizersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Organizers
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrganizerMaster.ToListAsync());
        }

        // GET: Organizers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizerMaster = await _context.OrganizerMaster
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizerMaster == null)
            {
                return NotFound();
            }

            return View(organizerMaster);
        }

        // GET: Organizers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organizers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,City,State,Zipcode,Country")] OrganizerMaster organizerMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organizerMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organizerMaster);
        }

        // GET: Organizers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizerMaster = await _context.OrganizerMaster.FindAsync(id);
            if (organizerMaster == null)
            {
                return NotFound();
            }
            return View(organizerMaster);
        }

        // POST: Organizers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,City,State,Zipcode,Country")] OrganizerMaster organizerMaster)
        {
            if (id != organizerMaster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizerMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizerMasterExists(organizerMaster.Id))
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
            return View(organizerMaster);
        }

        // GET: Organizers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizerMaster = await _context.OrganizerMaster
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizerMaster == null)
            {
                return NotFound();
            }

            return View(organizerMaster);
        }

        // POST: Organizers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organizerMaster = await _context.OrganizerMaster.FindAsync(id);
            _context.OrganizerMaster.Remove(organizerMaster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizerMasterExists(int id)
        {
            return _context.OrganizerMaster.Any(e => e.Id == id);
        }
    }
}
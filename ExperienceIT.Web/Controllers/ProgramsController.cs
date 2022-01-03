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
/// <summary>
/// Author: ExperienceIT group2    
///Description:All the CRUD operations of Programs are done.
/// </summary>
namespace ExperienceIT.Web.Controllers
{
    public class ProgramsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganizerMaster Organizer { get; private set; }

        public ProgramsController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public IActionResult GetProgramPage(int programId)
        {
            return View(programId);
        }

        // GET: Programs
        public async Task<IActionResult> Index()
        {
            var programMaster = await _context.ProgramMaster.ToListAsync();
            var organizations = await _context.OrganizerMaster.ToListAsync();
            var events = await _context.EventMaster.ToListAsync();
            var orgMapper = await _context.ProgramOrganizerMapper.ToListAsync();
            var eventMapper = await _context.ProgramEventMapper.ToListAsync();

            var model = new ProgramOrganizerViewModel()
            {
                ProgramList = programMaster,
                Organizations = organizations,                
                ProgramOrganizerMappers = orgMapper
            };

            return View(model);
        }

        // GET: Programs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = new ProgramOrganizerViewModel();

            //Get the program details from the program master based on the id
            var programMaster = await _context.ProgramMaster
                .FirstOrDefaultAsync(m => m.Id == id);

            //Going to the ProgramOrganizerMapper and getting all the organizers (id)
            //for that program and storing it in an integer array
            var orgArray = await _context.ProgramOrganizerMapper
                .Where(x => x.ProgramId == programMaster.Id)
                .Select(x => x.OrganizerId).ToArrayAsync();            

            model.Program = programMaster;

            //Look for the organization from the integer array and only
            //select those organizations
            model.Organizations = await _context.OrganizerMaster
                .Where(x => orgArray.Contains(x.Id)).ToListAsync();            

            if (programMaster == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Programs/Create
        public async Task<IActionResult> Create()
        {
            var model = new ProgramOrganizerViewModel()
            {
                Organizer = new OrganizerMaster(),
                Organizations = await _context.OrganizerMaster.ToListAsync()                
            };

            return View(model);
        }

        // POST: Programs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProgramOrganizerViewModel model)
        {
            var orgIds = Request.Form["orgIds"].ToString().Split(',');
            var program = model.Program;
            _context.Add(program);
            await _context.SaveChangesAsync();

            var programId = program.Id;

            foreach (var orgId in orgIds)
            {
                if (!string.IsNullOrEmpty(orgId))
                {
                    var organizationId = Convert.ToInt32(orgId);
                    var organizationMapper = new ProgramOrganizerMapper()
                    {
                        OrganizerId = organizationId,
                        ProgramId = programId
                    };
                    _context.Add(organizationMapper);
                    await _context.SaveChangesAsync();
                }

            }

            return RedirectToAction(nameof(Index));

        }

        // GET: Programs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = new ProgramOrganizerViewModel();
            if (id == null)
            {
                return NotFound();
            }

            var programMaster = await _context.ProgramMaster.FirstOrDefaultAsync(m => m.Id == id);
            var orgMaster = await _context.OrganizerMaster.ToListAsync();
            
            if (programMaster == null)
            {
                return NotFound();
            }

            var prgOrgMapper = await _context.ProgramOrganizerMapper.ToListAsync();
            var matchedOrg = prgOrgMapper.Where(x => x.ProgramId == programMaster.Id)
                .Select(x => x.OrganizerId);

            
            model.Program = programMaster;
            model.Organizations = orgMaster.ToList();
            model.ProgramOrganizations = orgMaster
                                  .Where(x => matchedOrg.Contains(x.Id))
                                  .ToList();
            model.ProgramOrganizerMappers = prgOrgMapper;


            return View(model);
        }

        // POST: Programs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProgramOrganizerViewModel model)
        {
            var orgIds = Request.Form["orgIds"];
            var program = model.Program;
            _context.Update(program);
            await _context.SaveChangesAsync();
            
            var programId = program.Id;
            var prgOrgMapper = await _context.ProgramOrganizerMapper
            .Where(x => x.ProgramId == programId).ToListAsync();
            _context.RemoveRange(prgOrgMapper);
            _context.SaveChanges();

            foreach (var orgId in orgIds)
            {
                if (!string.IsNullOrEmpty(orgId))
                {
                    var organizationId = Convert.ToInt32(orgId);
                    var organizationMapper = new ProgramOrganizerMapper()
                    {
                        OrganizerId = organizationId,
                        ProgramId = programId
                    };
                    _context.Update(organizationMapper);
                    await _context.SaveChangesAsync();
                }

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Programs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {     

            if (id == null)
            {
                return NotFound();
            }

            var programMaster = await _context.ProgramMaster
                .FirstOrDefaultAsync(m => m.Id == id);
            if (programMaster == null)
            {
                return NotFound();
            }

            return View(programMaster);
        }

        // POST: Programs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var programMaster = await _context.ProgramMaster.FindAsync(id);
            _context.ProgramMaster.Remove(programMaster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramMasterExists(int id)
        {
            return _context.ProgramMaster.Any(e => e.Id == id);
        }
    }
}

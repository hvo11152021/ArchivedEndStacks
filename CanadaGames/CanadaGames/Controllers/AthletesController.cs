using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CanadaGames.Data;
using CanadaGames.Models;

namespace CanadaGames.Controllers
{
    public class AthletesController : Controller
    {
        private readonly CanadaGamesContext _context;

        public AthletesController(CanadaGamesContext context)
        {
            _context = context;
        }

        // GET: Athletes
        public async Task<IActionResult> Index()
        {
            var canadaGamesContext = _context.Athletes.Include(a => a.Coach).Include(a => a.Contingent).Include(a => a.Gender).Include(a => a.Sport);
            return View(await canadaGamesContext.ToListAsync());
        }

        // GET: Athletes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athlete = await _context.Athletes
                .Include(a => a.Coach)
                .Include(a => a.Contingent)
                .Include(a => a.Gender)
                .Include(a => a.Sport)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (athlete == null)
            {
                return NotFound();
            }

            return View(athlete);
        }

        // GET: Athletes/Create
        public IActionResult Create()
        {
            ViewData["CoachID"] = new SelectList(_context.Coaches, "ID", "FirstName");
            ViewData["ContingentID"] = new SelectList(_context.Contingents, "ID", "Code");
            ViewData["GenderID"] = new SelectList(_context.Genders, "ID", "Code");
            ViewData["SportID"] = new SelectList(_context.Sports, "ID", "Code");
            return View();
        }

        // POST: Athletes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,MiddleName,LastName,AthleteCode,Hometown,DOB,Height,Weight,YearsInSport,Affiliation,Goals,Position,RoleModel,MedalInfo,ContingentID,SportID,GenderID,CoachID")] Athlete athlete)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(athlete);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Athelte.AthleteCode"))
                {
                    ModelState.AddModelError("AthleteCode", "Unable to save changes. You can't duplicate AthleteCode numbers.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }

            ViewDropDownList(athlete);
            return View(athlete);
        }

        // GET: Athletes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athlete = await _context.Athletes.FindAsync(id);
            if (athlete == null)
            {
                return NotFound();
            }
            ViewDropDownList(athlete);
            return View(athlete);
        }

        // POST: Athletes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var updateAthlete = await _context.Athletes.FirstOrDefaultAsync(a => a.ID == id);
            if (updateAthlete == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Athlete>(updateAthlete, "",
                a => a.FirstName, a => a.MiddleName, a => a.LastName, a => a.AthleteCode, a => a.Hometown, a => a.DOB,
                a => a.Height, a => a.Weight, a => a.YearsInSport, a => a.Affiliation, a => a.Goals, a => a.Position,
                a => a.RoleModel, a => a.MedalInfo, a => a.ContingentID, a => a.SportID, a => a.GenderID, a => a.CoachID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AthleteExists(updateAthlete.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException dex)
                {
                    if (dex.GetBaseException().Message.Contains("Unique constraint failed: Athelte.AthleteCode"))
                    {
                        ModelState.AddModelError("AthleteCode", "Failed to save changes. You can't duplicate AthleteCode numbers.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to save changes. Please contact your system administrator.");
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            ViewDropDownList(updateAthlete);
            return View(updateAthlete);
        }

        // GET: Athletes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athlete = await _context.Athletes
                .Include(a => a.Coach).AsNoTracking()
                .Include(a => a.Contingent).AsNoTracking()
                .Include(a => a.Gender).AsNoTracking()
                .Include(a => a.Sport).AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (athlete == null)
            {
                return NotFound();
            }

            return View(athlete);
        }

        // POST: Athletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var athlete = await _context.Athletes.FindAsync(id);
            try
            {
                _context.Athletes.Remove(athlete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Failed to delete record. Please contact your system administrator.");
            }
            return View(athlete);
        }

        private bool AthleteExists(int id)
        {
            return _context.Athletes.Any(e => e.ID == id);
        }

        private void ViewDropDownList(Athlete athlete)
        {
            ViewData["CoachID"] = new SelectList(_context.Coaches, "ID", "FirstName", athlete.CoachID);
            ViewData["ContingentID"] = new SelectList(_context.Contingents, "ID", "Code", athlete.ContingentID);
            ViewData["GenderID"] = new SelectList(_context.Genders, "ID", "Code", athlete.GenderID);
            ViewData["SportID"] = new SelectList(_context.Sports, "ID", "Code", athlete.SportID);
        }
    }
}

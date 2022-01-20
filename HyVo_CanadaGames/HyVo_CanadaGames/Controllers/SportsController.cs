using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HyVo_CanadaGames.Data;
using HyVo_CanadaGames.Models;
using HyVo_CanadaGames.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;
using HyVo_CanadaGames.Ultilities;

namespace HyVo_CanadaGames.Controllers
{
    public class SportsController : Controller
    {
        private readonly CanadaGamesContext _context;

        public SportsController(CanadaGamesContext context)
        {
            _context = context;
        }

        // GET: Sports
        public async Task<IActionResult> Index()
        {
            var sports = from s in _context.Sports
                .Include(s => s.AthleteSports).ThenInclude(a => a.Athlete)
                         select s;

            return View(sports);
        }

        // GET: Sports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sports = await _context.Sports
                .Include(s => s.AthleteSports).ThenInclude(a => a.Athlete)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (sports == null)
            {
                return NotFound();
            }

            return View(sports);
        }

        // GET: Sports/Create
        public IActionResult Create()
        {
            Sport sport = new Sport();
            PopulateAssignedAthleteData(sport);
            return View();
        }

        // POST: Sports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Code,Name")] Sport sport, string[] selectedOptions)
        {
            try
            {
                UpdateAthleteSports(selectedOptions, sport);
                if (ModelState.IsValid)
                {
                    _context.Add(sport);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { sport.ID });
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Please contact your system administrator.");
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Sport.SportCode"))
                {
                    ModelState.AddModelError("SportCode", "Unable to save changes. You can't duplicate SportCode numbers.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            PopulateAssignedAthleteData(sport);
            return View(sport);
        }

        // GET: Sports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sport = await _context.Sports
               .Include(d => d.AthleteSports)
               .ThenInclude(d => d.Athlete)
               .FirstOrDefaultAsync(d => d.ID == id);

            if (sport == null)
            {
                return NotFound();
            }
            PopulateAssignedAthleteData(sport);
            return View(sport);
        }

        // POST: Sports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Code,Name")] Sport sport, string[] selectedOptions)
        {
            var sportToUpdate = await _context.Sports
                 .Include(d => d.AthleteSports)
                 .ThenInclude(d => d.Athlete)
                 .FirstOrDefaultAsync(p => p.ID == id);

            if (sportToUpdate == null)
            {
                return NotFound();
            }

            UpdateAthleteSports(selectedOptions, sportToUpdate);

            if (await TryUpdateModelAsync<Sport>(sportToUpdate, "", s => s.Code, s => s.Name, s => s.Athletes, s => s.AthleteSports))
            {
                try
                {
                    _context.Update(sport);
                    await _context.SaveChangesAsync();
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Please contact your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportExists(sport.ID))
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
                    if (dex.GetBaseException().Message.Contains("Unique constraint failed: Athelte.SportCode"))
                    {
                        ModelState.AddModelError("SportCode", "Failed to save changes. You can't duplicate SportCode numbers.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to save changes. Please contact your system administrator.");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedAthleteData(sportToUpdate);
            return View(sportToUpdate);
        }

        // GET: Sports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sports = await _context.Sports
                .Include(s => s.AthleteSports).ThenInclude(a => a.Athlete)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (sports == null)
            {
                return NotFound();
            }

            return View(sports);
        }

        // POST: Sports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sports = await _context.Sports
                .Include(s => s.AthleteSports).ThenInclude(a => a.Athlete)
                .FirstOrDefaultAsync(s => s.ID == id);
            try
            {
                _context.Sports.Remove(sports);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Failed to delete record. Please contact your system administrator.");
            }
            return View(sports);
        }

        private void PopulateAssignedAthleteData(Sport sport)
        {
            var allOptions = _context.Athletes;
            var currentOptionsHS = new HashSet<int>(sport.AthleteSports.Select(a => a.AthleteID));
            var selected = new List<ListOptionVM>();
            var available = new List<ListOptionVM>();
            foreach (var s in allOptions)
            {
                if (currentOptionsHS.Contains(s.ID))
                {
                    selected.Add(new ListOptionVM { ID = s.ID, DisplayText = s.FullName });
                }
                else
                {
                    available.Add(new ListOptionVM { ID = s.ID, DisplayText = s.FullName });
                }
            }

            ViewData["selOpts"] = new MultiSelectList(selected.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availOpts"] = new MultiSelectList(available.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }

        private void UpdateAthleteSports(string[] selectedOptions, Sport sportToUpdate)
        {
            if (selectedOptions == null)
            {
                sportToUpdate.AthleteSports = new List<AthleteSport>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var currentOptionsHS = new HashSet<int>(sportToUpdate.AthleteSports.Select(b => b.AthleteID));
            foreach (var s in _context.Athletes)
            {
                if (selectedOptionsHS.Contains(s.ID.ToString()))
                {
                    if (!currentOptionsHS.Contains(s.ID))
                    {
                        sportToUpdate.AthleteSports.Add(new AthleteSport
                        {
                            AthleteID = s.ID,
                            SportID = sportToUpdate.ID
                        });
                    }
                }
                else
                {
                    if (currentOptionsHS.Contains(s.ID))
                    {
                        AthleteSport specToRemove = sportToUpdate.AthleteSports.FirstOrDefault(d => d.AthleteID == s.ID);
                        _context.Remove(specToRemove);
                    }
                }
            }
        }

        private bool SportExists(int id)
        {
            return _context.Sports.Any(e => e.ID == id);
        }
    }
}

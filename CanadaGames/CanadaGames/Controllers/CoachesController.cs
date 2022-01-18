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
    public class CoachesController : Controller
    {
        private readonly CanadaGamesContext _context;

        public CoachesController(CanadaGamesContext context)
        {
            _context = context;
        }

        // GET: Coaches
        public async Task<IActionResult> Index()
        {
            return View(await _context.Coaches.ToListAsync());
        }

        // GET: Coaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coaches
                .FirstOrDefaultAsync(m => m.ID == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        // GET: Coaches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,MiddleName,LastName")] Coach coach)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(coach);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Failed to save changes. Please contact your system administrator.");
            }
            return View(coach);
        }

        // GET: Coaches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var updateCoach = await _context.Coaches.SingleOrDefaultAsync(p => p.ID == id);

            if (updateCoach == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Coach>(updateCoach, "", c => c.FirstName, c => c.MiddleName, c => c.LastName))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachExists(updateCoach.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Failed to save changes. Please contact your system administrator.");
                }

            }
            return View(updateCoach);
        }

        // GET: Coaches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coaches.AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            try
            {
                _context.Coaches.Remove(coach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("Issues with Foreign Key constrains."))
                {
                    ModelState.AddModelError("", "Failed to delete coach. Can't delete coach with patients assigned.");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to save changes. Please contact your system administrator.");
                }
            }
            return View(coach);
        }

        private bool CoachExists(int id)
        {
            return _context.Coaches.Any(e => e.ID == id);
        }
    }
}

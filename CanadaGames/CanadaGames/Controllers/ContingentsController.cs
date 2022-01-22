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
    public class ContingentsController : Controller
    {
        private readonly CanadaGamesContext _context;

        public ContingentsController(CanadaGamesContext context)
        {
            _context = context;
        }

        // GET: Contingents
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Lookups");
        }

        // GET: Contingents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contingent = await _context.Contingents
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contingent == null)
            {
                return NotFound();
            }

            return View(contingent);
        }

        // GET: Contingents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contingents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Code,Name")] Contingent contingent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(contingent);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { contingent.ID });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(contingent);
        }

        // GET: Contingents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contingent = await _context.Contingents.FindAsync(id);
            if (contingent == null)
            {
                return NotFound();
            }
            return View(contingent);
        }

        // POST: Contingents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var contingentToUpdate = await _context.Contingents.SingleOrDefaultAsync(c => c.ID == id);

            if (contingentToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Contingent>(contingentToUpdate, "", c => c.Code, c => c.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { contingentToUpdate.ID });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContingentExists(contingentToUpdate.ID))
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
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(contingentToUpdate);
        }

        // GET: Contingents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contingent = await _context.Contingents
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contingent == null)
            {
                return NotFound();
            }

            return View(contingent);
        }

        // POST: Contingents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contingent = await _context.Contingents.FindAsync(id);
            try
            {
                _context.Contingents.Remove(contingent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(contingent);
        }

        private bool ContingentExists(int id)
        {
            return _context.Contingents.Any(e => e.ID == id);
        }
    }
}

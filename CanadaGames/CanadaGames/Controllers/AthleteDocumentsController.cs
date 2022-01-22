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
    public class AthleteDocumentsController : Controller
    {
        private readonly CanadaGamesContext _context;

        public AthleteDocumentsController(CanadaGamesContext context)
        {
            _context = context;
        }

        // GET: AthleteDocuments
        public async Task<IActionResult> Index()
        {
            var canadaGamesContext = _context.AthleteDocuments.Include(a => a.Athlete);
            return View(await canadaGamesContext.ToListAsync());
        }

        // GET: AthleteDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athleteDocument = await _context.AthleteDocuments
                .Include(a => a.Athlete)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (athleteDocument == null)
            {
                return NotFound();
            }

            return View(athleteDocument);
        }

        // GET: AthleteDocuments/Create
        public IActionResult Create()
        {
            ViewData["AthleteID"] = new SelectList(_context.Athletes, "ID", "Affiliation");
            return View();
        }

        // POST: AthleteDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AthleteID,ID,FileName")] AthleteDocument athleteDocument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(athleteDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AthleteID"] = new SelectList(_context.Athletes, "ID", "Affiliation", athleteDocument.AthleteID);
            return View(athleteDocument);
        }

        // GET: AthleteDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athleteDocument = await _context.AthleteDocuments.FindAsync(id);
            if (athleteDocument == null)
            {
                return NotFound();
            }
            ViewData["AthleteID"] = new SelectList(_context.Athletes, "ID", "Affiliation", athleteDocument.AthleteID);
            return View(athleteDocument);
        }

        // POST: AthleteDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AthleteID,ID,FileName")] AthleteDocument athleteDocument)
        {
            if (id != athleteDocument.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(athleteDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AthleteDocumentExists(athleteDocument.ID))
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
            ViewData["AthleteID"] = new SelectList(_context.Athletes, "ID", "Affiliation", athleteDocument.AthleteID);
            return View(athleteDocument);
        }

        // GET: AthleteDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athleteDocument = await _context.AthleteDocuments
                .Include(a => a.Athlete)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (athleteDocument == null)
            {
                return NotFound();
            }

            return View(athleteDocument);
        }

        // POST: AthleteDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var athleteDocument = await _context.AthleteDocuments.FindAsync(id);
            _context.AthleteDocuments.Remove(athleteDocument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AthleteDocumentExists(int id)
        {
            return _context.AthleteDocuments.Any(e => e.ID == id);
        }
    }
}

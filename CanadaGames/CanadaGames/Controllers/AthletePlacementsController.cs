using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CanadaGames.Data;
using CanadaGames.Models;
using CanadaGames.Utilities;

namespace CanadaGames.Controllers
{
    public class AthletePlacementsController : Controller
    {
        private readonly CanadaGamesContext _context;

        public AthletePlacementsController(CanadaGamesContext context)
        {
            _context = context;
        }

        // GET: PatientAppt
        public async Task<IActionResult> Index(int? AthleteID, int? SportID, int? page, int? pageSizeID, int? EventID, string actionButton,
            string SearchString, string sortDirection = "desc", string sortField = "Placement")
        {
            CookieHelper.CookieSet(HttpContext, ControllerName() + "URL", "", -1);

            PopulateDropDownLists();

            ViewData["Filtering"] = "btn-outline-dark";

            string[] sortOptions = new[] { "Event", "Placement" };

            var placement = from p in _context.Placements
                        .Include(p => p.Athlete)
                        .Include(p => p.Event).ThenInclude(p => p.Gender)
                        .Include(p => p.Event).ThenInclude(p => p.Sport)
                        where p.AthleteID == AthleteID.GetValueOrDefault()
                        select p;

            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Athletes");

            if (!AthleteID.HasValue)
            {
                return Redirect(ViewData["returnURL"].ToString());
            }
            if (SportID.HasValue)
            {
                placement = placement.Where(p => p.Event.SportID == SportID);
                ViewData["Filtering"] = "btn-danger";
            }
            if (EventID.HasValue)
            {
                placement = placement.Where(p => p.EventID == EventID);
                ViewData["Filtering"] = "btn-danger";
            }
            if (!String.IsNullOrEmpty(SearchString))
            {
                placement = placement.Where(p => p.Comments.ToUpper().Contains(SearchString.ToUpper()));
                ViewData["Filtering"] = "btn-danger";
            }
            if (!String.IsNullOrEmpty(actionButton))
            {
                page = 1;

                if (sortOptions.Contains(actionButton))
                {
                    if (actionButton == sortField)
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;
                }
            }
            if (sortField == "Sport")
            {
                if (sortDirection == "asc")
                {
                    placement = placement .OrderBy(p => p.Event.Sport.Name);
                }
                else
                {
                    placement = placement .OrderByDescending(p => p.Event.Sport.Name);
                }
            }
            if (sortField == "Event")
            {
                if (sortDirection == "asc")
                {
                    placement = placement.OrderBy(p => p.Event.Name);
                }
                else
                {
                    placement = placement.OrderByDescending(p => p.Event.Name);
                }
            }
            else
            {
                if (sortDirection == "asc")
                {
                    placement = placement.OrderByDescending(p => p.Place);
                }
                else
                {
                    placement = placement.OrderBy(p => p.Place);
                }
            }
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            Athlete athlete = _context.Athletes
                .Include(a => a.Coach)
                .Include(a => a.Gender)
                .Include(a => a.AthleteThumbnail)
                .Include(a => a.AthleteSports).ThenInclude(a => a.Sport)
                .Where(a => a.ID == AthleteID.GetValueOrDefault()).FirstOrDefault();
            ViewBag.Athlete = athlete;

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID);
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);

            var pagedData = await PaginatedList<Placement>.CreateAsync(placement.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: AthletePlacements/Details/5
        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placement = await _context.Placements
                .Include(p => p.Athlete)
                .Include(p => p.Event)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (placement == null)
            {
                return NotFound();
            }

            return View(placement);
        }*/

        // GET: AthletePlacements/Create
        public IActionResult Create(int? AthleteID, string AthleteName)
        {
            //Get the URL with the last filter, sort and page parameters
            ViewDataReturnURL();

            if (!AthleteID.HasValue)
            {
                return Redirect(ViewData["returnURL"].ToString());
            }

            ViewData["AthleteName"] = AthleteName;
            Placement p = new Placement()
            {
                AthleteID = AthleteID.GetValueOrDefault()
            };
            PopulateDropDownLists();
            return View(p);
        }

        // POST: AthletePlacements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Place,Comments,EventID,AthleteID")] Placement placement, string AthleteName)
        {
            ViewDataReturnURL();

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(placement);
                    await _context.SaveChangesAsync();
                    return Redirect(ViewData["returnURL"].ToString());
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            PopulateDropDownLists(placement);
            ViewData["AthleteName"] = AthleteName;
            return View(placement);
        }

        // GET: AthletePlacements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewDataReturnURL();

            var placement = await _context.Placements
                .Include(p => p.Event)
                .Include(p => p.Athlete)
                .FirstOrDefaultAsync(p => p.ID == id);
            if (placement == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(placement);
            return View(placement);
        }

        // POST: AthletePlacements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            ViewDataReturnURL();

            var placementToUpdate = await _context.Placements
                .Include(p => p.Event)
                .Include(p => p.Athlete)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (placementToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Placement>(placementToUpdate, "",
                p => p.Place, p => p.Comments, p => p.EventID, p => p.AthleteID))
            {
                try
                {
                    _context.Update(placementToUpdate);
                    await _context.SaveChangesAsync();
                    return Redirect(ViewData["returnURL"].ToString());
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!PlacementExists(placementToUpdate.ID))
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
            PopulateDropDownLists(placementToUpdate);
            return View(placementToUpdate);
        }

        // GET: AthletePlacements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewDataReturnURL();

            var placement = await _context.Placements
                .Include(p => p.Athlete)
                .Include(p => p.Event)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (placement == null)
            {
                return NotFound();
            }

            return View(placement);
        }

        // POST: AthletePlacements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var placement = await _context.Placements
                .Include(p => p.Event)
                .Include(p => p.Athlete)
                .FirstOrDefaultAsync(p => p.ID == id);

            ViewDataReturnURL();

            try
            {
                _context.Placements.Remove(placement);
                await _context.SaveChangesAsync();
                return Redirect(ViewData["returnURL"].ToString());
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(placement);
        }

        private SelectList SportSelectList(int? id)
        {
            var sQuery = from s in _context.Sports
                         orderby s.Name
                         select s;
            return new SelectList(sQuery, "ID", "Name", id);
        }

        private SelectList AthleteSelectList(int? id)
        {
            var aQuery = from a in _context.Athletes
                         orderby a.FirstName
                         select a;
            return new SelectList(aQuery, "ID", "FirstName", id);
        }

        private SelectList EventSelectList(int? id)
        {
            var eQuery = from e in _context.Events
                         orderby e.Name
                         select e;
            return new SelectList(eQuery, "ID", "Name", id);
        }

        private void PopulateDropDownLists(Placement placement = null)
        {
            ViewData["SportID"] = SportSelectList(placement?.Event.SportID);
            ViewData["AthleteID"] = AthleteSelectList(placement?.AthleteID);
            ViewData["EventID"] = EventSelectList(placement?.EventID);
        }
        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }
        private void ViewDataReturnURL()
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, ControllerName());
        }

        private bool PlacementExists(int id)
        {
            return _context.Placements.Any(e => e.ID == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HyVo_CanadaGames.Data;
using HyVo_CanadaGames.Models;
using HyVo_CanadaGames.Ultilities;
using HyVo_CanadaGames.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;

namespace HyVo_CanadaGames.Controllers
{
    public class AthletesController : Controller
    {
        private readonly CanadaGamesContext _context;

        public AthletesController(CanadaGamesContext context)
        {
            _context = context;
        }

        // GET: Athletes
        public async Task<IActionResult> Index(int? SportID, int? GenderID, int? ContingentID, int? CoachID, int? page, int? pageAmountID, string FindName, string FindMedia, string actionButton, string sortDirection = "asc", string sortField = "Athlete")
        {
            ViewData["Filtering"] = "";
            string[] sortOptions = new[] { "Full Name", "Age", "Contingent", "Sport" };

            ViewDropDownList();

            //Linq query
            var athletes = from a in _context.Athletes
                .Include(a => a.Coach)
                .Include(a => a.Contingent)
                .Include(a => a.Gender)
                .Include(a => a.Sport)
                .Include(s => s.AthleteSports).ThenInclude(s => s.Sport)
                          select a;

            //Add filter
            if (CoachID.HasValue)
            {
                athletes = athletes.Where(a => a.CoachID == CoachID);
                ViewData["Filtering"] = " show";
            }
            if (ContingentID.HasValue)
            {
                athletes = athletes.Where(a => a.ContingentID == ContingentID);
                ViewData["Filtering"] = " show";
            }
            if (GenderID.HasValue)
            {
                athletes = athletes.Where(a => a.GenderID == GenderID);
                ViewData["Filtering"] = " show";
            }
            if (SportID.HasValue)
            {
                athletes = athletes.Where(a => a.SportID == SportID);
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(FindName))
            {
                athletes = athletes.Where(a => a.LastName.ToUpper().Contains(FindName.ToUpper())
                                       || a.FirstName.ToUpper().Contains(FindName.ToUpper()));
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(FindMedia))
            {
                athletes = athletes.Where(a => a.MediaInfo.ToUpper().Contains(FindMedia.ToUpper()));
                ViewData["Filtering"] = " show";
            }

            //Configure ActionButton
            if (!String.IsNullOrEmpty(actionButton))
            {
                page = 1; //Reset page
                if (actionButton != "Filter")
                {
                    if (actionButton == sortField)
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;
                }
            }

            //Sort direction
            athletes = (sortField == "Age") ? (athletes = sortDirection == "asc" ? athletes.OrderByDescending(a => a.DOB) : athletes.OrderBy(a => a.DOB)) :
                       (sortField == "Contingent") ? (athletes = sortDirection == "asc" ? athletes.OrderByDescending(a => a.Contingent) : athletes.OrderBy(a => a.Contingent)) :
                       (sortField == "Sport") ? (athletes = sortDirection == "asc" ? athletes.OrderByDescending(a => a.Sport) : athletes.OrderBy(a => a.Sport)) :
                       (athletes = sortDirection == "asc" ? athletes.OrderBy(a => a.LastName).ThenBy(a => a.FirstName) : athletes.OrderByDescending(a => a.LastName).ThenByDescending(a => a.FirstName));

            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            int pageAmount = PageSizeHelper.SetPageSize(HttpContext, pageAmountID);
            ViewData["pageAmountID"] = PageSizeHelper.PageSizeList(pageAmount);
            var pagedData = await PaginatedList<Athlete>.CreateAsync(athletes.AsNoTracking(), page ?? 1, pageAmount);

            return View(pagedData);
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
                .Include(a => a.AthleteSports).ThenInclude(s => s.Sport)
                .FirstOrDefaultAsync(a => a.ID == id);
            if (athlete == null)
            {
                return NotFound();
            }

            return View(athlete);
        }

        // GET: Athletes/Create
        public IActionResult Create()
        {
            var athlete = new Athlete();
            PopulateAssignedSportData(athlete);
            ViewDropDownList();
            return View();
        }

        // POST: Athletes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,MiddleName,LastName,AthleteCode,Hometown,DOB,Height,Weight,YearsInSport,Affiliation,Goals,Position,RoleModel,MedalInfo,ContingentID,SportID,GenderID,CoachID")] Athlete athlete, string[] selectedOptions)
        {
            try
            {
                if (selectedOptions != null)
                {
                    foreach (var condition in selectedOptions)
                    {
                        var conditionToAdd = new AthleteSport { AthleteID = athlete.ID, SportID = int.Parse(condition) };
                        athlete.AthleteSports.Add(conditionToAdd);
                    }
                }
                if (ModelState.IsValid)
                {
                    _context.Add(athlete);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see contact system administrator.");
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

            PopulateAssignedSportData(athlete);
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

            var athlete = await _context.Athletes
                .Include(a => a.AthleteSports).ThenInclude(p => p.Sport)
                .FirstOrDefaultAsync(a => a.ID == id);
            if (athlete == null)
            {
                return NotFound();
            }

            PopulateAssignedSportData(athlete);
            ViewDropDownList(athlete);
            return View(athlete);
        }

        // POST: Athletes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string[] selectedOptions, Byte[] RowVersion)
        {
            var updateAthlete = await _context.Athletes
                .Include(a => a.AthleteSports).ThenInclude(a => a.Sport)
                .FirstOrDefaultAsync(a => a.ID == id);
            if (updateAthlete == null)
            {
                return NotFound();
            }

            //update history
            UpdateAthleteSports(selectedOptions, updateAthlete);

            //_context.Entry(updateAthlete).Property("RowVersion").OriginalValue = RowVersion;

            if (await TryUpdateModelAsync<Athlete>(updateAthlete, "", a => a.FirstName, a => a.MiddleName, a => a.LastName, a => a.AthleteCode, a => a.Hometown, a => a.DOB, a => a.Height, a => a.Weight, a => a.YearsInSport, a => a.Affiliation, a => a.Goals, a => a.MediaInfo, a => a.ContingentID, a => a.SportID, a => a.GenderID, a => a.CoachID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see contact system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AthleteExists(updateAthlete.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "The record you editing "
                            + "was modified by another user. Please try again.");
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

            PopulateAssignedSportData(updateAthlete);
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
                .Include(a => a.Coach)
                .Include(a => a.Contingent)
                .Include(a => a.Gender)
                .Include(a => a.Sport)
                .Include(a => a.AthleteSports).ThenInclude(s => s.Sport)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ID == id);
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
            var athlete = await _context.Athletes
                .Include(a => a.Coach).AsNoTracking()
                .Include(a => a.Contingent).AsNoTracking()
                .Include(a => a.Gender).AsNoTracking()
                .Include(a => a.Sport).AsNoTracking()
                .Include(a => a.AthleteSports).ThenInclude(s => s.Sport)
                .FirstOrDefaultAsync(a => a.ID == id);
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

        ///////////////////////////////////////////////////////////////////////

        private void PopulateAssignedSportData(Athlete athlete)
        {
            var allOptions = _context.Sports;
            var currentOptionIDs = new HashSet<int>(athlete.AthleteSports.Select(b => b.SportID));
            var checkBoxes = new List<CheckOptionVM>();
            foreach (var option in allOptions)
            {
                checkBoxes.Add(new CheckOptionVM
                {
                    ID = option.ID,
                    DisplayText = option.Name,
                    Assigned = currentOptionIDs.Contains(option.ID)
                });
            }
            ViewData["ConditionOptions"] = checkBoxes;
        }

        private void UpdateAthleteSports(string[] selectedOptions, Athlete athleteToUpdate)
        {
            if (selectedOptions == null)
            {
                athleteToUpdate.AthleteSports = new List<AthleteSport>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var athleteOptionsHS = new HashSet<int>
                (athleteToUpdate.AthleteSports.Select(c => c.SportID));
            foreach (var option in _context.Sports)
            {
                if (selectedOptionsHS.Contains(option.ID.ToString())) 
                {
                    if (!athleteOptionsHS.Contains(option.ID))
                    {
                        athleteToUpdate.AthleteSports.Add(new AthleteSport { AthleteID = athleteToUpdate.ID, SportID = option.ID });
                    }
                }
                else
                {
                    if (athleteOptionsHS.Contains(option.ID))
                    {
                        AthleteSport conditionToRemove = athleteToUpdate.AthleteSports.SingleOrDefault(c => c.SportID == option.ID);
                        _context.Remove(conditionToRemove);
                    }
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////

        private SelectList CoachList(int? selectedID)
        {
            return new SelectList(_context.Coaches
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName), "ID", "FormalName", selectedID);
        }

        private SelectList ContingentList(int? selectedID)
        {
            return new SelectList(_context.Contingents, "ID", "Code", selectedID);
        }

        private SelectList GenderList(int? selectedID)
        {
            return new SelectList(_context.Genders, "ID", "Code", selectedID);
        }

        private SelectList SportList(int? selectedID)
        {
            return new SelectList(_context.Sports, "ID", "Code", selectedID);
        }

        ///////////////////////////////////////////////////////////////////////

        private bool AthleteExists(int id)
        {
            return _context.Athletes.Any(e => e.ID == id);
        }

        private void ViewDropDownList(Athlete athlete = null)
        {
            ViewData["CoachID"] = CoachList(athlete?.CoachID);
            ViewData["ContingentID"] = ContingentList(athlete?.ContingentID);
            ViewData["GenderID"] = GenderList(athlete?.GenderID);
            ViewData["SportID"] = SportList(athlete?.SportID);
        }
    }
}

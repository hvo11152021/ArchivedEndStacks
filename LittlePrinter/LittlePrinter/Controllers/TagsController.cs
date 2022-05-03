using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LittlePrinter.Models;
using LittlePrinter.Data;

namespace LittlePrinter.Controllers
{
    public class TagsController : Controller
    {
        private readonly LittlePrinterContext _context;

        public TagsController(LittlePrinterContext context)
        {
            _context = context;
        }

        // GET: Tags
        public async Task<IActionResult> Index(string SearchString, int? page, int? pageSizeID, string actionButton, string sortDirection = "asc", string sortField = "Style PPJ")
        {
            var cartons = from c in _context.Tags
                          select c;

            //Clear the sort/filter/paging URL Cookie for Controller
            CookieHelper.CookieSet(HttpContext, ControllerName() + "URL", "", -1);

            //Toggle the Open/Closed state of the collapse depending on if we are filtering
            ViewData["Filtering"] = ""; //Asume not filtering
            //Then in each "test" for filtering, add ViewData["Filtering"] = " show" if true;

            //NOTE: make sure this array has matching values to the column headings
            string[] sortOptions = new[] { "Style PPJ", "Brand" };

            if (!String.IsNullOrEmpty(SearchString))
            {
                cartons = cartons.Where(p => p.StylePPJ.ToUpper().Contains(SearchString.ToUpper())
                                       || p.Brand.ToUpper().Contains(SearchString.ToUpper()));
                ViewData["Filtering"] = " show";
            }

            //Before we sort, see if we have called for a change of filtering or sorting
            if (!String.IsNullOrEmpty(actionButton)) //Form Submitted!
            {
                page = 1;//Reset page to start

                if (sortOptions.Contains(actionButton))//Change of sort is requested
                {
                    //Reverse order on same field
                    if (actionButton == sortField) sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    sortField = actionButton;//Sort by the button clicked
                }
            }
            //Now we know which field and direction to sort by
            if (sortField == "Brand")
            {
                cartons = sortDirection == "asc" ? cartons.OrderBy(p => p.Brand).ThenBy(p => p.StylePPJ) : cartons = cartons.OrderByDescending(p => p.Brand).ThenByDescending(p => p.StylePPJ);
            }
            else //Sorting by Last Name
            {
                cartons = sortDirection == "asc" ? cartons.OrderBy(p => p.StylePPJ).ThenBy(p => p.Brand) : cartons.OrderByDescending(p => p.StylePPJ).ThenByDescending(p => p.Brand);
            }
            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID);
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);

            var pagedData = await PaginatedList<Tag>.CreateAsync(cartons.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            if (id == null || _context.Tags == null) NotFound();

            var cartonLabel = await _context.Tags.FirstOrDefaultAsync(m => m.ID == id);

            if (cartonLabel == null) NotFound();

            List<string> names = new List<string>();
            List<int> values = new List<int>();

            void SetNamesAndValues(string n, int v)
            {
                names.Add(n);
                values.Add(v);
            }

            if (cartonLabel.Col000 != 0) SetNamesAndValues("000", cartonLabel.Col000);
            if (cartonLabel.Col00 != 0) SetNamesAndValues("00", cartonLabel.Col00);
            for (int i = 0; i <= 60; i++)
            {
                var prop = typeof(Tag).GetProperty($"Col{i}");
                int objInt = (int)prop.GetValue(cartonLabel, null);
                if (objInt != 0)
                {
                    names.Add($"{i}");
                    values.Add(objInt);
                }
            }

            var sizes = new List<(string n, int v)>{
                ("XXS", cartonLabel.Size2XS),
                ("XS", cartonLabel.SizeXS),
                ("S", cartonLabel.SizeS),
                ("M", cartonLabel.SizeM),
                ("L", cartonLabel.SizeL),
                ("XL", cartonLabel.SizeXL),
                ("XXL", cartonLabel.Size2XL),
                ("3XL", cartonLabel.Size3XL),
                ("X1", cartonLabel.SizeX1),
                ("X2", cartonLabel.SizeX2),
                ("X3", cartonLabel.SizeX3),
                ("LL", cartonLabel.SizeLL),
                ("3L", cartonLabel.Size3L),
                ("4L", cartonLabel.Size4L),
                ("5L", cartonLabel.Size5L)
            };

            foreach (var size in sizes)
            {
                if (size.v != 0) SetNamesAndValues(size.n, size.v);
            }

            ViewData["Names"] = names;
            ViewData["Values"] = values;

            return View(cartonLabel);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            ViewDataReturnURL();
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CartonNumber,BuyerCartonNumber, StylePPJ,Brand,Description,Fab,ColorName," +
            "Col000,Col00,Col0,Col1,Col2,Col3,Col4,Col5,Col6,Col7,Col8,Col9,Col10," +
            "Col11,Col12,Col13,Col14,Col15,Col16,Col17,Col18,Col19,Col20," +
            "Col21,Col22,Col23,Col24,Col25,Col26,Col27,Col28,Col29,Col30," +
            "Col31,Col32,Col33,Col34,Col35,Col36,Col37,Col38,Col39,Col40," +
            "Col41,Col42,Col43,Col44,Col45,Col46,Col47,Col48,Col49,Col50," +
            "Col51,Col52,Col53,Col54,Col55,Col56,Col57,Col58,Col59,Col60," +
            "Size2XS,SizeXS,SizeS,SizeM,SizeL,SizeXL,Size2XL,Size3XL,SizeX1,SizeX2,SizeX3,SizeLL,Size3L,Size4L,Size5L," +
            "TotalQuantity,CartonQuantity,TotalPieces,TotalNetWeight,TotalGrossWeight,Dimension")] Tag cartonLabel)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            if (ModelState.IsValid)
            {
                _context.Add(cartonLabel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cartonLabel);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            if (id == null || _context.Tags == null) NotFound();

            var cartonLabel = await _context.Tags.FindAsync(id);
            if (cartonLabel == null) NotFound();
            return View(cartonLabel);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            var labelToUpdate = await _context.Tags.SingleOrDefaultAsync(l => l.ID == id);

            if (labelToUpdate == null) NotFound();

            if (await TryUpdateModelAsync(labelToUpdate, "", l => l.CartonNumber, l => l.BuyerCartonNumber, l => l.StylePPJ, l => l.Brand, l => l.Description, l => l.Fab, l => l.ColorName,
                l => l.Col00, l => l.Col0, l => l.Col1, l => l.Col2, l => l.Col3, l => l.Col4, l => l.Col5, l => l.Col6, l => l.Col7, l => l.Col8, l => l.Col9, l => l.Col10,
                l => l.Col11, l => l.Col12, l => l.Col13, l => l.Col14, l => l.Col15, l => l.Col16, l => l.Col17, l => l.Col18, l => l.Col19, l => l.Col20,
                l => l.Col21, l => l.Col22, l => l.Col23, l => l.Col24, l => l.Col25, l => l.Col26, l => l.Col27, l => l.Col28, l => l.Col29, l => l.Col30,
                l => l.Col31, l => l.Col32, l => l.Col33, l => l.Col34, l => l.Col35, l => l.Col36, l => l.Col37, l => l.Col38, l => l.Col39, l => l.Col40,
                l => l.Col41, l => l.Col42, l => l.Col43, l => l.Col44, l => l.Col45, l => l.Col46, l => l.Col47, l => l.Col48, l => l.Col49, l => l.Col50,
                l => l.Col51, l => l.Col52, l => l.Col53, l => l.Col54, l => l.Col55, l => l.Col56, l => l.Col57, l => l.Col58, l => l.Col59, l => l.Col60,
                l => l.Size2XS, l => l.SizeXS, l => l.SizeS, l => l.SizeM, l => l.SizeL, l => l.SizeXL, l => l.Size2XL, l => l.Size3XL,
                l => l.SizeX1, l => l.SizeX2, l => l.SizeX3, l => l.SizeLL, l => l.Size3L, l => l.Size4L, l => l.Size5L,
                l => l.TotalQuantity, l => l.TotalPieces, l => l.TotalNetWeight, l => l.TotalGrossWeight, l => l.Dimension))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(labelToUpdate.ID))
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
            return View(labelToUpdate);
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            if (id == null || _context.Tags == null) NotFound();

            var cartonLabel = await _context.Tags.FirstOrDefaultAsync(m => m.ID == id);

            if (cartonLabel == null) NotFound();

            List<string> names = new List<string>();
            List<int> values = new List<int>();
            if (cartonLabel.Col000 != 0)
            {
                names.Add("000");
                values.Add(cartonLabel.Col000);
            }
            if (cartonLabel.Col00 != 0)
            {
                names.Add("00");
                values.Add(cartonLabel.Col00);
            }
            for (int i = 0; i <= 60; i++)
            {
                var prop = typeof(Tag).GetProperty($"Col{i}");
                int objInt = (int)prop.GetValue(cartonLabel, null);
                if (objInt != 0)
                {
                    names.Add($"{i}");
                    values.Add(objInt);
                }
            }
            if (cartonLabel.Size2XS != 0)
            {
                names.Add("XXS");
                values.Add(cartonLabel.Size2XS);
            }
            if (cartonLabel.SizeXS != 0)
            {
                names.Add("XS");
                values.Add(cartonLabel.Size2XS);
            }
            if (cartonLabel.SizeS != 0)
            {
                names.Add("S");
                values.Add(cartonLabel.SizeS);
            }
            if (cartonLabel.SizeM != 0)
            {
                names.Add("M");
                values.Add(cartonLabel.SizeM);
            }
            if (cartonLabel.SizeL != 0)
            {
                names.Add("L");
                values.Add(cartonLabel.SizeL);
            }
            if (cartonLabel.SizeXL != 0)
            {
                names.Add("XL");
                values.Add(cartonLabel.SizeXL);
            }
            if (cartonLabel.Size2XL != 0)
            {
                names.Add("XXL");
                values.Add(cartonLabel.Size2XL);
            }
            if (cartonLabel.Size3XL != 0)
            {
                names.Add("3XL");
                values.Add(cartonLabel.Size3XL);
            }
            if (cartonLabel.SizeX1 != 0)
            {
                names.Add("X1");
                values.Add(cartonLabel.SizeX1);
            }
            if (cartonLabel.SizeX2 != 0)
            {
                names.Add("X2");
                values.Add(cartonLabel.SizeX2);
            }
            if (cartonLabel.SizeX3 != 0)
            {
                names.Add("X3");
                values.Add(cartonLabel.SizeX3);
            }
            if (cartonLabel.SizeLL != 0)
            {
                names.Add("LL");
                values.Add(cartonLabel.SizeLL);
            }
            if (cartonLabel.Size3L != 0)
            {
                names.Add("3L");
                values.Add(cartonLabel.Size3L);
            }
            if (cartonLabel.Size4L != 0)
            {
                names.Add("4L");
                values.Add(cartonLabel.Size4L);
            }
            if (cartonLabel.Size5L != 0)
            {
                names.Add("5L");
                values.Add(cartonLabel.Size5L);
            }

            ViewData["Names"] = names;
            ViewData["Values"] = values;

            return View(cartonLabel);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            if (_context.Tags == null)
            {
                return Problem("Entity set 'ShippingContext.CartonLabels'  is null.");
            }
            var cartonLabel = await _context.Tags.FindAsync(id);
            if (cartonLabel != null)
            {
                _context.Tags.Remove(cartonLabel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private int ConvertInput(string input)
        {
            if (input == null || input == "")
                return 0;
            else
                return int.Parse(input);
        }

        private double ConvertDouble(string input)
        {
            if (input == null || input == "")
                return 0;
            else
                return double.Parse(input);
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.ID == id);
        }

        private string ControllerName() => this.ControllerContext.RouteData.Values["controller"].ToString();

        private void ViewDataReturnURL()
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, ControllerName());
        }
    }
}

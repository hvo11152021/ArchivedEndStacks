using CanadaGames.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CanadaGames.Controllers
{
    public class LookupsController : Controller
    {
        private readonly CanadaGamesContext _context;

        public LookupsController(CanadaGamesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["ContingentsID"] = new SelectList(_context.Contingents.OrderBy(c => c.Name), "ID", "Name");
            ViewData["HometownsID"] = new SelectList(_context.Hometowns.OrderBy(h => h.Name), "ID", "Name");
            ViewData["GendersID"] = new SelectList(_context.Genders.OrderBy(g => g.Name), "ID", "Name");
            return View();
        }
    }
}
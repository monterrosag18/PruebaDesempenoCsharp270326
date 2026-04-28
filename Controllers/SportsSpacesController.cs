using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsComplex.Data;
using SportsComplex.Models;
using System.Linq;
using System.Collections.Generic;

namespace SportsComplex.Controllers {
    public class SportsSpacesController : Controller {
        private readonly ApplicationDbContext _context;

        public SportsSpacesController(ApplicationDbContext context) {
            _context = context;
        }

        public IActionResult Index(string searchType) {
            var query = _context.SportsSpaces.AsQueryable();
            
            if (!string.IsNullOrEmpty(searchType)) {
                query = query.Where(s => s.SpaceType.Contains(searchType));
            }
            
            ViewBag.CurrentFilter = searchType;
            return View(query.ToList());
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SportsSpace space) {
            if (ModelState.IsValid) {
                try {
                    _context.SportsSpaces.Add(space);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                } catch {
                    ModelState.AddModelError("", "Error al guardar. Verifica que el nombre no esté duplicado.");
                }
            }
            return View(space);
        }

        public IActionResult Edit(int id) {
            var space = _context.SportsSpaces.Find(id);
            if (space == null) return NotFound();
            return View(space);
        }

        [HttpPost]
        public IActionResult Edit(SportsSpace space) {
            if (ModelState.IsValid) {
                try {
                    _context.SportsSpaces.Update(space);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                } catch {
                    ModelState.AddModelError("", "Error al actualizar. Verifica que el nombre no esté duplicado.");
                }
            }
            return View(space);
        }
    }
}

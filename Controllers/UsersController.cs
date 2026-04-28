using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsComplex.Data;
using SportsComplex.Models;
using System.Linq;

namespace SportsComplex.Controllers {
    public class UsersController : Controller {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context) {
            _context = context;
        }

        public IActionResult Index() {
            return View(_context.Users.ToList());
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user) {
            if (ModelState.IsValid) {
                try {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                } catch {
                    ModelState.AddModelError("", "Error al guardar. Verifica que el documento o email no estén duplicados.");
                }
            }
            return View(user);
        }

        public IActionResult Edit(int id) {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user) {
            if (ModelState.IsValid) {
                try {
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                } catch {
                    ModelState.AddModelError("", "Error al actualizar. Verifica que el documento o email no estén duplicados por otro usuario.");
                }
            }
            return View(user);
        }
    }
}

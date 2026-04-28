using Microsoft.AspNetCore.Mvc;
using SportsComplex.Models;
using SportsComplex.Services;
using SportsComplex.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;

namespace SportsComplex.Controllers {
    public class ReservationsController : Controller {
        private readonly ReservationService _service;
        private readonly ApplicationDbContext _context;

        public ReservationsController(ReservationService service, ApplicationDbContext context) {
            _service = service; 
            _context = context;
        }

        public IActionResult Index(int? userId, int? spaceId) {
            // OPTIMIZACIÓN: Usamos IQueryable para filtrar en la BASE DE DATOS, no en la memoria.
            var query = _context.Reservations
                .Include(r => r.User)
                .Include(r => r.SportsSpace)
                .AsQueryable();
            
            if (userId.HasValue) {
                query = query.Where(r => r.UserId == userId);
            }
            if (spaceId.HasValue) {
                query = query.Where(r => r.SportsSpaceId == spaceId);
            }
            
            // Ejecutamos la consulta una sola vez
            var result = query.ToList();

            // Estadísticas basadas en el resultado filtrado
            var stats = new Dictionary<string, int> {
                { "Total", result.Count },
                { "Canceled", result.Count(r => r.State == "Canceled") }
            };
            
            ViewBag.Statistics = stats;

            return View(result);
        }

        public IActionResult Create() {
            ViewBag.Users = _context.Users.ToList();
            ViewBag.Spaces = _context.SportsSpaces.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create(Reservation r) {
            try {
                _service.CreateReservation(r);
                return RedirectToAction("Index");
            } catch (Exception ex) {
               
                ModelState.AddModelError("", ex.Message);
                
             
                ViewBag.Users = _context.Users.ToList();
                ViewBag.Spaces = _context.SportsSpaces.ToList();
                return View(r);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id) {
            _service.CancelReservation(id);
            return RedirectToAction("Index");
        }
    }
}
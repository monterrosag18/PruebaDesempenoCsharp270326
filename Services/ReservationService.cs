using System;
using System.Linq;
using SportsComplex.Data;
using SportsComplex.Models;

namespace SportsComplex.Services
{
    public class ReservationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _email;

        public ReservationService(ApplicationDbContext context, IEmailService email)
        {
            _context = context;
            _email = email;
        }

        public void CreateReservation(Reservation r)
        {
            try
            {
                
                if (r.EndTime <= r.StartTime) 
                    throw new Exception("End time must be greater than Start time");
                
                if (r.ReservationDate < DateTime.Now.Date) 
                    throw new Exception("You cannot reserve in the past");
                
                if (r.ReservationDate == DateTime.Now.Date && r.StartTime < DateTime.Now.TimeOfDay) 
                    throw new Exception("That time has already passed today");

                
                bool spaceOccupied = _context.Reservations.Any(res =>
                    res.SportsSpaceId == r.SportsSpaceId &&
                    res.ReservationDate == r.ReservationDate &&
                    res.State != "Canceled" &&
                    r.StartTime < res.EndTime && r.EndTime > res.StartTime
                );

                if (spaceOccupied) 
                    throw new Exception("The sports space is already occupied at that time");

               
                bool userOccupied = _context.Reservations.Any(res =>
                    res.UserId == r.UserId &&
                    res.ReservationDate == r.ReservationDate &&
                    res.State != "Canceled" &&
                    r.StartTime < res.EndTime && r.EndTime > res.StartTime
                );

                if (userOccupied) 
                    throw new Exception("The user already has another reservation at that time");

                
                r.State = "Active";
                _context.Reservations.Add(r);
                _context.SaveChanges();

              
                var usr = _context.Users.Find(r.UserId);
                if (usr != null)
                {
                    _email.Send(usr.Email, "Reservation Confirmed", $"Successful reservation on {r.ReservationDate.ToShortDateString()}");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void CancelReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation != null)
            {
                reservation.State = "Canceled";
                _context.SaveChanges();
            }
        }
    }
}
using Microsoft.EntityFrameworkCore;
using SportsComplex.Models;

namespace SportsComplex.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        
        public DbSet<User> Users { get; set; }
        public DbSet<SportsSpace> SportsSpaces { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
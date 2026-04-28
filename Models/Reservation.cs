using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsComplex.Models {
    [Table("Reservations")]
    public class Reservation {
        [Key] public int Id { get; set; }
        
        [Required] public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = null!; 
        
        [Required] public int SportsSpaceId { get; set; }
        [ForeignKey("SportsSpaceId")]
        public SportsSpace SportsSpace { get; set; } = null!; 
        
        [Required] public DateTime ReservationDate { get; set; }
        [Required] public TimeSpan StartTime { get; set; }
        [Required] public TimeSpan EndTime { get; set; }
        public string State { get; set; } = "Active";
    }
}
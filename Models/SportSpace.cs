using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsComplex.Models {
    [Table("SportsSpaces")]
    public class SportsSpace {
        [Key] public int Id { get; set; }
        
       
        [Required] public required string Name { get; set; } 
        [Required] public required string SpaceType { get; set; } 
        
        [Required] public int Capacity { get; set; }
    }
}
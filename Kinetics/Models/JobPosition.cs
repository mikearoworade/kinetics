using System.ComponentModel.DataAnnotations;

namespace Kinetics.Models
{
    public class JobPosition
    {
        public int JobPositionID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Position name cannot exceed 100 characters.")]
        [Display(Name ="Position Name")]
        public string PositionName { get; set; }

        // Navigation Property
        public ICollection<Employee> Employees { get; set; }
    }
}

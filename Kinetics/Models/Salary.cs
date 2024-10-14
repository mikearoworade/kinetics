using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kinetics.Models
{
    public class Salary
    {
        public int SalaryID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]  // Specify the precision and scale
        [Range(0, double.MaxValue, ErrorMessage = "Gross salary must be a positive number")]
        public decimal GrossSalary { get; set; }

        [Required]
        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        // Foreign Key Relationship with Employee
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}

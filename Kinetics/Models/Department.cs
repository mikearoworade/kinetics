using System.ComponentModel.DataAnnotations;

namespace Kinetics.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters.")]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        // Navigation Property
        public ICollection<Employee> Employees { get; set; }
    }
}

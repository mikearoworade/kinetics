using System.ComponentModel.DataAnnotations;

namespace Kinetics.Models
{
    public class EmployeeProperty
    {
        [Key]
        public int PropertyID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Property cannot exceed 100 characters.")]
        public string PropertyName { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        [Required]
        public string Status { get; set; } = "Issued";

        // Foreign Key
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}

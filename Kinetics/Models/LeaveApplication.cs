using System.ComponentModel.DataAnnotations;

namespace Kinetics.Models
{
    public class LeaveApplication
    {
        [Key]
        public int LeaveID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Leave type cannot exceed 50 characters.")]
        public string LeaveType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]

        public string ApprovedByManager { get; set; } = "Pending";
        [Required]
        public string ApprovedByHR { get; set; } = "Pending";

        // Foreign Key
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

    }
}

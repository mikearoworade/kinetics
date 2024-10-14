using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kinetics.Models
{
    public class FinancialAssistanceRequest
    {
        [Key]
        public int RequestID { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]  // Specify the precision and scale
        [Range(1, double.MaxValue, ErrorMessage = "Requested amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [StringLength(255, ErrorMessage = "Reason cannot exceed 255 characters.")]
        public string Reason { get; set; }

        [Required]
        public string ApprovedByManager { get; set; } = "Pending";
        [Required]
        public string ApprovedByHR { get; set; } = "Pending";

        // Foreign Key
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}

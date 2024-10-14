using System.ComponentModel.DataAnnotations;

namespace Kinetics.Models
{
    public class EmployeeTraining
    {
        [Key]
        public int TrainingID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Training Name cannot exceed 100 characters.")]
        public string TrainingName { get; set; }

        [Required]
        public DateTime TrainingDate { get; set; }

        // Foreign Key
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Kinetics.Models
{
    public class Branch
    {
        public int BranchID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Branch name cannot exceed 100 characters.")]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Branch location cannot 150 characters.")]
        [Display(Name = "Branch Location")]
        public string BranchLocation { get; set; }

        // Navigation Property
        public ICollection<Employee> Employees { get; set; }

    }
}

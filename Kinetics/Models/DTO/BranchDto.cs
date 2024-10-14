using System.ComponentModel.DataAnnotations;

namespace Kinetics.Models.DTO
{
    public class BranchDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Branch name cannot exceed 100 characters.")]
        public string BranchName { get; set; } = string.Empty;

        [Required]
        [StringLength(150, ErrorMessage = "Branch location cannot 150 characters.")]
        public string BranchLocation { get; set; } = string.Empty;
    }
}

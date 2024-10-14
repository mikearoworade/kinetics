using System.ComponentModel.DataAnnotations;

namespace Kinetics.Models.DTO
{
    public class JobPositionDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Position name cannot exceed 100 characters.")]
        [Display(Name = "Position Name")]
        public string PositionName { get; set; }
    }
}

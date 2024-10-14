using System.ComponentModel.DataAnnotations;

namespace Kinetics.Models.DTO
{
    public class DepartmentDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters.")]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
}

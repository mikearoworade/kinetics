using System.ComponentModel.DataAnnotations;

namespace Kinetics.Models.DTO
{
    public class RoleDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Role name cannot exceed 100 characters.")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}

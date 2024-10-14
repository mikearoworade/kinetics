using System.ComponentModel.DataAnnotations;

namespace Kinetics.Models
{
    public class Role
    {
        public int RoleID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Role name cannot exceed 100 characters.")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        // Navigation Property
        public ICollection<Employee> Employees { get; set; }
    }
}

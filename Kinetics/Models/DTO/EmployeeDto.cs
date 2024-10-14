using System.ComponentModel.DataAnnotations;

namespace Kinetics.Models.DTO
{
    public class EmployeeDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "StaffID cannot exceed 20 characters")]
        [Display(Name = "Staff ID")]
        public string StaffID { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        //Foreign Keys and Navigation Property
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
        public int PositionID { get; set; }
        public int BranchID { get; set; }
        public int? LineManagerID { get; set; }
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        // Property to store the file name of the uploaded photo
        public string? Photo { get; set; }  // Store the image file path or name
    }
}

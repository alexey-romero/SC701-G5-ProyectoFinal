using System.ComponentModel.DataAnnotations;

namespace PAWPMD.Models.Models
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "Second last name cannot exceed 50 characters.")]
        public string? SecondLastName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username cannot exceed 30 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password confirmation does not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}

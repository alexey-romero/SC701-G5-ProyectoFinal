using System.ComponentModel.DataAnnotations;

namespace PAWPMD.Models.Models
{
    public class LoginRequest
    {
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string? Username { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Custom validation to ensure that either Username or Email is provided
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Username) || !string.IsNullOrEmpty(Email);
        }
    }
}

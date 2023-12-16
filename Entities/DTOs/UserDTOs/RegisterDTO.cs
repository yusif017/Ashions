using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs.UserDTOs
{
    public class RegisterDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "3 simvoldan cox olmalidir"), MaxLength(25, ErrorMessage = "max 25 simvoldan ibaret olmalidir ")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Surname is required.")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)] // Type Passwordd oldugunu deyirik 
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Pasword and password repead is not match.")]
        public string ConfirmPassword { get; set; }
        public string? Address { get; set; }
    }
}

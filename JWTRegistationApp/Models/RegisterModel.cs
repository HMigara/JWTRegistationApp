using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace JWTRegistationApp.Models
{
    public class RegisterModel
    {
        [Required, MinLength(3), MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public IFormFile Document { get; set; } = null!;

        [Required]
        public DateTime RegistrationDate { get; set; }
    }
}

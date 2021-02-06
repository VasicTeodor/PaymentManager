using System.ComponentModel.DataAnnotations;

namespace PublishingCompany.Api.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "You must specify password at least 4 characters long")]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace PaymentManager.Api.Dtos
{
    public class RegisterDto
    {
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "You must specify username at least 8 characters long")]
        public string UserName { get; set; }
        [Required]
        public string UserType { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "You must specify password at least 8 characters long")]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace IdentityJWT.Models.DTO
{
    public class HeroDTO
    {
        [Required]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ConfirmPassword { get; set; } = string.Empty;
    }
}

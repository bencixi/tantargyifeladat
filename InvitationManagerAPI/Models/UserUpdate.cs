using System.ComponentModel.DataAnnotations;

namespace InvitationManagerAPI.Models
{
    public class UserUpdate
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; }
        [Required]
        public int PhoneNumber { get; set; } = 0;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}

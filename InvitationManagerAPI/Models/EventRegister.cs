using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InvitationManagerAPI.Models
{
    public class EventRegister : Controller
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public DateTime Start_Date { get; set; }
        [Required]
        public DateTime End_Date { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required, MinLength(4), MaxLength(4)]
        public int Zip { get; set; }
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Street { get; set; } = string.Empty;
        public bool AllDay { get; set; } = false;
    }
}

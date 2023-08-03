using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace InvitationManagerAPI.Models
{
    public class CalendarBookings
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set;}
        public string Description { get; set; } = string.Empty;
        public int Zip { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public bool AllDay { get; set; } = false;

    }
}

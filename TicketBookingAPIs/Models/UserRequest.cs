using System.ComponentModel.DataAnnotations;

namespace TicketBookingAPIs.Models
{
    public class UserRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}

using System.Net.Sockets;
using System.Xml.Linq;

namespace TicketBookingAPIs.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string? EventName { get; set; }
        public bool? TicketAvailability { get; set; }
    }
    public class EventDetail : Event
    {
        // date, time, venue, ticket availability, and ticket prices
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string? Venue { get; set; }
        public decimal? TicketPrices { get; set; }
    }
}

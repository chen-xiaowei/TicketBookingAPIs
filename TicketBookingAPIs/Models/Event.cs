using System.Net.Sockets;
using System.Xml.Linq;

namespace TicketBookingAPIs.Models
{
    public class Event
    {
        /// <summary>
        /// A unique Identifier for each Event
        /// </summary>
        public int EventID { get; set; }
        /// <summary>
        /// The Event Name
        /// </summary>
        public string? EventName { get; set; }
        /// <summary>
        /// Whether it has tickets left
        /// </summary>
        public bool? TicketAvailability { get; set; }
    }
    public class EventDetail : Event
    {
        /// <summary>
        /// The Date of when the Event will take place
        /// </summary>
        public DateOnly Date { get; set; }
        /// <summary>
        /// The time of when the Event will happen
        /// </summary>
        public TimeOnly Time { get; set; }
        /// <summary>
        /// The location of where the event will take place
        /// </summary>
        public string? Venue { get; set; }
        /// <summary>
        /// Price for tickets
        /// </summary>
        public decimal? TicketPrices { get; set; }
    }
}

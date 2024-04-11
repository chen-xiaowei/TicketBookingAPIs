using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace TicketBookingAPIs.Models
{
    public class BookTicketRequest
    {
        /// <summary>
        /// A unique identifier of a User
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// A unique identifier of a Event
        /// </summary>
        public int EventID { get; set; }
        /// <summary>
        /// Type of Tickets
        /// </summary>
        public TicketType TicketType { get; set; }
        // <summary>
        /// Qty of booking tickets
        /// </summary>
        public int Quantity { get; set; }
    }
    public class BookTicketResponse
    {
        /// <summary>
        /// A unique identifier for this booking. The value can be assigned once the booking has been made for the event.
        /// </summary>
        public int BookID { get; set; }
    }
    public class MyBooking : BookTicketRequest
    {
        /// <summary>
        /// A unique identifier for this booking. The value can be assigned once the booking has been made for the event.
        /// </summary>
        public int BookID { get; set; }
        /// <summary>
        /// The Event Name
        /// </summary>
        public string? EventName { get; set; }
    }
    public class CancelBookingRequest
    {
        /// <summary>
        /// A unique identifier of a User
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// A unique identifier for this booking. The value can be assigned once the booking has been made for the event.
        /// </summary>
        public int BookID { get; set; }
    }
    public class BookingPaymentRequest
    {
        /// <summary>
        /// A unique identifier of a User
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// A unique identifier for this booking. The value can be assigned once the booking has been made for the event.
        /// </summary>
        public int BookID { get; set; }
        /// <summary>
        /// The total prices of each booking tickets.
        /// </summary>
        public decimal? TotalPrices { get; set; }
    }
    public enum TicketType
    {
        [Display(Name = "Basktball")]
        Basktball = 1,

        [Display(Name = "Baseball")]
        Baseball = 2,

        [Display(Name = "Golf")]
        Golf = 3,
    }
}

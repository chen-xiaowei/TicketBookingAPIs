using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace TicketBookingAPIs.Models
{
    public class BookTicketRequest
    {
        public int UserID { get; set; }
        public int EventID { get; set; }
        public TicketType TicketType { get; set; }
        public int Quantity { get; set; }
    }
    public class BookTicketResponse
    {
        public int BookID { get; set; }
    }
    public class MyBooking : BookTicketRequest
    {
        public int BookID { get; set; }
        public string? EventName { get; set; }
    }
    public class CancelBookingRequest
    {
        public int UserID { get; set; }
        public int BookID { get; set; }
    }
    public class BookingPaymentRequest
    {
        public int UserID { get; set; }
        public int BookID { get; set; }
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

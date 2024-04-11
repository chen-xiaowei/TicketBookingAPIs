using System.ComponentModel.DataAnnotations;

namespace TicketBookingAPIs.Models
{
    public class UserRequest
    {
        /// <summary>
        /// A unique identifier of a User
        /// </summary>
        public string? UserID { get; set; }
        /// <summary>
        /// Name of user
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// Password of the User
        /// </summary>
        public string? Password { get; set; }
    }
}

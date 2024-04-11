using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.ComponentModel;
using TicketBookingAPIs.Models;
using TicketBookingAPIs.Services;

namespace TicketBookingAPIs.Controllers
{
    [ApiVersion("1")]
    public class BookingController : BaseController
    {
        private readonly ILogger<BookingController> _logger;

        public BookingController(ILogger<BookingController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        [Authorize(Roles = "Admin")]
        [Description("1.Users should be able to browse a list of available events")]
        public ApiResult<List<Event>> Events()
        {
            //throw new ApplicationException("test exception");
            _logger.LogDebug("Query Events");
            var events = new List<Event>();
            for (var i=0;i<10;i++)
            {
                events.Add(new Event { EventID = i + 1, EventName = $"Name{i + 1}", TicketAvailability = true });
            }
            return new ApiResult<List<Event>>(ApiResultStatusCode.Success, events, ApiResultStatusCode.Success.ToString(), new Meta());
        }
        [HttpGet()]
        [Description("2.Users should be able to view details of a specific event, including event\r\nname, date, time, venue, ticket availability, and ticket prices\r\n")]
        public ApiResult EventDetail(int eventId)
        {
            _logger.LogDebug("Query Event Detail by event id.");
            var events = new List<EventDetail>();
            for (var i = 0; i < 10; i++)
            {
                events.Add(new EventDetail { EventID = eventId, EventName = $"Name{i + 1}", TicketAvailability = true, 
                    Date= DateOnly.FromDateTime(DateTime.Now.AddDays(i)),
                    Time = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                    TicketPrices = i + 20.00m, Venue = $"No.{i + 8}, Bao'an Avenue, Nansha District, Guangzhou."
                });
            }
            return new ApiResult<List<EventDetail>>(ApiResultStatusCode.Success, events, ApiResultStatusCode.Success.ToString(), new Meta());
        }
        [HttpPost()]
        [Description("3.Users should be able to book tickets for an event by specifying the event\r\nID, ticket type, and quantity\r\n")]
        public ApiResult BookingTickets(BookTicketRequest book)
        {
            if(book == null)
            {
                return new ApiResult(ApiResultStatusCode.BadRequest, ApiResultStatusCode.BadRequest.ToString());
            }
            return new ApiResult<BookTicketResponse>(ApiResultStatusCode.Success, new BookTicketResponse { BookID = 1 }, ApiResultStatusCode.Success.ToString());
        }
        [HttpGet()]
        [Description("4.Users should be able to view their own bookings")]
        public ApiResult MyBookings(int userID)
        {
            if (userID <= 0)
            {
                return new ApiResult(ApiResultStatusCode.BadRequest, ApiResultStatusCode.BadRequest.ToString());
            }
            var data = new List<MyBooking>();
            for (var i = 0; i < 10; i++)
            {
                data.Add(new MyBooking
                {
                    EventID = i + 1,
                    EventName = $"Name{i + 1}",
                    TicketType = (TicketType)new Random().Next(1,3),
                    Quantity = i + 1
                });
            }
            return new ApiResult<List<MyBooking>>(ApiResultStatusCode.Success, data, ApiResultStatusCode.Success.ToString(), new Meta());
        }
        
        [HttpPost()]
        [Description("5.Users should be able to cancel a booking")]
        public ApiResult CancelBooking(CancelBookingRequest book)
        {
            if (book == null)
            {
                return new ApiResult(ApiResultStatusCode.BadRequest, ApiResultStatusCode.BadRequest.ToString());
            }
            return new ApiResult(ApiResultStatusCode.Success, ApiResultStatusCode.Success.ToString());
        }
        [HttpPost()]
        [Description("6.User should be able to make payment to the booking")]
        public ApiResult Payment(BookingPaymentRequest book)
        {
            if (book == null)
            {
                return new ApiResult(ApiResultStatusCode.BadRequest, ApiResultStatusCode.BadRequest.ToString());
            }
            return new ApiResult(ApiResultStatusCode.Success, ApiResultStatusCode.Success.ToString());
        }
    }
}

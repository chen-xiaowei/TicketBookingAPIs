using TicketBookingAPIs.Models;

namespace TicketBookingAPIs.Services
{
    public interface IJwtService
    {
        AccessTokenObject CreateToken(UserRequest req);
        AccessTokenObject RefreshToken(RefreshTokenRequest req);
    }
}

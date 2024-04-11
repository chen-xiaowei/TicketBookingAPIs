using System.IdentityModel.Tokens.Jwt;

namespace TicketBookingAPIs.Services
{
    public class AccessTokenObject
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }

        public AccessTokenObject()
        { }

        public AccessTokenObject(string accessToken, string refreshToken, int expiresIn)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            TokenType = "Bearer";
            ExpiresIn = expiresIn;
        }
    }
    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

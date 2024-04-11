using System.IdentityModel.Tokens.Jwt;

namespace TicketBookingAPIs.Services
{
    public class AccessTokenObject
    {
        /// <summary>
        ///  A type of token used for authentication and authorization
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// A longer validity period token that is to facilitate the process of obtaining a new AccessToken when the existing one expires.
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// we use the type of Bearer in this case
        /// </summary>
        public string TokenType { get; set; }
        /// <summary>
        /// Expiration date of the token
        /// </summary>
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
        /// <summary>
        ///  A type of token used for authentication and authorization
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// A longer validity period token that is to facilitate the process of obtaining a new AccessToken when the existing one expires.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}

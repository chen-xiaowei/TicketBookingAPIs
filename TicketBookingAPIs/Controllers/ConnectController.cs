using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Web;
using TicketBookingAPIs.Models;
using TicketBookingAPIs.Services;

namespace TicketBookingAPIs.Controllers
{
    [ApiVersion("1")]
    public class ConnectController : BaseController
    {
        private readonly ILogger<ConnectController> _logger;
        private readonly IJwtService _jwtService;
        public ConnectController(ILogger<ConnectController> logger, IJwtService jwtService)
        {
            _logger = logger;
            _jwtService = jwtService;
        }

        
        [HttpPost(Name = "Token")]
        [AllowAnonymous]
        public ApiResult<AccessTokenObject> Token(UserRequest user)
        {
            ApiResult<AccessTokenObject> result = new ApiResult<AccessTokenObject>(ApiResultStatusCode.ServerError, new AccessTokenObject(), "ServerError");
            if (user.UserName == "admin" && user.Password == "123456")
            {
                result.Data = _jwtService.CreateToken(user);
                result.StatusCode = ApiResultStatusCode.Success;
                result.Message = "Success";
            }
            if(!string.IsNullOrEmpty(result.Data.AccessToken))
            {
                HttpContext.Session.SetString("JWToken", result.Data.AccessToken);
            }
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Refresh")]
        public ApiResult Refresh(RefreshTokenRequest token)
        {
            ApiResult<AccessTokenObject> result = new ApiResult<AccessTokenObject>(ApiResultStatusCode.ServerError, new AccessTokenObject(), "ServerError");

            AccessTokenObject accessTokenObject = _jwtService.RefreshToken(token);
            if (string.IsNullOrEmpty(accessTokenObject.AccessToken))
            {
                return result;
            }

            HttpContext.Session.SetString("JWToken", accessTokenObject.AccessToken);

            result.Data = accessTokenObject;
            result.StatusCode = ApiResultStatusCode.Success;
            result.Message = "AccessToken has been refreshed successfully";

            return result;
        }
    }
}

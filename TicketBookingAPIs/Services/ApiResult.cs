using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TicketBookingAPIs.Services
{
    public class ApiResult
    {
        public Meta Meta { get; set; }
        public ApiResultStatusCode StatusCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public List<string> Errors { get; set; }

        public ApiResult(ApiResultStatusCode statusCode, string message = null, Meta meta = null)
        {
            StatusCode = statusCode;
            Message = message;
            Meta = meta;
        }
    }
    public class ApiResult<T> : ApiResult where T : class
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        public ApiResult(ApiResultStatusCode statusCode, T data, string message = null, Meta meta = null)
           : base(statusCode, message, meta)
        {
            Data = data;
        }
    }
        public enum ApiResultStatusCode
    {
        [Display(Name = "OK")]
        Success = 200,     

        [Display(Name = "Bad Request")]
        BadRequest = 400,

        [Display(Name = "Unauthorized")]
        UnAuthorized = 401,

        [Display(Name = "Forbidden")]
        Forbidden = 403,

        [Display(Name = "Not Found")]
        NotFound = 404,
             
        [Display(Name = "Unprocessable Entity")]
        UnprocessableEntity = 422,

        [Display(Name = "Server Errors")]
        ServerError = 500,
    }
}

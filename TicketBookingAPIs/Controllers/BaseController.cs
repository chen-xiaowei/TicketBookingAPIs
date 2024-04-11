using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security;
using Microsoft.AspNetCore.Identity;
namespace TicketBookingAPIs.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]// api/v1/[controller]/[action]
    //[Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        
    }
}

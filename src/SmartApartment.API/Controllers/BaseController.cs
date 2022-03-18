using Microsoft.AspNetCore.Mvc;

namespace SmartApartment.API.Controllers
{
    [ApiController]
    [ProducesErrorResponseType(typeof(ErrorResponse))]
    [Route("api")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
       
    }
}

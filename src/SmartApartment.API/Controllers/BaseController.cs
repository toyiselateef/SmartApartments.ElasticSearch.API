using Microsoft.AspNetCore.Mvc;

namespace SmartApartment.API.Controllers
{
    [ApiController]
    [ProducesErrorResponseType(typeof(ErrorResponse))]
    [Route("api/apartments")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
       
    }
}

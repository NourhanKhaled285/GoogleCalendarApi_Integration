using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GoogleCalendarApis.Errors;

namespace GoogleCalendarApis.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public ActionResult Error(int code)
        {  
            return new ObjectResult(new ApiResponse(code));
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Management.System.APIs.Controllers
{
    // Base controller class for all API controllers
    // Provides common configuration and functionality for derived controllers
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {

    }
}

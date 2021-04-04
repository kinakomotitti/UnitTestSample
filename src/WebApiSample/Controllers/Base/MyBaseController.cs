using Microsoft.AspNetCore.Mvc;

namespace WebApiSample.Controllers.Base
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MyBaseController: ControllerBase
    {
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Wd3w.Localizer.Api.Controllers
{
    [ApiController]
    [Route("api/sample")]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        public async Task<string> GetHelloAsync()
        {
            return "hello world";
        }
    }
}
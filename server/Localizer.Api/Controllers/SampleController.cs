using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Localizer.Api.Controllers
{
	[ApiController]
	[Route("api/sample")]
	public class SampleController : ControllerBase
	{
		[HttpGet]
		public async Task<string> GetHelloAsync() => "hello world";
	}
}
using Microsoft.AspNetCore.Mvc;

namespace RabbitOM.Decoder.Controllers
{
	[ApiController]
	[Route( "[registry]" )]
	public class RegistryController : ControllerBase
	{
		[HttpGet("find")]
		public ActionResult Find( [FromQuery] string type , [FromQuery] string? version = default )
		{
			throw new NotImplementedException();
		}

		[HttpPost("list")]
		public ActionResult ListAll()
		{
			throw new NotImplementedException();
		}

		[HttpPost("pull")]
		public ActionResult Pull( [FromQuery] string type )
		{
			throw new NotImplementedException();
		}
	}
}

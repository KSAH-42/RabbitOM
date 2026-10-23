using Microsoft.AspNetCore.Mvc;

namespace RabbitOM.Decoder.Controllers
{
	using RabbitOM.Decoder.Models;

	[ApiController]
	[Route( "[configuration]" )]
	public class ConfigurationController : ControllerBase
	{
		[HttpGet()]
		public ActionResult<Configuration> GetConfiguration()
		{
			throw new NotImplementedException();
		}

		[HttpPost()]
		public ActionResult SetConfiguration( [FromBody] Configuration configuration )
		{
			throw new NotImplementedException();
		}
	}
}

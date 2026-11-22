using Microsoft.AspNetCore.Mvc;

namespace RabbitOM.Decoder.Api.Controllers
{
	using RabbitOM.Decoder.Api.Data;
	
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

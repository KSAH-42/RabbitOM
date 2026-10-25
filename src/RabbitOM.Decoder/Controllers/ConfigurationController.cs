using Microsoft.AspNetCore.Mvc;

namespace RabbitOM.Decoder.Controllers
{
	using RabbitOM.Decoder.Models;
	using RabbitOM.Decoder.Services;

	[ApiController]
	[Route( "[configuration]" )]
	public class ConfigurationController : ControllerBase
	{
		private readonly IConfigurationService _configurationService;

		public ConfigurationController( IConfigurationService configurationService )
		{
			_configurationService = configurationService ?? throw new ArgumentNullException( nameof( configurationService ) );
		}

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

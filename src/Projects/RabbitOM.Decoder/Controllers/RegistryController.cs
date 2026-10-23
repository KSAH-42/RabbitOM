using Microsoft.AspNetCore.Mvc;

namespace RabbitOM.Decoder.Controllers
{
	using RabbitOM.Decoder.Services;

	[ApiController]
	[Route( "[registry]" )]
	public class RegistryController : ControllerBase
	{
		private readonly IRegistryService _registryService;

		public RegistryController( IRegistryService registryService )
		{
			_registryService = registryService ?? throw new ArgumentNullException( nameof( registryService ) );
		}

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

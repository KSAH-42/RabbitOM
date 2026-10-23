using Microsoft.AspNetCore.Mvc;

namespace RabbitOM.Decoder.Controllers
{
	using RabbitOM.Decoder.Services;

	[ApiController]
	[Route( "[management]" )]
	public class ManagementController : ControllerBase
	{
		private readonly IManagementService _managementService;

		public ManagementController( IManagementService managementService )
		{
			_managementService = managementService ?? throw new ArgumentNullException( nameof( managementService ) );
		}

		[HttpPost("install")]
		public ActionResult<string> Install( [FromQuery] string? type , [FromQuery] string? version = null )
		{
			throw new NotImplementedException();
		}

		[HttpPost("uninstall")]
		public ActionResult UnInstall( [FromQuery] string? id )
		{
			throw new NotImplementedException();
		}

		[HttpPost("create")]
		public ActionResult Create( [FromQuery] string? id )
		{
			throw new NotImplementedException();
		}

		[HttpPost("remove")]
		public ActionResult Remove( [FromQuery] string? id )
		{
			throw new NotImplementedException();
		}

		[HttpPost("remove-all")]
		public ActionResult RemoveAll()
		{
			throw new NotImplementedException();
		}
	}
}

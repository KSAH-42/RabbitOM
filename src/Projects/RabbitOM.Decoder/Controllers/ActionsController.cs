using Microsoft.AspNetCore.Mvc;

namespace RabbitOM.Decoder.Controllers
{
	using RabbitOM.Decoder.Services;

	[ApiController]
	[Route( "[actions]" )]
	public class ActionsController : ControllerBase
	{
		private readonly IActionService _actionService;

		public ActionsController( IActionService actionService )
		{
			_actionService = actionService ?? throw new ArgumentNullException( nameof( actionService ) );
		}

		[HttpPost("start/{id:string}")]
		public ActionResult Start( string id )
		{
			throw new NotImplementedException();
		}

		[HttpPost("start-all")]
		public ActionResult StartAll()
		{
			throw new NotImplementedException();
		}

		[HttpPost("stop/{id:string}")]
		public ActionResult Stop( string id )
		{
			throw new NotImplementedException();
		}

		[HttpPost("stop-all")]
		public ActionResult StopAll()
		{
			throw new NotImplementedException();
		}
	}
}

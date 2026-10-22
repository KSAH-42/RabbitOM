using Microsoft.AspNetCore.Mvc;

namespace RabbitOM.Decoder.Controllers
{
	[ApiController]
	[Route( "[actions]" )]
	public class ActionsController : ControllerBase
	{
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

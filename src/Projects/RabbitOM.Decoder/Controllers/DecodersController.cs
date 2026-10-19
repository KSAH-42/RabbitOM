using Microsoft.AspNetCore.Mvc;

namespace RabbitOM.Decoder.Controllers
{
	[ApiController]
	[Route( "[decoders]" )]
	public class DecodersController : ControllerBase
	{
		[HttpGet()]
		public ActionResult GetAllDecoders()
		{
			throw new NotImplementedException();
		}

		[HttpGet("{id:string}")]
		public ActionResult GetDecoderById( string id )
		{
			throw new NotImplementedException();
		}

		[HttpPost("create/{type:string}")]
		public ActionResult CreateDecoder( string type ) // TODO: use a class pass the type, version, and other options
		{
			throw new NotImplementedException();
		}

		[HttpDelete("remove/{id:string}")]
		public ActionResult RemoveDecoderById( string id )
		{
			throw new NotImplementedException();
		}

		[HttpDelete("remove")]
		public ActionResult RemoveDecoders()
		{
			throw new NotImplementedException();
		}
	}
}

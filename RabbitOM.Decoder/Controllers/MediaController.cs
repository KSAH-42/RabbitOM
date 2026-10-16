using Microsoft.AspNetCore.Mvc;

namespace RabbitOM.Decoder.Controllers
{
	using RabbitOM.Decoder.Models;

	[ApiController]
	[Route( "[controller]" )]
	public class MediaController : ControllerBase
	{
		[HttpGet("configuration")]
		public ActionResult<DecoderConfiguration> GetConfiguration()
		{
			throw new NotImplementedException();
		}

		[HttpPost("configuration")]
		public ActionResult SetConfiguration( [FromBody] DecoderConfiguration configuration )
		{
			throw new NotImplementedException();
		}

		[HttpPost("start")]
		public ActionResult Start()
		{
			throw new NotImplementedException();
		}

		[HttpPost("stop")]
		public ActionResult Stop()
		{
			throw new NotImplementedException();
		}

		[HttpGet("getStatus")]
		public ActionResult<DecoderStatus> GetStatus()
		{
			throw new NotImplementedException();
		}

		[HttpGet("getTransports")]
		public ActionResult<List<string>> GetTransports()
		{
			throw new NotImplementedException();
		}
	}
}

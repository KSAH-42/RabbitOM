using Microsoft.AspNetCore.Mvc;

namespace RabbitOM.Decoder.Controllers
{
	using RabbitOM.Decoder.Models;
	using RabbitOM.Decoder.Models.Configuration;

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

		[HttpGet("status")]
		public ActionResult<DecoderStatus> GetStatus()
		{
			throw new NotImplementedException();
		}

		[HttpGet("transports")]
		public ActionResult<List<string>> GetTransports()
		{
			throw new NotImplementedException();
		}
	}
}

using Microsoft.AspNetCore.Mvc;

namespace RabbitOM.Decoder.FFMpeg.Api.Controllers
{
	using RabbitOM.Decoder.FFMpeg.Api.Data.Configuration;
	using RabbitOM.Decoder.FFMpeg.Api.Data.Diagnostics;

	[ApiController]
	[Route( "[controller]" )]
	public class ChannelsController : ControllerBase
	{
		[HttpPost("{index:int}/start")]
		public ActionResult Start( int index , [FromBody] VideoChannelConfiguration configuration ) // in case if we dont' a new protocol add route like this start, start-rtsp, start-http, etc... and pass configmodel without any inheritance and the route start will used rtsp by default
		{
			throw new NotImplementedException();
		}

		[HttpPost("{index:int}/stop")]
		public ActionResult Stop( int index )
		{
			throw new NotImplementedException();
		}

		[HttpGet("{index:int}/configuration")]
		public ActionResult<VideoChannelConfiguration> GetConfiguration( int index )
		{
			throw new NotImplementedException();
		}

		[HttpGet("{index:int}/status")]
		public ActionResult<VideoChannelStatus> GetStatus( int index )
		{
			throw new NotImplementedException();
		}

		[HttpGet("{index:int}/events")]
		public ActionResult PullEvents( int index , [FromQuery] int timeout )
		{
			throw new NotImplementedException();
		}
	}
}

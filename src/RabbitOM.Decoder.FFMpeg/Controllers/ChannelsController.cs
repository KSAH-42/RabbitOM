using Microsoft.AspNetCore.Mvc;

namespace RabbitOM.Decoder.FFMpeg.Controllers
{
	using RabbitOM.Decoder.FFMpeg.Models;
	using RabbitOM.Decoder.FFMpeg.Services;

	[ApiController]
	[Route( "[controller]" )]
	public class ChannelsController : ControllerBase
	{
		private readonly IChannelsService _channelsService;

		public ChannelsController( IChannelsService channelsServices )
		{
			_channelsService = channelsServices ?? throw new ArgumentNullException( nameof( channelsServices ) );
		}

		[HttpPost("{index:int}/start")]
		public ActionResult Start( int index , [FromBody] RtspChannelConfigurationModel configuration ) // in case if we dont' a new protocol add route like this start, start-rtsp, start-http, etc... and pass configmodel without any inheritance and the route start will used rtsp by default
		{
			throw new NotImplementedException();
		}

		[HttpPost("{index:int}/stop")]
		public ActionResult Stop( int index )
		{
			throw new NotImplementedException();
		}

		[HttpGet("{index:int}/configuration")]
		public ActionResult<RtspChannelConfigurationModel> GetConfiguration( int index )
		{
			throw new NotImplementedException();
		}

		[HttpGet("{index:int}/status")]
		public ActionResult<RtspChannelStatusModel> GetStatus( int index )
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

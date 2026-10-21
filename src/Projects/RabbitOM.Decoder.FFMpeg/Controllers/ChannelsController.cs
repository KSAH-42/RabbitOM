using Microsoft.AspNetCore.Mvc;

namespace RabbitOM.Decoder.FFMpeg.Controllers
{
	using RabbitOM.Decoder.FFMpeg.Models;
	using RabbitOM.Decoder.FFMpeg.Models.Configuration;
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
		public ActionResult Start( int index , [FromBody] ChannelConfiguration configuration )
		{
			throw new NotImplementedException();
		}

		[HttpPost("{index:int}/stop")]
		public ActionResult Stop( int index )
		{
			throw new NotImplementedException();
		}

		[HttpGet("{index:int}/configuration")]
		public ActionResult<ChannelConfiguration> GetConfiguration( int index )
		{
			throw new NotImplementedException();
		}

		[HttpGet("{index:int}/status")]
		public ActionResult<ChannelStatus> GetStatus( int index )
		{
			throw new NotImplementedException();
		}
	}
}

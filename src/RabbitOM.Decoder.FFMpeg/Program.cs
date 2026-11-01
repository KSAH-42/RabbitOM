using RabbitOM.Decoder.FFMpeg.Application.Services;
using RabbitOM.Decoder.FFMpeg.Infra.Channels;

namespace RabbitOM.Decoder.FFMpeg
{
	public static class Program
	{
		public static void Main( string[] args )
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddSwaggerGen();
			builder.Services.AddSingleton<IVideoChannelService>( new RtspVideoChannelService() );

			var app = builder.Build();

			app.UseHttpsRedirection();

			if ( app.Environment.IsDevelopment() )
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();
			app.MapControllers();

			app.Run();
		}
	}
}

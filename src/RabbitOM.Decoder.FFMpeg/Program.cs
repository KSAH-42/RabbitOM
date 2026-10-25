using RabbitOM.Decoder.FFMpeg.Services;

namespace RabbitOM.Decoder.FFMpeg
{
	public static class Program
	{
		public static void Main( string[] args )
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddSwaggerGen();
			builder.Services.AddSingleton<IChannelsService>( new ChannelsService() );

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

namespace RabbitOM.Decoder
{
	public static class Program
	{
		public static void Main( string[] args )
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			app.UseHttpsRedirection();
			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}

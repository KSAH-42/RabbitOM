using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RabbitOM.Player.Net
{
    public static class HttpServerBuilderExtensions
    {
        public static WebApplication BuildWith<TOptions,TAuthenticationHandler>( this WebApplicationBuilder builder , string apiKey , Action<TOptions> optionsHandler )
			where TOptions : AuthenticationSchemeOptions, new()
			where TAuthenticationHandler : AuthenticationHandler<TOptions>
		{
			if ( builder == null )
			{
				throw new ArgumentNullException( nameof( apiKey ) );
			}

			if ( optionsHandler == null )
			{
				throw new ArgumentNullException( nameof( optionsHandler ) );
			}

			if ( string.IsNullOrWhiteSpace( apiKey ) )
			{
				return builder.Build();
			}

			builder.Services
				.AddAuthentication( "ApiKey" )
				.AddScheme<TOptions,TAuthenticationHandler>( "ApiKey" , optionsHandler )
				;

			builder.Services.AddAuthorization();

			var app = builder.Build();

			app.UseAuthentication();
			app.UseAuthorization();

			return app;
		}
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace RabbitOM.Player.Net
{
	public sealed class HttpServerAuthenticator : AuthenticationHandler<HttpServerAuthenticatorOptions>
	{
		private readonly string _apiKey;

		public HttpServerAuthenticator( IOptionsMonitor<HttpServerAuthenticatorOptions> options , ILoggerFactory logger , UrlEncoder encoder ) : base( options , logger , encoder  )
		{
			_apiKey = options.CurrentValue.ApiKey;
		}

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			if ( ! Request.Headers.TryGetValue( "X-API-Key" , out var keys ) || keys.Count == 0 )
			{
				return await Task.FromResult( AuthenticateResult.Fail( "no api key found" ) );
			}

			if ( ! keys.Any( key => StringComparer.OrdinalIgnoreCase.Equals( _apiKey ?? string.Empty , key ?? string.Empty ) ) )
			{
				return await Task.FromResult( AuthenticateResult.Fail( "invalid api key" ) );
			}

			return await Task.FromResult(
				AuthenticateResult.Success(
					new AuthenticationTicket(
						new ClaimsPrincipal(
							new ClaimsIdentity( new[]
							{
								new Claim(ClaimTypes.Name,"ClientApi"),
								new Claim(ClaimTypes.Role,"ApiConsumer")
							} , Scheme.Name ) ),
						Scheme.Name )
					));
		}
	}
}

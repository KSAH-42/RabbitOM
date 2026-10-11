using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace RabbitOM.Player.Net
{
	public sealed class HttpAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		private readonly IApiKeyValidator _validator;

		public HttpAuthenticationHandler( IOptionsMonitor<AuthenticationSchemeOptions> options , ILoggerFactory logger , UrlEncoder encoder , IApiKeyValidator validator )
			: base( options , logger , encoder  )
		{
			_validator = validator ?? throw new ArgumentNullException( nameof( validator ) );
		}

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			if ( ! Request.Headers.TryGetValue( "X-API-Key" , out var key ) )
			{
				return await Task.FromResult( AuthenticateResult.Fail( "no api key" ) );
			}

			if ( ! _validator.TryValidate( key ) )
			{
				return await Task.FromResult( AuthenticateResult.Fail( "invalid key" ) );
			}

			return await Task.FromResult(
				AuthenticateResult.Success(
					new AuthenticationTicket(
						new ClaimsPrincipal(
							new ClaimsIdentity( new[]
							{
								new Claim( ClaimTypes.Name, "ClientApi" ),
								new Claim(ClaimTypes.Role, "ApiConsumer")
							} , Scheme.Name ) ),
						Scheme.Name )
					));
		}
	}
}

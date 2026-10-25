using Microsoft.AspNetCore.Authentication;
using System;

namespace RabbitOM.Player.Net
{
	public sealed class HttpServerAuthenticatorOptions : AuthenticationSchemeOptions
	{
		public string ApiKey { get; set; }
	}
}

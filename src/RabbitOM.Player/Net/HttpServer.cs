using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace RabbitOM.Player.Net
{
    public sealed class HttpServer : IDisposable
	{
        private readonly HttpServerRoutes _routes = new HttpServerRoutes();
        private WebApplication _webApplication;




        public string ApiKey { get; set; }

        public bool IsStarted
        {
            get => _webApplication != null;
        }



        public void Start(params string[] endpoints)
        {
            if ( endpoints == null )
            {
                throw new ArgumentNullException( nameof( endpoints ) );
            }

            if ( endpoints.Length == 0 || endpoints.Any( string.IsNullOrWhiteSpace ) )
            {
                throw new ArgumentException( nameof( endpoints ) );
            }

            if ( _webApplication != null )
            {
                throw new InvalidOperationException( "the server is already started" );
            }

            var builder = WebApplication.CreateBuilder();

            builder.WebHost.UseKestrel().UseUrls( endpoints );

            var webApplication = builder.BuildWith<HttpServerAuthenticatorOptions,HttpServerAuthenticator>( ApiKey , options => options.ApiKey = ApiKey );

            foreach ( var route in _routes.PostRoutes )
            {
                webApplication.MapPost( route.Key , route.Value ).UseUIDispatcher( route.Value );
            }

            webApplication.StartAsync().GetAwaiter().GetResult();

            _webApplication = webApplication;
        }

        public void Stop()
        {
            if ( _webApplication != null )
            {
                _webApplication.StopAsync();
                _webApplication.DisposeAsync().GetAwaiter().GetResult();
                _webApplication = null;
            }
        }

        public void Dispose()
        {
            Stop();
            _routes.RemoveAllRoutes();
        }

        public void MapPost(string pattern, Delegate action)
        {
            _routes.AddOrUpdateRoute( pattern, action );
        }
	}
}

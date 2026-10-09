using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace RabbitOM.Player.Scripting
{
    public sealed class ScriptServer : IDisposable
	{
        private readonly ScriptServerRoutes _routes = new ScriptServerRoutes();
        private WebApplication _webApplication;


        public bool IsStarted
        {
            get => _webApplication != null;
        }


        public void Start(string endpoint)
        {
            if ( string.IsNullOrWhiteSpace( endpoint ) )
            {
                throw new ArgumentNullException( nameof( endpoint ) );
            }

            if ( _webApplication != null )
            {
                throw new InvalidOperationException( "the server is already started" );
            }

            try
            {
                var builder = WebApplication.CreateBuilder();

                builder.WebHost.UseKestrel().UseUrls( endpoint );

                var webApplication = builder.Build();

                foreach ( var route in _routes.PostRoutes )
                {
                    webApplication.MapPost( route.Key , route.Value ).UseUIDispatcher( route.Value );
                }

                webApplication.StartAsync().GetAwaiter().GetResult();

                _webApplication = webApplication;
            }
            catch( Exception )
            {
                throw;
            }
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
            _routes.AddPostRoute( pattern, action );
        }
	}
}

using System;
using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace RabbitOM.Player.Scripting
{
    public sealed class ScriptServer : IDisposable
	{
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string,Func<HttpContext, Task>>> _routes = new();
        private readonly Dispatcher _dispatcher = Application.Current.Dispatcher;
        private WebApplication _webApplication;




        public bool IsStarted
        {
            get => _webApplication != null;
        }




        public static string NormalizeRoute(string route)
        {
            if ( string.IsNullOrWhiteSpace( route ) )
            {
                return "/";
            }

            route = route.Trim();

            return route.StartsWith('/') ? route.ToLowerInvariant() : string.Concat( "/" , route ).ToLowerInvariant();
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

                webApplication.Run( async context =>
                {
                    var path = NormalizeRoute(context.Request.Path.Value );

                    await OnHandleRequest( context , context.Request.Method , path );
                });

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
            _routes.Clear();
        }


        public void MapRouteGet(string route, Action action)
        {
            AddRoute("GET", route, async context =>
            {
                await _dispatcher.InvokeAsync(action);
                context.Response.StatusCode = StatusCodes.Status200OK;
            });
        }

        public void MapRoutePost(string route, Action action)
        {
            AddRoute("POST", route, async context =>
            {
                await _dispatcher.InvokeAsync( action );
                context.Response.StatusCode = StatusCodes.Status200OK;
            });
        }

        private void AddRoute(string method, string route, Func<HttpContext, Task> handler)
        {
            if ( string.IsNullOrWhiteSpace( method ) )
            {
                throw new ArgumentNullException( nameof( method ) );
            }

            if ( string.IsNullOrWhiteSpace( route) )
            {
                throw new ArgumentNullException( nameof( route ) );
            }

            if ( ! _routes.ContainsKey( method ) )
            {
                _routes[ method ] = new ConcurrentDictionary<string, Func<HttpContext, Task>>();
            }

            _routes[method][ NormalizeRoute(route)] = handler ?? throw new ArgumentNullException( nameof( handler ) );
        }








        // here were are not on wpf app where is recommend to return void. there is no eventargs
        private async Task OnHandleRequest( HttpContext context , string method , string path )
        {
            if ( ! _routes.TryGetValue( method , out var handlers ) )
            {
                await context.Response.WriteAsync("404 Not Found",context.RequestAborted);
                return;
            }

            if (! handlers.TryGetValue( path, out var handler ) )
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("404 Not Found",context.RequestAborted);
                return;
            }

            try
            {
                await handler(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync($"Internal Error: {ex.Message}",context.RequestAborted);
            }
        }
	}
}

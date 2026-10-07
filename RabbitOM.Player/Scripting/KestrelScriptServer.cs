using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace RabbitOM.Player.Scripting
{
    // TODO: refactor 
	public sealed class KestrelScriptServer : ScriptServer
	{
		private readonly ConcurrentDictionary<(string Method, string Route), Func<HttpContext, Task>> _routes = new();
        private readonly Dispatcher _dispatcher = Application.Current.Dispatcher;
        private WebApplication _app;
        private bool _isStarted;


        public override bool IsStarted => _isStarted;

        public override void MapRouteGet(string route, Action action)
        {
            RegisterRoute("GET", route, async context =>
            {
                await DispatchOnUIThreadAsync(action);
                context.Response.StatusCode = StatusCodes.Status200OK;
            });
        }

        public override void MapRouteGet<TDto>(string route, Action<TDto> action)
        {
            RegisterRoute("GET", route, async context =>
            {
                TDto dto = await DeserializeGetRequestAsync<TDto>(context);
                if (dto != null)
                {
                    await DispatchOnUIThreadAsync(() => action(dto));
                    context.Response.StatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
            });
        }

        public override void MapRoutePost(string route, Action action)
        {
            RegisterRoute("POST", route, async context =>
            {
                await DispatchOnUIThreadAsync(action);
                context.Response.StatusCode = StatusCodes.Status200OK;
            });
        }

        public override void MapRoutePost<TDto>(string route, Action<TDto> action)
        {
            RegisterRoute("POST", route, async context =>
            {
                TDto dto = await DeserializeJsonBodyAsync<TDto>(context);
                if (dto != null)
                {
                    await DispatchOnUIThreadAsync(() => action(dto));
                    context.Response.StatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
            });
        }

        public override void Start(string endpoint)
        {
            if (_isStarted) return;

            var builder = WebApplication.CreateBuilder();

            builder.WebHost.UseKestrel();
            builder.WebHost.UseUrls(endpoint);

            _app = builder.Build();

            _app.Run(async context =>
            {
                string method = context.Request.Method;
                string path = NormalizeRoute(context.Request.Path.Value ?? "/");

                if (_routes.TryGetValue((method, path), out var handler))
                {
                    try
                    {
                        await handler(context);
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsync($"Internal Error: {ex.Message}");
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync("404 Not Found");
                }
            });

            _app.StartAsync().GetAwaiter().GetResult();
            _isStarted = true;
        }

        public override void Stop()
        {
            if (!_isStarted || _app == null) return;

            _app.StopAsync();
            _isStarted = false;
        }

        public override void Close()
        {
            Stop();
            Dispose();
        }

        protected override void Dispose( bool disposing )
        {
            if (_app != null)
            {
                if (_isStarted)
                {
                    Stop();
                }
                _app.DisposeAsync().GetAwaiter().GetResult();
                _app = null;
            }
        }

        private async Task DispatchOnUIThreadAsync(Action action)
        {
            if (_dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                await _dispatcher.InvokeAsync(action);
            }
        }


        private void RegisterRoute(string method, string route, Func<HttpContext, Task> handler)
        {
            _routes[(method, NormalizeRoute(route))] = handler;
        }

        private static string NormalizeRoute(string route)
        {
            route = route.Trim();
            if (!route.StartsWith("/")) route = "/" + route;
            return route.ToLowerInvariant();
        }

        private static async Task<TDto?> DeserializeJsonBodyAsync<TDto>(HttpContext context)
        {
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return await JsonSerializer.DeserializeAsync<TDto>(context.Request.Body, options);
            }
            catch
            {
                return default;
            }
        }

        private static async Task<TDto?> DeserializeGetRequestAsync<TDto>(HttpContext context)
        {
            if (context.Request.ContentLength > 0)
            {
                return await DeserializeJsonBodyAsync<TDto>(context);
            }

            try
            {
                var dict = new Dictionary<string, string>();
                foreach (var key in context.Request.Query.Keys)
                {
                    dict[key] = context.Request.Query[key].ToString();
                }

                string json = JsonSerializer.Serialize(dict);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<TDto>(json, options);
            }
            catch
            {
                return default;
            }
        }
	}
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Windows;
using System.Windows.Threading;

namespace RabbitOM.Player.Net
{
	public sealed class WebServer : IAsyncDisposable
    {
        private readonly Dispatcher _dispatcher;
        private WebApplication? _app;

        public WebServer()
            : this(Application.Current.Dispatcher)
        {
        }

        public WebServer(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void Setup(int port)
        {
            if (_app != null)
                return;

            var builder = WebApplication.CreateBuilder();

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(port);
            });

            _app = builder.Build();
        }

        public async Task StartAsync()
        {
            await _app.StartAsync();
        }

        public async Task StopAsync()
        {
            if (_app == null)
                return;

            await _app.StopAsync();

            await _app.DisposeAsync();

            _app = null;
        }

        public void MapGet(string route, Func<string> handler)
        {
            EnsureStarted();

            _app!.MapGet(route, async () =>
            {
                return await _dispatcher.InvokeAsync(handler);
            });
        }

        public void MapGet<T>(string route, Func<T> handler)
        {
            EnsureStarted();

            _app!.MapGet(route, async () =>
            {
                return await _dispatcher.InvokeAsync(handler);
            });
        }

        public void MapGet(string route, Func<Task<string>> handler)
        {
            EnsureStarted();

            _app!.MapGet(route, async () =>
            {
                return await _dispatcher.InvokeAsync(handler);
            });
        }

        public void MapPost(string route, Func<HttpContext, Task> handler)
        {
            EnsureStarted();

            _app!.MapPost(route, async context =>
            {
                await _dispatcher.InvokeAsync(() => handler(context));
            });
        }

        public void MapPost(string route, Action handler)
        {
            //EnsureStarted();

            _app!.MapPost(route, () =>
            {
                _dispatcher.Invoke(() => handler());
                return "OK";
            });
        }

        private void EnsureStarted()
        {
            if (_app == null)
                throw new InvalidOperationException("StartAsync must be called first.");
        }

        public async ValueTask DisposeAsync()
        {
            await StopAsync();
        }
    }
}

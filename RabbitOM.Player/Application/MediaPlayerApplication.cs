using System;
using System.Windows;

namespace RabbitOM.Player.Application
{
    public sealed class MediaPlayerApplication : IApplication
    {
        private readonly MainWindow _window;

        public MediaPlayerApplication( MainWindow window ) // TODO: use an interface ???? it make sense to inject the main window into the global app object. it just make, may be we can inject a controller.
        {
            _window = window ?? throw new ArgumentNullException( nameof( window ) );
        }

        public Window Window
        {
            get => _window;
        }

        public void StartStreaming()
        {
            _window.StartStreamingCommand.Execute( null , _window );
        }

        public void StartStreaming( string uri )
        {
            _window.SelectedSource = uri;
            _window.StartStreamingCommand.Execute( null , _window );
        }

        public void StopStreaming()
        {
            _window.StopStreamingCommand.Execute( null , _window );
        }
    }
}

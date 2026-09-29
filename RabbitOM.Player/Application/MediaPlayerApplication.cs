using System;
using System.Windows;

namespace RabbitOM.Player.Application
{
    public sealed class MediaPlayerApplication : IApplication
    {
        private readonly MainWindow _window;

        public MediaPlayerApplication( MainWindow window )
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

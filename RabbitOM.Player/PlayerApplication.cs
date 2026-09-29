using System;
using System.Windows;

namespace RabbitOM.Player
{
    public sealed class PlayerApplication : IApplication
    {
        private readonly MainWindow _window;

        public PlayerApplication( MainWindow window )
        {
            _window = window ?? throw new ArgumentNullException( nameof( window ) );
        }

        public Window Window
        {
            get => _window;
        }

        public void StartStreaming()
        {
            throw new NotImplementedException();
        }

        public void StartStreaming( string uri )
        {
            throw new NotImplementedException();
        }

        public void StopStreaming()
        {
            throw new NotImplementedException();
        }
    }
}

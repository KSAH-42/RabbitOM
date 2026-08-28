using System;
using System.Windows;
using System.Windows.Controls;

namespace RabbitOM.Player.Controls
{
    public sealed class MediaControlHandler : IMediaControlHandler
    {
        private readonly Image _image;
        private readonly FrameworkElement _element;
        private readonly Action _communicationStartedHandler;
        private readonly Action _communicationStoppedHandler;
        private readonly Action _connectedHandler;
        private readonly Action _disconnectedHandler;
        private readonly Action _frameDecodedHandler;
        private readonly Action<string> _errorHandler;
        private readonly Action<Exception> _exceptionHandler;

        public MediaControlHandler( Image image , FrameworkElement element , Action communicationStartedHandler , Action communicationStoppedHandler , Action connectedHandler , Action disconnectedHandler , Action frameDecodedHandler , Action<string> errorHandler , Action<Exception> exceptionHandler )
        {
            _image = image ?? throw new ArgumentNullException( nameof( image ) );
            _element = element ?? throw new ArgumentNullException( nameof( element ) );
            
            _communicationStartedHandler = communicationStartedHandler ?? throw new ArgumentNullException( nameof( communicationStartedHandler ) );
            _communicationStoppedHandler = communicationStoppedHandler ?? throw new ArgumentNullException( nameof( communicationStoppedHandler ) );
            _connectedHandler = connectedHandler ?? throw new ArgumentNullException( nameof( connectedHandler ) );
            _disconnectedHandler = disconnectedHandler ?? throw new ArgumentNullException( nameof( disconnectedHandler ) );
            _frameDecodedHandler = frameDecodedHandler ?? throw new ArgumentNullException( nameof( frameDecodedHandler ) );
            _errorHandler = errorHandler ?? throw new ArgumentNullException( nameof( errorHandler ) );
            _exceptionHandler = exceptionHandler ?? throw new ArgumentNullException( nameof( exceptionHandler ) );
        }

        public Image Image
        {
            get => _image;
        }

        public void Dispatch( Action action )
        {
            _element.Dispatcher.BeginInvoke( System.Windows.Threading.DispatcherPriority.Render , action );
        }

        public void OnCommunicationStarted()
        {
            _communicationStartedHandler();
        }

        public void OnCommunicationStopped()
        {
            _communicationStoppedHandler();
        }

        public void OnConnected()
        {
            _connectedHandler();
        }

        public void OnDisconnected()
        {
            _disconnectedHandler();
        }

        public void OnFrameDecoded()
        {
            _frameDecodedHandler();
        }

        public void OnError( string error )
        {
            _errorHandler( error );
        }

        public void OnException( Exception exception )
        {
            _exceptionHandler( exception );
        }
    }
}

using System;
using System.Windows.Controls;

namespace RabbitOM.Player.Controls
{
    public interface IMediaControlHandler
    {
        Image Image { get; }

        void Dispatch( Action action );

        void OnCommunicationStarted();

        void OnCommunicationStopped();

        void OnConnected();

        void OnDisconnected();

        void OnFrameDecoded();

        void OnError( string error );

        void OnException( Exception exception );
    }
}

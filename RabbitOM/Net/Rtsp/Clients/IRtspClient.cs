using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public interface IRtspClient : IRtspClientEvents , IDisposable
    {
        object SyncRoot
        {
            get;
        }

        IRtspClientConfiguration Configuration
        {
            get;
        }

        bool IsConnected
        {
            get;
        }

        bool IsCommunicationStarted
        {
            get;
        }

        bool IsCommunicationStopping
        {
            get;
        }




        bool StartCommunication();

        void StopCommunication();

        void StopCommunication( TimeSpan shutdownTimeout );

        bool WaitForConnected( TimeSpan timeout );
    }
}

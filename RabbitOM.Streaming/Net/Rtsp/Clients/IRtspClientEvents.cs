using System;

namespace RabbitOM.Streaming.Net.Rtsp.Clients
{
    /// <summary>
    /// Represent the client event contract
    /// </summary>
    public interface IRtspClientEvents 
    {
        /// <summary>
        /// Raised when the communication has been started
        /// </summary>
        event EventHandler<RtspClientCommunicationStartedEventArgs> CommunicationStarted;

        /// <summary>
        /// Raised when the communication has been stopped
        /// </summary>
        event EventHandler<RtspClientCommunicationStoppedEventArgs> CommunicationStopped;

        /// <summary>
        /// Raised when the client is connected
        /// </summary>
        event EventHandler<RtspClientConnectedEventArgs>            Connected;

        /// <summary>
        /// Raised when the client is disconnected
        /// </summary>
        event EventHandler<RtspClientDisconnectedEventArgs>         Disconnected;

        /// <summary>
        /// Raise when an data has been received
        /// </summary>
        event EventHandler<RtspPacketReceivedEventArgs>             PacketReceived;

        /// <summary>
        /// Raise when an error occurs
        /// </summary>
        event EventHandler<RtspClientErrorEventArgs>                Error;
    }
}

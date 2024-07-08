using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    /// <summary>
    /// Represent the client event contract
    /// </summary>
    public interface IRTSPClientEvents 
    {
        /// <summary>
        /// Raised when the communication has been started
        /// </summary>
        event EventHandler<RTSPClientCommunicationStartedEventArgs> CommunicationStarted;

        /// <summary>
        /// Raised when the communication has been stopped
        /// </summary>
        event EventHandler<RTSPClientCommunicationStoppedEventArgs> CommunicationStopped;

        /// <summary>
        /// Raised when the client is connected
        /// </summary>
        event EventHandler<RTSPClientConnectedEventArgs>            Connected;

        /// <summary>
        /// Raised when the client is disconnected
        /// </summary>
        event EventHandler<RTSPClientDisconnectedEventArgs>         Disconnected;

        /// <summary>
        /// Raise when an data has been received
        /// </summary>
        event EventHandler<RTSPPacketReceivedEventArgs>             PacketReceived;

        /// <summary>
        /// Raise when an error occurs
        /// </summary>
        event EventHandler<RTSPClientErrorEventArgs>                Error;
    }
}

using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public interface IRtspClientEvents
    {
        event EventHandler<RtspClientCommunicationStartedEventArgs> CommunicationStarted;
        event EventHandler<RtspClientCommunicationStoppedEventArgs> CommunicationStopped;
        event EventHandler<RtspClientConnectedEventArgs> Connected;
        event EventHandler<RtspClientDisconnectedEventArgs> Disconnected;
        event EventHandler<RtspPacketReceivedEventArgs> PacketReceived;
        event EventHandler<RtspClientErrorEventArgs> Error;
    }
}

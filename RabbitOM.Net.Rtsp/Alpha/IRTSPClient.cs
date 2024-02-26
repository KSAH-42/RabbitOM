using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    internal interface IRTSPClient : IDisposable
    {
        event EventHandler<RTSPCommunicationStartedEventArgs> CommunicationStarted;
        event EventHandler<RTSPCommunicationStoppedEventArgs> CommunicationStopped;
        event EventHandler<RTSPConnectedEventArgs> Connected;
        event EventHandler<RTSPDisconnectedEventArgs> Disconnected;
        event EventHandler<RTSPStreamingSetupEventArgs> StreamingSetup;
        event EventHandler<RTSPStreamingStartedEventArgs> StreamingStarted;
        event EventHandler<RTSPStreamingStoppedEventArgs> StreamingStopped;
        event EventHandler<RTSPStreamingStatusChangedEventArgs> StreamingStatusChanged;
        event EventHandler<RTSPMessageReceivedEventArgs> MessageReceived;
        event EventHandler<RTSPPacketReceivedEventArgs> PacketReceived;
        event EventHandler<RTSPErrorEventArgs> Error;


        object SyncRoot { get; }
        RTSPPipeLineCollection PipeLines { get; }
        IRTSPClientConfiguration Configuration { get; }
        bool IsCommunicationStarted { get; }
        bool IsConnected { get; }
        bool IsStreamingStarted { get; }
        bool IsReceivingPacket { get; }
        bool IsDisposed { get; }


        bool StartCommunication();
        void StopCommunication();
        void StopCommunication(TimeSpan shutdownTimeout);
    }
}

using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers
{
    using RabbitOM.Streaming.Net.RtspV2.Receivers.Events;

    public interface IRtspReceiver
    {
        event EventHandler<RtspCommunicationStartedEventArgs> CommunicationStarted;
        event EventHandler<RtspCommunicationStoppedEventArgs> CommunicationStopped;
        event EventHandler<RtspConnectedEventArgs> Connected;
        event EventHandler<RtspDisconnectedEventArgs> Disconnected;
        event EventHandler<RtspStreamingStartedEventArgs> StreamingStarted;
        event EventHandler<RtspStreamingStoppedEventArgs> StreamingStopped;
        event EventHandler<RtspStreamingStatusChangedEventArgs> StreamingStatusChanged;
        event EventHandler<RtspDataReceivedEventArgs> DataReceived;
        event EventHandler<RtspErrorEventArgs> Error;




        bool IsCommunicationStarted { get; }
        bool IsCommunicationStopping { get; }
        bool IsConnected { get; }
        bool IsStreamingStarted { get; }
        bool IsReceivingData { get; }
        



        bool StartCommunication();
        void StopCommunication();
        void StopCommunication(TimeSpan timeout);
    }
}

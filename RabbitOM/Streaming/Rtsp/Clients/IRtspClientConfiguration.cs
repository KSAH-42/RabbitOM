using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public interface IRtspClientConfiguration
    {
        object SyncRoot
        {
            get;
        }

        string Uri
        {
            get;
            set;
        }

        string UserName
        {
            get;
            set;
        }

        string Password
        {
            get;
            set;
        }

        TimeSpan ReceiveTimeout
        {
            get;
            set;
        }

        TimeSpan SendTimeout
        {
            get;
            set;
        }

        RtspKeepAliveType KeepAliveType
        {
            get;
            set;
        }

        TimeSpan RetriesInterval
        {
            get;
            set;
        }

        TimeSpan KeepAliveInterval
        {
            get;
            set;
        }

        RtspMediaFormat MediaFormat
        {
            get;
            set;
        }

        RtspDeliveryMode DeliveryMode
        {
            get;
            set;
        }

        int RtpPort
        {
            get;
            set;
        }

        string MulticastAddress
        {
            get;
            set;
        }

        byte TimeToLive
        {
            get;
            set;
        }

        void ToDefault();
    }
}

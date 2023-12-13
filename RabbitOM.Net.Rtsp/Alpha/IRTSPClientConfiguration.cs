using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public interface IRTSPClientConfiguration
    {
        object SyncRoot { get; }
        string Uri { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        TimeSpan ReceiveTimeout { get; set; }
        TimeSpan ReceiveTransportTimeout { get; set; }
        TimeSpan SendTimeout { get; set; }
        TimeSpan KeepAliveInterval { get; set; }
        TimeSpan RetriesInterval { get; set; }
        TimeSpan RetriesTransportInterval { get; set; }
        string MulticastAddress { get; set; }
        int RtpPort { get; set; }
        byte TimeToLive { get; set; }
        RTSPMediaFormat MediaFormat { get; set; }
        RTSPKeepAliveType KeepAliveType { get; set; }
        RTSPDeliveryMode DeliveryMode { get; set; }
        RTSPHeaderCollection OptionsHeaders { get; }
        RTSPHeaderCollection DescribeHeaders { get; }
        RTSPHeaderCollection SetupHeaders { get; }
        RTSPHeaderCollection PlayHeaders { get; }
        RTSPHeaderCollection TearDownHeaders { get; }
        RTSPHeaderCollection KeepAliveHeaders { get; }
    }
}

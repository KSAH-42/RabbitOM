using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{

    public interface IRtspClientChannel : IDisposable
    {        
        string EndPoint { get; set; }
        
        TimeSpan ReceiveTimeout { get; set; }
        
        TimeSpan SendTimeout { get; set; }

        bool IsOpened { get; }




        void Open();
        
        void Close();

        RtspResponseMessage SendMessage( RtspRequestMessage request );
    }
}

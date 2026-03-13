using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public interface IClientChannel : IDisposable
    {        
        string Address { get; set; }
        
        TimeSpan ReceiveTimeout { get; set; }
        
        TimeSpan SendTimeout { get; set; }

        bool IsOpened { get; }




        void Open();
        
        void Close();

        void Abort();

        RtspResponseMessage SendMessage( RtspRequestMessage request );
    }
}

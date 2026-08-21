using System;

namespace RabbitOM.Streaming.RtspV2.Transports
{
    public sealed class UdpRtspReceiver : IReceiver
    {
        public bool IsOpened
        {
            get => throw new NotImplementedException( "to be implemented" );
        }

        public void Open()
        {
            throw new NotImplementedException( "to be implemented" );
        }

        public void Close()
        {
            throw new NotImplementedException( "to be implemented" );
        }

        public void Dispose()
        {
            throw new NotImplementedException( "to be implemented" );
        }

        public int Receive( byte[] buffer , int offset , int count )
        {
            throw new NotImplementedException( "to be implemented" );
        }
    }
}

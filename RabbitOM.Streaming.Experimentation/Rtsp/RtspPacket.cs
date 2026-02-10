using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public struct RtspPacket
    {
        private readonly byte[] _buffer;
        
        private readonly int _channel;



        public RtspPacket( byte[] buffer , int channel )
        {
            if ( buffer == null )
            {
                throw new ArgumentNullException( nameof( buffer ) );
            }

            if ( buffer.Length <= 0 )
            {
                throw new ArgumentException( nameof( buffer ) );
            }

            if ( channel < 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( channel ) );
            }

            _buffer = buffer;
            _channel = channel;
        }




        public byte[] Buffer { get => _buffer; }

        public int Channel { get => _channel; }
    }
}

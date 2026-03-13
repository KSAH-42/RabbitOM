using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public struct InterleavedPacket
    {
        public InterleavedPacket( byte[] buffer , int channel )
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

            Buffer = buffer;
            Channel = channel;
        }




        public byte[] Buffer { get; }

        public int Channel { get; }
    }
}

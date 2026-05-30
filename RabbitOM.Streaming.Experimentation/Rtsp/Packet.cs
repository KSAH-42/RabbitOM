using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    // TODO: /!\ mirror type here

    public struct Packet
    {
        public Packet( byte[] buffer ) : this ( buffer , 0 )
        {
        }

        public Packet( byte[] buffer , int channel )
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





        public static bool IsNullOrEmpty( in Packet packet )
        {
            return packet.Buffer == null || packet.Buffer.Length == 0;
        }
    }
}

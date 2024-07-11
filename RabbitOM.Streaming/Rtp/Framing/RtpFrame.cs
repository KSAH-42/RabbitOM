using System;

namespace RabbitOM.Streaming.Rtp.Framing
{
    public class RtpFrame
    {
        private readonly byte[] _data;



        public RtpFrame( byte[] data )
        {
            if ( data == null )
            {
                throw new ArgumentNullException( nameof( data ) );
            }

            if ( data.Length == 0 )
            {
                throw new ArgumentException( nameof( data ) );
            }

            _data = data;
        }




        public byte[] Data
        {
            get => _data;
        }
    }
}
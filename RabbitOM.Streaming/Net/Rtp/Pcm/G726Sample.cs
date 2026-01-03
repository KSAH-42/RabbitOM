using System;

namespace RabbitOM.Streaming.Net.Rtp.Pcm
{
    public class G726Sample : RtpSample
    {
        private G726Sample( byte[] data , G726BitRate bitrate , int numberOfBits ) 
            : base( data )
        {
            BitRate = bitrate;
            NumberOfBits = numberOfBits;
        }



        public G726BitRate BitRate { get; }

        public int NumberOfBits { get; }        



        public static G726Sample NewSample( byte[] data , G726BitRate bitrate )
        {
            switch ( bitrate )
            {
                case G726BitRate.Format_16000: return new G726Sample( data , bitrate , 2 );
                case G726BitRate.Format_24000: return new G726Sample( data , bitrate , 3 );
                case G726BitRate.Format_32000: return new G726Sample( data , bitrate , 4 );
                case G726BitRate.Format_40000: return new G726Sample( data , bitrate , 5 );
            }

            throw new NotSupportedException();
        }
    }
}
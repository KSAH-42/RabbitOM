using System;

namespace RabbitOM.Streaming.Net.Rtp.Pcm
{
    public class G726SampleMediaElement : RtpMediaElement
    {
        private G726SampleMediaElement( G726BitRate bitrate , int numberOfBits , byte[] data ) : base( data )
        {
            BitRate = bitrate;
            NumberOfBits = numberOfBits;
        }


        public G726BitRate BitRate { get; }

        public int NumberOfBits { get; }


        public static G726SampleMediaElement NewSample( byte[] data , G726BitRate bitrate )
        {
            switch ( bitrate )
            {
                case G726BitRate.Format_16000: return new G726SampleMediaElement( bitrate , 2 , data );
                case G726BitRate.Format_24000: return new G726SampleMediaElement( bitrate , 3 , data );
                case G726BitRate.Format_32000: return new G726SampleMediaElement( bitrate , 4 , data );
                case G726BitRate.Format_40000: return new G726SampleMediaElement( bitrate , 5 , data );
            }

            throw new NotSupportedException();
        }
    }
}
using System;

namespace RabbitOM.Streaming.Net.Rtp.Pcm
{
    public class G726SampleMediaElement : RtpMediaElement
    {
        public G726SampleMediaElement( G726BitRate bitrate , byte[] data ) : base( data )
            => BitRate = bitrate;

        public G726BitRate BitRate { get; }

        public static int CountBits( G726SampleMediaElement sample )
        {
            if ( sample == null )
            {
                throw new ArgumentNullException( nameof( sample ) );
            }

            switch ( sample.BitRate )
            {
                case G726BitRate.BitRate_16000: return 2;
                case G726BitRate.BitRate_24000: return 3;
                case G726BitRate.BitRate_32000: return 4;
                case G726BitRate.BitRate_40000: return 5;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
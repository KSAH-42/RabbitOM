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

            if ( sample.Buffer == null || sample.Buffer.Length == 0 )
            {
                return 0;
            }

            switch ( sample.BitRate )
            {
                case G726BitRate.Format_16000: return 2;
                case G726BitRate.Format_24000: return 3;
                case G726BitRate.Format_32000: return 4;
                case G726BitRate.Format_40000: return 5;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
using System;
using System.Text;

namespace RabbitOM.Net.Sdp.Tests.ConsoleApp
{
    static class SdpSampleFactory
    {
        public static string CreateSimpleSdp()
        {
            var builder = new StringBuilder();

            builder.AppendLine( " v=0 " );
            builder.AppendLine( " o = jdoe 2890844526 2890842807 IN IP4 10.47.16.5     " );
            builder.AppendLine( " s = SDP Seminar                                       " );
            builder.AppendLine( "   i = A Seminar on the session description protocol " );
            builder.AppendLine( " u =     http://www.example.com/seminars/sdp.pdf   ; " );
            builder.AppendLine( " e = j.doe@example.com " );
            builder.AppendLine( " c = IN IP4 224.2.17.12 / 127   " );
            builder.AppendLine( " t = 2873397496 2873404696     " );
            builder.AppendLine( " a = recvonly : data                   " );
            builder.AppendLine( " m =audio 49170 RTP / AVP 10  " );
            builder.AppendLine( " m = video 51372 RTP / AVP 99" );
            builder.AppendLine( " m = video 51373/2 RTP/AVP 99" );
            builder.AppendLine( " m =      video       51373/     2 RTP    /      AVP 99" );
            builder.AppendLine( " a = rtpmap:99 h263 - 1998 / 90000 " );

            return builder.ToString();
        }
    }
}

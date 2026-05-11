using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    public sealed class RtpInfoRtspHeaderValue
    {
        public RtpInfoRtspHeaderValueCollection RtpInfos { get; } = new RtpInfoRtspHeaderValueCollection();

        public override string ToString()
        {
            return string.Join( ", " , RtpInfos );
        }

        public static bool TryParse( string input , out RtpInfoRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new RtpInfoRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( Types.RtpInfoRtspHeaderValue.TryParse( token , out var info ) )
                    {
                        header.RtpInfos.TryAdd( info );
                    }
                }

                if ( header.RtpInfos.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}

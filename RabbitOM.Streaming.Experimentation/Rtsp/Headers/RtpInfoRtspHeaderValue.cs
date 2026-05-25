using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;

    public sealed class RtpInfoRtspHeaderValue
    {
        public RtpInfoRtspHeaderValueCollection RtpInfos { get; } = new RtpInfoRtspHeaderValueCollection();

        public static bool TryParse( string input , out RtpInfoRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new RtpInfoRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( DataTypes.RtpInfoRtspHeaderValue.TryParse( token , out var info ) )
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

        public override string ToString()
        {
            return string.Join( ", " , RtpInfos );
        }
    }
}

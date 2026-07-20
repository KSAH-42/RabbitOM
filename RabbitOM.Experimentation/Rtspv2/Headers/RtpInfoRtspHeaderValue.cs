using System;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    using RabbitOM.Streaming.RtspV2.Headers.DataTypes;

    public sealed class RtpInfoRtspHeaderValue
    {
        public RtpInfoSourceRtspHeaderValueCollection Values { get; } = new RtpInfoSourceRtspHeaderValueCollection();

        public static bool TryParse( string input , out RtpInfoRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new RtpInfoRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( RtpInfoSourceRtspHeaderValue.TryParse( token , out var info ) )
                    {
                        header.Values.TryAdd( info );
                    }
                }

                if ( header.Values.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.Join( ", " , Values );
        }
    }
}

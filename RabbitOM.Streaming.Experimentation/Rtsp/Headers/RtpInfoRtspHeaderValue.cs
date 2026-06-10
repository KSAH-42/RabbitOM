using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;

    public sealed class RtpInfoRtspHeaderValue
    {
        public TrackRtspHeaderValueCollection Tracks { get; } = new TrackRtspHeaderValueCollection();

        public static bool TryParse( string input , out RtpInfoRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new RtpInfoRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( TrackRtspHeaderValue.TryParse( token , out var info ) )
                    {
                        header.Tracks.TryAdd( info );
                    }
                }

                if ( header.Tracks.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.Join( ", " , Tracks );
        }
    }
}

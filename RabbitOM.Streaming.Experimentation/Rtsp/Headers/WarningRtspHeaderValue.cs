using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;
    
    public sealed class WarningRtspHeaderValue
    {
        public WarningInfoRtspHeaderValueCollection Values { get; } = new WarningInfoRtspHeaderValueCollection();
        
        public override string ToString()
        {
            return string.Join( ", " , Values );
        }

        public static bool TryParse( string input , out WarningRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new WarningRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( DataTypes.WarningInfoRtspHeaderValue.TryParse( token , out var warning ) )
                    {
                        header.Values.TryAdd( warning );
                    }
                }

                if ( header.Values.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}

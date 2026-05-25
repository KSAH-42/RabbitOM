using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;
    
    public sealed class WarningRtspHeaderValue
    {
        public WarningFieldRtspHeaderValueCollection Fields { get; } = new WarningFieldRtspHeaderValueCollection();
        
        public override string ToString()
        {
            return string.Join( ", " , Fields );
        }

        public static bool TryParse( string input , out WarningRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new WarningRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( DataTypes.WarningFieldRtspHeaderValue.TryParse( token , out var warning ) )
                    {
                        header.Fields.TryAdd( warning );
                    }
                }

                if ( header.Fields.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}

using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
    
    public sealed class WarningRtspHeaderValue
    {
        public WarningInfoRtspHeaderValueCollection Infos { get; } = new WarningInfoRtspHeaderValueCollection();
        
        public override string ToString()
        {
            return string.Join( ", " , Infos );
        }

        public static bool TryParse( string input , out WarningRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new WarningRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( WarningInfo.TryParse( token , out var warning ) )
                    {
                        header.Infos.TryAdd( warning );
                    }
                }

                if ( header.Infos.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}

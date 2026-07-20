using System;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    using RabbitOM.Streaming.RtspV2.Headers.DataTypes;

    public sealed class AllowRtspHeaderValue
    {
        public RtspMethodHeaderValueCollection Methods { get; } = new RtspMethodHeaderValueCollection();

        public static bool TryParse( string input , out AllowRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AllowRtspHeaderValue();

                foreach( var token in tokens )
                {
                    if ( RtspMethod.TryParse( token , out var method ) )
                    {
                        header.Methods.TryAdd( method );
                    }
                }

                if ( header.Methods.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.Join( ", " , Methods );
        }
    }
}

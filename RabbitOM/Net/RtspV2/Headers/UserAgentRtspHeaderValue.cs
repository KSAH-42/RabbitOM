using System;

namespace RabbitOM.Net.RtspV2.Headers
{
    using RabbitOM.Net.RtspV2.Headers.DataTypes;

    public sealed class UserAgentRtspHeaderValue
    {
        public RtspHeaderValueCollection<ProductInfoRtspHeaderValue> Values { get; } = new RtspHeaderValueCollection<ProductInfoRtspHeaderValue>();

        public static bool TryParse( string input , out UserAgentRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new UserAgentRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( ProductInfoRtspHeaderValue.TryParse( token , out var element ) )
                    {
                        header.Values.TryAdd( element );
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

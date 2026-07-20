using System;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    using RabbitOM.Streaming.RtspV2.Headers.DataTypes;

    // TODO: add unit test to detect that implicit operator( string type) is not implemented, it should be block, because it lets to people to think what we can write every thing, lets the developer to use add( string name, string value) method instead

    public sealed class AcceptRtspHeaderValue
    {
        public MediaTypeWithQualityRtspHeaderValueCollection Values { get; } = new MediaTypeWithQualityRtspHeaderValueCollection();

        public static bool TryParse( string input , out AcceptRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AcceptRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( MediaTypeWithQualityRtspHeaderValue.TryParse( token , out var element ) )
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

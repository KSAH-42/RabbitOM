using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances;

    public sealed class AllowRtspHeaderValue
    {
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;
        private static readonly StringValueValidator ValueValidator = StringValueValidator.DefaultValidator;

        public StringCollection Methods { get; } = new StringCollection( IsValidMethod );
        
        public override string ToString()
        {
            return string.Join( ", " , Methods );
        }

        public static bool IsValidMethod( string value )
        {
            return ValueValidator.TryValidate( value ) && RtspMethod.TryParse( value , out _ );
        }

        public static bool TryParse( string input , out AllowRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AllowRtspHeaderValue();

                foreach( var token in tokens )
                {
                    header.Methods.TryAdd( token );
                }

                if ( header.Methods.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}

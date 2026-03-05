using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class RefererRtspHeader : RtspHeader
    {
        public static string TypeName { get; } = "Referer";
        
        

        
        
        public string Uri { get; private set; } = string.Empty;

        
        
        


        public void SetUri( string value )
        {
            Uri = StringRtspHeaderParser.TrimValue( value , StringRtspHeaderParser.SpaceWithQuotesChars );
        }





        
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace( Uri ) ? string.Empty : Uri ;
        }

        public static bool TryParse( string input , out RefererRtspHeader result )
        {
            result = null;

            var value = StringRtspHeaderParser.TrimValue( input );

            if ( ! string.IsNullOrWhiteSpace( value ) )
            {
                result = new RefererRtspHeader();
                result.SetUri( value );
            }

            return result != null;
        }
    }
}

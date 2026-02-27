using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ContentBaseRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Content-Base";
        
        

        
        
        public Uri Uri { get; private set; }

        
        
        


        public void SetUri( Uri value )
        {
            Uri = value;
        }

        public void SetUri( string value )
        {
            Uri = UriRtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( value ) , out var result )
                ? result
                : null
                ;
        }
        
        public override string ToString()
        {
            return Uri?.ToString() ?? string.Empty;
        }

        



        
        
        public static bool TryParse( string input , out LocationRtspHeaderValue result )
        {
            result = null;

            if ( UriRtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , out Uri uri ) )
            {
                var header = new LocationRtspHeaderValue();

                header.SetUri( StringRtspHeaderNormalizer.Normalize( input ) );

                if ( header.Uri != null )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}

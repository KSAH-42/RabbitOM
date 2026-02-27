using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ContentBaseRtspHeader
    {
        public static readonly string TypeName = "Content-Base";
        
        

        
        
        public Uri Uri { get; private set; }

        
        
        


        public void SetUri( Uri value )
        {
            Uri = value;
        }

        public void SetUri( string value )
        {
            Uri = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return;
            }

            if ( Uri.TryCreate( RtspHeaderValueNormalizer.Normalize( value ) , UriKind.RelativeOrAbsolute , out var result ) )
            {
                Uri = result;
            }
        }
        
        public override string ToString()
        {
            return Uri?.ToString() ?? string.Empty;
        }

        



        
        
        public static bool TryParse( string input , out LocationRtspHeader result )
        {
            result = null;

            if ( UriRtspHeaderParser.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , out Uri uri ) )
            {
                var header = new LocationRtspHeader();

                header.SetUri( RtspHeaderValueNormalizer.Normalize( input ) );

                if ( header.Uri != null )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}

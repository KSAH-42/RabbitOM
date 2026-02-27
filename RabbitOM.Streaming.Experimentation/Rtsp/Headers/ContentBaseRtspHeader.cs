using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ContentBaseRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Content-Base";
        
        

        
        
        public string Uri { get; private set; } = string.Empty;

        
        
        


        public void SetUri( string value )
        {
            Uri = StringRtspHeaderNormalizer.Normalize( value );
        }
        
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace( Uri ) ? string.Empty : Uri;
        }

        



        
        
        public static bool TryParse( string input , out LocationRtspHeader result )
        {
            result = null;

            var value = StringRtspHeaderNormalizer.Normalize( input );

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new LocationRtspHeader();
            
            result.SetUri( value );

            return true;
        }
    }
}

using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ContentBaseRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Content-Base";
        
        

        
        
        public string Uri { get; private set; } = string.Empty;

        
        
        


        public void SetUri( string value )
        {
            Uri = RtspHeaderParser.Formatter.Filter( value );
        }
        
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace( Uri ) ? string.Empty : Uri;
        }

        



        
        
        public static bool TryParse( string input , out ContentBaseRtspHeader result )
        {
            result = null;

            var value = RtspHeaderParser.Formatter.Filter( input );

            if ( ! string.IsNullOrWhiteSpace( value ) )
            {
                result = new ContentBaseRtspHeader();        
                result.SetUri( value );
            }

            return result != null;
        }
    }
}

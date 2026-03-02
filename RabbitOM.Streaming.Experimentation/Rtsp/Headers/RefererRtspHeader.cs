using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class RefererRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Referer";
        
        

        
        
        public string Uri { get; private set; } = string.Empty;

        
        
        


        public void SetUri( string value )
        {
            Uri = RtspHeaderParser.Formatter.Filter( value );
        }





        
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace( Uri ) ? string.Empty : Uri ;
        }

        public static bool TryParse( string input , out RefererRtspHeader result )
        {
            result = null;

            var value = RtspHeaderParser.Formatter.Filter( input );

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new RefererRtspHeader();

            result.SetUri( value );

            return true;
        }
    }
}

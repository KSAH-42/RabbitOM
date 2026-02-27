using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class RefererRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Referer";
        
        

        
        
        public string Uri { get; private set; } = string.Empty;

        
        
        


        public void SetUri( string value )
        {
            Uri = StringRtspHeaderNormalizer.Normalize( value );
        }





        
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace( Uri ) ? string.Empty : Uri ;
        }

        public static bool TryParse( string input , out RefererRtspHeaderValue result )
        {
            result = null;

            var value = StringRtspHeaderNormalizer.Normalize( input );

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new RefererRtspHeaderValue();

            result.SetUri( value );

            return true;
        }
    }
}

using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class LocationRtspHeader : RtspHeader 
    {
        public const string TypeName = "Location";
        




        private Uri _uri;
        




        public Uri Uri
        {
            get => _uri;
            set => _uri = value;
        }




        public override bool TryValidate()
        {
            return _uri != null;
        }

        public override string ToString()
        {
            return _uri?.ToString() ?? string.Empty;
        }
        




        public static bool TryParse( string value , out LocationRtspHeader result )
        {
            result = null;

            if ( ! Uri.TryCreate( value , UriKind.RelativeOrAbsolute , out var uri ) )
            {
                return false;
            }

            result = new LocationRtspHeader() { Uri = uri };

            return true;
        }
    }
}

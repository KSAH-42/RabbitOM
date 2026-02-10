using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class MediaRangeRtspHeader : RtspHeader 
    {
        public const string TypeName = "Media-Range";
        




        private string _value;
        



        public string Value
        {
            get => _value ?? string.Empty;
            set => _value = value;
        }



        public override bool TryValidate()
        {
            return ! string.IsNullOrWhiteSpace( _value );
        }

        public override string ToString()
        {
            return _value ?? string.Empty;
        }
        



        public static bool TryParse( string value , out MediaRangeRtspHeader result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new MediaRangeRtspHeader() { Value = value };

            return true;
        }
    }
}

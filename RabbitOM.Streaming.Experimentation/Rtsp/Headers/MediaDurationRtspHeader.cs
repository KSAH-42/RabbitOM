using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class MediaDurationRtspHeader : RtspHeader 
    {
        public const string TypeName = "Media-Duration";
        




        private double _duration;
        



        public double Duration
        {
            get => _duration;
            set => _duration = value;
        }




        public override bool TryValidate()
        {
            return true;
        }

        public override string ToString()
        {
            return string.Format( CultureInfo.InvariantCulture , "{0:F3}" , _duration );
        }
        





        public static bool TryParse( string value , out MediaDurationRtspHeader result )
        {
            result = null;

            if ( ! double.TryParse( value , NumberStyles.Float, CultureInfo.InvariantCulture , out var duration ) )
            {
                return false;
            }

            result = new MediaDurationRtspHeader() { Duration = duration };

            return true;
        }
    }
}

using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class AcceptRtspHeader : RtspHeader 
    {
        public const string TypeName = "Accept";




        private readonly List<StringWithQualityRtspHeaderValue> _mimes = new List<StringWithQualityRtspHeaderValue>();
        



        public IReadOnlyList<StringWithQualityRtspHeaderValue> Mimes
        {
            get => _mimes;
        }
        



        
        public override bool TryValidate()
        {
            return Mimes.Count > 0;
        }

        public bool TryAddMime( StringWithQualityRtspHeaderValue value )
        {
            if ( value == null )
            {
                return false;
            }
            
            _mimes.Add( value );

            return true;
        }

        public void RemoveMime( StringWithQualityRtspHeaderValue value )
        {
            _mimes.Remove( value );
        }

        public void RemoveMimes()
        {
            _mimes.Clear();
        }
        
        public override string ToString()
        {
            return string.Join( ", " , _mimes );
        }
        





        public static bool TryParse( string value , out AcceptRtspHeader result )
        {
            result = null;

            // pattern: application/sdp , application/sdp; q=2, application/rtsl, application/mheg

            throw new NotImplementedException();
        }
    }
}

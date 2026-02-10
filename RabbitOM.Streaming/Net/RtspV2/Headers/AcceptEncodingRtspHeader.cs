using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public class AcceptEncodingRtspHeader : RtspHeader 
    {
        public const string TypeName = "Accept-Encoding";

        private readonly HashSet<StringWithQualityRtspHeaderValue> _encodings = new HashSet<StringWithQualityRtspHeaderValue>();

        public IReadOnlyCollection<StringWithQualityRtspHeaderValue> Encodings
        {
            get => _encodings;
        }

        public override bool TryValidate()
        {
            return _encodings.Count > 0;
        }

        public bool TryAddEncoding( StringWithQualityRtspHeaderValue value )
        {
            if ( StringWithQualityRtspHeaderValue.IsNullOrEmpty( value ) )
            {
                return false;
            }

            return _encodings.Add( value );
        }

        public void RemoveEncoding( StringWithQualityRtspHeaderValue value )
        {
            _encodings.Remove( value );
        }

        public void RemoveAllEncodings()
        {
            _encodings.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _encodings );
        }

        public static bool TryParse( string input , out AcceptEncodingRtspHeader result )
        {
            result = null;

            if ( ! RtspHeaderParser.TryParse( input , "," , out var tokens ) )
            {
                return false;
            }

            var header = new AcceptEncodingRtspHeader();

            foreach ( var token in tokens )
            {
                if ( StringWithQualityRtspHeaderValue.TryParse( token , out var encoding ) )
                {
                    header.TryAddEncoding( encoding );
                }
            }
            
            if ( header.Encodings.Count <= 0 )
            {
                return false;
            }

            result = header;

            return true;
        }
    }
}

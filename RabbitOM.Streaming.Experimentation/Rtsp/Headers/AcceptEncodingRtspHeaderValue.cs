using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class AcceptEncodingRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Accept-Encoding";


        private readonly Dictionary<string,StringWithQualityRtspHeaderValue> _encodings = new Dictionary<string,StringWithQualityRtspHeaderValue>( StringComparer.OrdinalIgnoreCase );


        public IReadOnlyCollection<StringWithQualityRtspHeaderValue> Encodings
        {
            get => _encodings.Values;
        }


        public static bool TryParse( string input , out AcceptEncodingRtspHeaderValue result )
        {
            result = null;
            
            if ( RtspHeaderParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AcceptEncodingRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( StringWithQualityRtspHeaderValue.TryParse( token , out var encoding ) )
                    {
                        header.AddEncoding( encoding );
                    }
                }
            
                if ( header.Encodings.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
        

        public bool AddEncoding( StringWithQualityRtspHeaderValue encoding )
        {
            return AddEncoding( encoding , RtspHeaderProtocolValidator.IsValidEncoding );
        }
        
        public bool AddEncoding( StringWithQualityRtspHeaderValue encoding , Func<string,bool> validator )
        {
            if ( StringWithQualityRtspHeaderValue.IsNullOrEmpty( encoding ) )
            {
                return false;
            }

            if ( validator == null || ! validator.Invoke( encoding.Value ) )
            {
                return false;
            }

            if ( _encodings.ContainsKey( encoding.Value ) )
            {
                return false;
            }

            _encodings[ encoding.Value ] = encoding;
            return true;
        }

        public bool RemoveEncoding( StringWithQualityRtspHeaderValue encoding )
        {
            if ( StringWithQualityRtspHeaderValue.IsNullOrEmpty( encoding ) || ! _encodings.ContainsValue( encoding ) )
            {
                return false;
            }

            return _encodings.Remove( encoding.Value );
        }

        public bool RemoveEncodingBy( Func<StringWithQualityRtspHeaderValue,bool> predicate )
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

            return _encodings.Remove( _encodings.Values.FirstOrDefault( predicate )?.Value ?? string.Empty );
        }

        public void RemoveEncodings()
        {
            _encodings.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _encodings.Select( element => element.Value.ToString() ) );
        }
    }
}

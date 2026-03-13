using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class AcceptEncodingRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Accept-Encoding";


        private readonly Dictionary<string,WeightedString> _encodings = new Dictionary<string,WeightedString>( StringComparer.OrdinalIgnoreCase );


        public IReadOnlyCollection<WeightedString> Encodings
        {
            get => _encodings.Values;
        }


        public static bool TryParse( string input , out AcceptEncodingRtspHeader result )
        {
            result = null;
            
            if ( RtspHeaderParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AcceptEncodingRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( WeightedString.TryParse( token , out var encoding ) )
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
        
        
        public bool AddEncoding( WeightedString encoding )
        {
            if ( WeightedString.IsNullOrEmpty( encoding ) || ! SupportedTypes.Encodings.Contains( encoding.Value ) )
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

        public bool RemoveEncoding( WeightedString encoding )
        {
            if ( WeightedString.IsNullOrEmpty( encoding ) || ! _encodings.ContainsValue( encoding ) )
            {
                return false;
            }

            return _encodings.Remove( encoding.Value );
        }

        public bool RemoveEncodingBy( Func<WeightedString,bool> predicate )
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

            return _encodings.Remove( _encodings.Values.FirstOrDefault( predicate )?.Value ?? string.Empty );
        }

        public void ClearEncodings()
        {
            _encodings.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _encodings.Values );
        }
    }
}

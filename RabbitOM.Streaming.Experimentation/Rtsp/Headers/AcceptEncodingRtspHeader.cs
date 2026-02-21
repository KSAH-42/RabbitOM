using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class AcceptEncodingRtspHeader
    {
        public static readonly string TypeName = "Accept-Encoding";



        private readonly HashSet<StringRtspHeader> _encodings = new HashSet<StringRtspHeader>();



        public IReadOnlyCollection<StringRtspHeader> Encodings
        {
            get => _encodings;
        }



        public static bool TryParse( string input , out AcceptEncodingRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( input ) , ',' , out var tokens ) )
            {
                var header = new AcceptEncodingRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( StringRtspHeader.TryParse( token , out var encoding ) )
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




        public bool AddEncoding( StringRtspHeader encoding )
        {
            if ( StringRtspHeader.IsNullOrEmpty( encoding ) )
            {
                return false;
            }

            return _encodings.Add( encoding );
        }

        public bool RemoveEncoding( StringRtspHeader encoding )
        {
            return _encodings.Remove( encoding );
        }

        public void RemoveEncodings()
        {
            _encodings.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _encodings );
        }
    }
}

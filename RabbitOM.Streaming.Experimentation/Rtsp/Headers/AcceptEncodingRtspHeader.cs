
using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class AcceptEncodingRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Accept-Encoding";

        public static readonly IReadOnlyCollection<string> SupportedEncodings = new HashSet<string>( StringComparer.OrdinalIgnoreCase )
        {
            "zip",
            "tar",
            "gzip",
            "identity" ,
            "deflate" ,
            "br",
            "*",
        };







        private readonly HashSet<StringWithQuality> _encodings = new HashSet<StringWithQuality>();







        public IReadOnlyCollection<StringWithQuality> Encodings
        {
            get => _encodings;
        }









        public bool AddEncoding( StringWithQuality encoding )
        {
            if ( StringWithQuality.IsNullOrEmpty( encoding ) )
            {
                return false;
            }

            if ( SupportedEncodings.Contains( encoding.Name ) )
            {
                return _encodings.Add( encoding );
            }

            return false;
        }

        public bool RemoveEncoding( StringWithQuality encoding )
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








        public static bool TryParse( string input , out AcceptEncodingRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                var header = new AcceptEncodingRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( StringWithQuality.TryParse( token , out var encoding ) )
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
    }
}

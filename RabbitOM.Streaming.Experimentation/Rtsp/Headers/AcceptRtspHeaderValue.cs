using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class AcceptRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Accept";
        
        public static readonly IReadOnlyCollection<string> SupportedFormats = new HashSet<string>( StringComparer.OrdinalIgnoreCase )
        {
            "application/sdp",
            "application/text" ,
            "application/xml" ,
            "application/json" ,
            "application/parameters" ,
            "application/binary" ,
            "text" ,
            "text/sdp" ,
            "text/xml" ,
            "text/json" ,
            "text/plain" ,
            "text/parameters" ,
            "sdp" ,
            "xml" ,
            "json" ,
            "binary" ,
        };






        private readonly HashSet<StringWithQuality> _mimes = new HashSet<StringWithQuality>();
        




        public IReadOnlyCollection<StringWithQuality> Mimes
        {
            get => _mimes;
        }
        




        public bool AddMime( StringWithQuality mime )
        {
            if ( StringWithQuality.IsNullOrEmpty( mime ) )
            {
                return false;
            }

            if ( SupportedFormats.Contains( mime.Name ) )
            {
                return _mimes.Add( mime );
            }
            
            return false;
        }

        public void RemoveMime( StringWithQuality mime )
        {
            _mimes.Remove( mime );
        }

        public void RemoveMimes()
        {
            _mimes.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _mimes );
        }



        
        
        
        public static bool TryParse( string input , out AcceptRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                var header = new AcceptRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( StringWithQuality.TryParse( token , out var mime ) )
                    {
                        header.AddMime( mime );
                    }
                }
            
                if ( header.Mimes.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}

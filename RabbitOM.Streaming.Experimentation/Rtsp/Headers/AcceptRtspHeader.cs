using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    public sealed class AcceptRtspHeader : RtspHeader
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






        private readonly HashSet<WeightedString> _mimes = new HashSet<WeightedString>();
        




        public IReadOnlyCollection<WeightedString> Mimes
        {
            get => _mimes;
        }
        




        public bool AddMime( WeightedString mime )
        {
            if ( WeightedString.IsNullOrEmpty( mime ) )
            {
                return false;
            }

            if ( SupportedFormats.Contains( mime.Value ) )
            {
                return _mimes.Add( mime );
            }
            
            return false;
        }

        public void RemoveMime( WeightedString mime )
        {
            _mimes.Remove( mime );
        }

        public void ClearMimes()
        {
            _mimes.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _mimes );
        }



        
        
        
        public static bool TryParse( string input , out AcceptRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , "," , out var tokens ) )
            {
                var header = new AcceptRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( WeightedString.TryParse( token , out var mime ) )
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

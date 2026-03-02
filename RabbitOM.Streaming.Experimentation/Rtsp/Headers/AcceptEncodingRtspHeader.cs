

using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    public sealed class AcceptEncodingRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Accept-Encoding";

        





        // TODO: replace a dictionary
        private readonly HashSet<WeightedString> _encodings = new HashSet<WeightedString>();







        public IReadOnlyCollection<WeightedString> Encodings
        {
            get => _encodings;
        }









        public bool AddEncoding( WeightedString encoding )
        {
            if ( WeightedString.IsNullOrEmpty( encoding ) )
            {
                return false;
            }

            if ( Constants.EncodingTypes.Contains( encoding.Value ) )
            {
                return _encodings.Add( encoding );
            }

            return false;
        }

        public bool RemoveEncoding( WeightedString encoding )
        {
            return _encodings.Remove( encoding );
        }

        public void ClearEncodings()
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
            
            if ( RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , "," , out var tokens ) )
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
    }
}

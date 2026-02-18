using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class AcceptRtspHeader
    {
        public static readonly string TypeName = "Accept";
        




        private readonly HashSet<StringRtspHeader> _mimes = new HashSet<StringRtspHeader>();
        




        public IReadOnlyCollection<StringRtspHeader> Mimes
        {
            get => _mimes;
        }
        




        public static bool TryParse( string input , out AcceptRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( input ) , ',' , out var tokens ) )
            {
                var header = new AcceptRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( StringRtspHeader.TryParse( token , out var mime ) )
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
        




        public bool AddMime( StringRtspHeader value )
        {
            if ( ! StringRtspHeader.IsNullOrEmpty( value ) )
            {
                return _mimes.Add( value );
            }

            return false;
        }

        public void RemoveMime( StringRtspHeader value )
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
    }
}

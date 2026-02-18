using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class AcceptRangesRtspHeader
    {
        public static readonly string TypeName = "Accept-Ranges";
        




        private readonly HashSet<string> _units = new HashSet<string>( StringComparer.OrdinalIgnoreCase );
        




        public IReadOnlyCollection<string> Units
        {
            get => _units;
        }
        




        public static bool TryParse( string input , out AcceptRangesRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( input ) , ',' , out var tokens ) )
            {
                var header = new AcceptRangesRtspHeader();

                foreach ( var token in tokens )
                {
                    header.AddUnit( token );
                }

                if ( header.Units.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }





        public bool AddUnit( string value )
        {
            var unit = RtspValueNormalizer.Normalize( value );

            if ( string.IsNullOrWhiteSpace( unit ) )
            {
                return false;
            }
            
            return _units.Add( unit );
        }

        public void RemoveUnit( string value )
        {
            _units.Remove( RtspValueNormalizer.Normalize( value ) );
        }

        public void RemoveUnits()
        {
            _units.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _units );
        }
    }
}

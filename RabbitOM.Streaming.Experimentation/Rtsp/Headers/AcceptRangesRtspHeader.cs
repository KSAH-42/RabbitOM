using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class AcceptRangesRtspHeader : RtspHeader 
    {
        public const string TypeName = "Accept-Ranges";
        
        private readonly HashSet<string> _units = new HashSet<string>();

        public IReadOnlyCollection<string> Units
        {
            get => _units;
        }

        public bool TryAddUnit( string value )
        {
            var text = StringRtspNormalizer.Normalize( value );

            if ( text.Any( x => char.IsLetterOrDigit( x ) ) || text == "*" )
            {
                return _units.Add( text );
            }

            return false;
        }

        public void RemoveUnit( string value )
        {
            _units.Remove( StringRtspNormalizer.Normalize( value ) );
        }

        public void RemoveUnits()
        {
            _units.Clear();
        }

        public override bool TryValidate()
        {
            return _units.Count > 0 && _units.All( StringRtspValidator.TryValidate );
        }

        public override string ToString()
        {
            return string.Join( ", " , _units );
        }
        
        public static bool TryParse( string input , out AcceptRangesRtspHeader result )
        {
            result = null;

            if ( ! RtspHeaderParser.TryParse( StringRtspNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                return false;
            }

            var header = new AcceptRangesRtspHeader();

            foreach ( var token in tokens )
            {
                header.TryAddUnit( token );
            }

            if ( header.Units.Count <= 0 )
            {
                return false;
            }

            result = header;

            return true;
        }
    }
}

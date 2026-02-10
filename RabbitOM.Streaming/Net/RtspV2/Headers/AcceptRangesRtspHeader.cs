using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
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

            if ( string.IsNullOrWhiteSpace( text ) )
            {
                return false;
            }

            return _units.Add( text );
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
        
        public static bool TryParse( string value , out AcceptRangesRtspHeader result )
        {
            result = null;

            throw new NotImplementedException();
        }
    }
}

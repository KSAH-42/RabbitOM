using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public class VaryRtspHeader : RtspHeader 
    {
        public const string TypeName = "Vary";

        

        private readonly List<string> _headersNames = new List<string>();



        public IReadOnlyCollection<string> HeadersNames { get => _headersNames; }



        public override bool TryValidate()
        {
            return _headersNames.Count > 0;
        }

        public void AddHeaderName( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            _headersNames.Add( value );
        }

        public void RemoveHeaderName( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return;
            }

            _headersNames.Add( value );
        }

        public void RemoveAllHeadersNames()
        {
            _headersNames.Clear();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }




        public static bool TryParse( string value , out VaryRtspHeader result )
        {
            throw new NotImplementedException();
        }
    }
}

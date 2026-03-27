using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers
{
    internal sealed class RtspHeaderParsers
    {
        public delegate bool TryParseDelegate( string input , out RtspHeaderValue result );
        public delegate bool TryParseDelegate<TValue>( string input , out TValue result ) where TValue : RtspHeaderValue;

        private readonly Dictionary<string,TryParseDelegate> _parsers = new Dictionary<string, TryParseDelegate>( StringComparer.OrdinalIgnoreCase );
    
        public void AddParser<TValue>( string typeName , TryParseDelegate<TValue> handler ) where TValue : RtspHeaderValue
        {
            _parsers[ typeName ] = new TryParseDelegate( (string input , out RtspHeaderValue result) =>
            {
                result = handler( input , out var value ) ? value : null;

                return result != null;
            });
        }

        public bool TryParse( string typeName , string input , out RtspHeaderValue result )
        {
            result = null;

            if ( ! _parsers.TryGetValue( typeName ?? string.Empty , out var parseHandler ) )
            {
                return false;
            }

            if ( ! parseHandler.Invoke( input , out var obj ) )
            {
                return false;
            }

            result = obj;

            return result != null;
        }
    }
}

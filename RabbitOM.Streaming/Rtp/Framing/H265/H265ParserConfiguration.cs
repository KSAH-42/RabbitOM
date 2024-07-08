using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265ParserConfiguration
    {
        private readonly object _lock = new object();

        private readonly HashSet<int> _supportedPayloads = new HashSet<int>();

        public bool SupportsPayload( int value )
        {
            lock ( _lock )
            {
                return _supportedPayloads.Contains( value );
            }
        }

        public bool RegisterPayload( int value )
        {
            lock ( _lock )
            {
                return _supportedPayloads.Add( value );
            }
        }

        public bool UnRegisterPayload( int value )
        {
            lock ( _lock )
            {
                return _supportedPayloads.Remove( value );
            }
        }
    }
}
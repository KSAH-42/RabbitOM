using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public sealed class RtcpByePacket : RtcpPacket
    {
        public const int Type = 203;




        private readonly List<uint> _sscrIds = new List<uint>();



        public RtcpByePacket( byte version ) : base ( version ) { }



        public string Reason { get; private set; }
        
        public IReadOnlyList<uint>SynchronizationSourcesIds { get => _sscrIds; }




        public static bool TryCreateFrom( RtcpMessage message , out RtcpByePacket result )
        {
            result = null;
            
            if ( message == null || message.Payload.Count == 0 )
            {
                return false;
            }

            result = new RtcpByePacket( message.Version );

            var offset = message.Payload.Offset;

            for ( var i = 0 ; i < message.SpecificParameter ; ++ i )
            {
                if ( offset + 4 < message.Payload.Array.Length )
                {
                    uint sscrId = 0;

                    sscrId  = (uint) ( message.Payload.Array[ offset ++ ] << 24 );
                    sscrId |= (uint) ( message.Payload.Array[ offset ++ ] << 16 );
                    sscrId |= (uint) ( message.Payload.Array[ offset ++ ] << 8 );
                    sscrId |= (uint) ( message.Payload.Array[ offset ++ ] );

                    result._sscrIds.Add( sscrId );

                    offset += 4;
                }
            }

            if ( offset < message.Payload.Array.Length )
            {
                var textBuffer = new byte[ message.Payload.Array[ offset ++ ] ];

                for ( var i = 0 ; i < textBuffer.Length && offset < message.Payload.Array.Length - 1 ; ++ i )
                {
                    textBuffer[ i ] = message.Payload.Array[ offset ++ ];
                }

                result.Reason = System.Text.Encoding.ASCII.GetString( textBuffer );
            }

            return true;
        }
    }
}

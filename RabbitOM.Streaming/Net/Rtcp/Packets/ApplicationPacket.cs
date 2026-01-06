using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public sealed class ApplicationPacket : RtcpPacket
    {
        public const int PacketType = 204;

        public const int MinimumSize = 8;




        public uint SynchronizationSourceId { get; private set; }
        
        public string Name { get; private set; }
        
        public string Data { get; private set; }




        public static bool TryParse( RtcpMessage message , out ApplicationPacket result )
        {
            result = null;

            if ( message == null || message.Payload.Count < MinimumSize )
            {
                return false;
            }

            var offset = message.Payload.Offset;

            result.SynchronizationSourceId  = (uint) ( message.Payload.Array[ offset ++ ] << 24 );
            result.SynchronizationSourceId |= (uint) ( message.Payload.Array[ offset ++ ] << 16 );
            result.SynchronizationSourceId |= (uint) ( message.Payload.Array[ offset ++ ] << 8  );
            result.SynchronizationSourceId |=          message.Payload.Array[ offset ++ ];
            
            var buffer = new List<byte>();

            buffer.Add( message.Payload.Array[ offset ++ ] );
            buffer.Add( message.Payload.Array[ offset ++ ] );
            buffer.Add( message.Payload.Array[ offset ++ ] );
            buffer.Add( message.Payload.Array[ offset ++ ] );

            result.Name = System.Text.Encoding.ASCII.GetString( buffer.ToArray() );

            if ( message.Payload.Count > 8 )
            {
                buffer.Clear();

                while( offset < message.Payload.Array.Length )
                {
                    buffer.Add( message.Payload.Array[ offset ++ ] );
                }

                result.Data = System.Text.Encoding.ASCII.GetString( buffer.ToArray() );
            }

            return true;
        }
    }
}

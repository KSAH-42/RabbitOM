using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp.Payloads
{
    public struct RtcpApplicationPayload
    {
        public uint SynchronizationSourceId { get; private set; }

        public string Name { get; private set; }

        public string Data { get; private set; }



        public static bool TryParse( in ArraySegment<byte> payload , out RtcpApplicationPayload result )
        {
            result = default;

            if ( payload.Array == null || payload.Count < 8 )
            {
                return false;
            }

            var offset = payload.Offset;

            result.SynchronizationSourceId |= (uint) ( payload.Array[ offset ++ ] << 24 );
            result.SynchronizationSourceId |= (uint) ( payload.Array[ offset ++ ] << 16 );
            result.SynchronizationSourceId |= (uint) ( payload.Array[ offset ++ ] << 8  );
            result.SynchronizationSourceId |=          payload.Array[ offset ++ ];
            
            var buffer = new List<byte>();

            buffer.Add( payload.Array[ offset ++ ] );
            buffer.Add( payload.Array[ offset ++ ] );
            buffer.Add( payload.Array[ offset ++ ] );
            buffer.Add( payload.Array[ offset ++ ] );

            result.Name = System.Text.Encoding.ASCII.GetString( buffer.ToArray() );

            if ( payload.Count > 8 )
            {
                buffer.Clear();

                while( offset < ( payload.Array.Length - 4 ) )
                {
                    buffer.Add( payload.Array[ offset ++ ] );
                }
            }

            return true;
        }
    }
}

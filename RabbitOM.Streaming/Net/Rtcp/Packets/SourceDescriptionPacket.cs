using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public sealed class SourceDescriptionPacket : RtcpPacket
    {
        public const int PacketType = 202;




        private readonly List<RtcpSourceDescriptionItem> _items = new List<RtcpSourceDescriptionItem>();




        public uint SynchronizationSourceId { get; private set; }
        
        public IReadOnlyList<RtcpSourceDescriptionItem> Items { get => _items; }





        private void AddItem( RtcpSourceDescriptionItem item )
        {
            _items.Add( item ?? throw new ArgumentNullException( nameof( item ) ) );
        }





        public static bool TryParse( RtcpMessage message , out SourceDescriptionPacket result )
        {
            result = null;

            if ( message == null || message.Payload.Count < RtcpSourceDescriptionItem.MinimumSize )
            {
                return false;
            }

            result = new SourceDescriptionPacket() { Version = message.Version };

            var offset = message.Payload.Offset;

            while ( offset + RtcpSourceDescriptionItem.MinimumSize <= message.Payload.Array.Length )
            {
                if ( RtcpSourceDescriptionItem.TryParse( new ArraySegment<byte>( message.Payload.Array , offset , message.Payload.Count - RtcpSourceDescriptionItem.MinimumSize ) , out var item ) )
                {
                    result.AddItem( item );
                }

                offset += RtcpSourceDescriptionItem.MinimumSize + item?.Value?.Length ?? 0 ;
            }

            return true;
        }
    }
}

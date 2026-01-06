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
            throw new NotImplementedException();
        }
    }
}

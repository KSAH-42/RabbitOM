using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public sealed class RtcpSourceDescriptionPacket : RtcpPacket
    {
        public const int Type = 202;



        private readonly List<RtcpSourceDescriptionItem> _items = new List<RtcpSourceDescriptionItem>();



        public RtcpSourceDescriptionPacket( byte version ) : base ( version ) { }



        public uint SynchronizationSourceId { get; private set; }
        
        public IReadOnlyList<RtcpSourceDescriptionItem> Items { get => _items; }






        public static bool TryCreateFrom( RtcpMessage message , out RtcpSourceDescriptionPacket result )
        {
            result = null;

            if ( message == null || message.Payload.Count < RtcpSourceDescriptionItem.MinimumSize )
            {
                return false;
            }

            result = new RtcpSourceDescriptionPacket( message.Version );

            var offset = message.Payload.Offset;

            while ( offset + RtcpSourceDescriptionItem.MinimumSize <= message.Payload.Array.Length )
            {
                if ( RtcpSourceDescriptionItem.TryParse( new ArraySegment<byte>( message.Payload.Array , offset , message.Payload.Count - RtcpSourceDescriptionItem.MinimumSize ) , out var item ) )
                {
                    System.Diagnostics.Debug.Assert( item != null );

                    result._items.Add( item );
                }

                offset += RtcpSourceDescriptionItem.MinimumSize + item?.Value?.Length ?? 0 ;
            }

            return true;
        }
    }
}

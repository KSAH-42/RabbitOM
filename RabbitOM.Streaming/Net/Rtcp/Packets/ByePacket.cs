using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public sealed class ByePacket : RtcpPacket
    {
        public const int PacketType = 203;




        private readonly List<uint> _synchronizationSourcesIds = new List<uint>();




        public string Reason { get; private set; }
        
        public IReadOnlyList<uint>SynchronizationSourcesIds { get => _synchronizationSourcesIds; }




        private void AddSynchronizationSourceId( uint id )
        {
            _synchronizationSourcesIds.Add( id );
        }




        public static bool TryParse( RtcpMessage message , out ByePacket result )
        {
            throw new NotImplementedException();
        }
    }
}

using System;

namespace RabbitOM.Streaming.Net.Rtcp.Packets.Serialization
{
    public static class RtcpPacketSerializer
    {
        public static RtcpPacket Deserialize( in ArraySegment<byte> buffer )
        {
            if ( RtcpMessage.TryParse( buffer , out var message ) )
            {
                if ( message.Version <= RtcpPacket.DefaultVersion )
                {
                    throw new NotSupportedException();
                }

                if ( message.Type == ApplicationPacket.PacketType )
                {
                    if ( ApplicationPacket.TryParse( message , out var result ) )
                    {
                        return result;
                    }
                }

                else if ( message.Type == ByePacket.PacketType )
                {
                    if ( ByePacket.TryParse( message , out var result ) )
                    {
                        return result;
                    }
                }

                else if ( message.Type == ReceiverReportPacket.PacketType )
                {
                    if ( ReceiverReportPacket.TryParse( message , out var result ) )
                    {
                        return result;
                    }
                }

                else if ( message.Type == SenderReportPacket.PacketType )
                {
                    if ( SenderReportPacket.TryParse( message , out var result ) )
                    {
                        return result;
                    }
                }

                else if ( message.Type == SourceDescriptionPacket.PacketType )
                {
                    if ( SourceDescriptionPacket.TryParse( message , out var result ) )
                    {
                        return result;
                    }
                }
                else
                {
                    throw new NotSupportedException();
                }
            }

            throw new FormatException();
        }
    }
}

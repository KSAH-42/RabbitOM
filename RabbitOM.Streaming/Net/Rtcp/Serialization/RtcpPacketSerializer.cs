using System;

namespace RabbitOM.Streaming.Net.Rtcp.Serialization
{
    public static class RtcpPacketSerializer
    {
        public static RtcpPacket Deserialize( in ArraySegment<byte> buffer )
        {
            if ( RtcpMessage.TryParse( buffer , out var message ) )
            {
                if ( message.Version < RtcpPacket.DefaultVersion )
                {
                    throw new NotSupportedException();
                }

                if ( message.Type == ApplicationPacket.PacketType )
                {
                    if ( ApplicationPacket.TryCreateFrom( message , out var result ) )
                    {
                        return result;
                    }
                }

                else if ( message.Type == ByePacket.PacketType )
                {
                    if ( ByePacket.TryCreateFrom( message , out var result ) )
                    {
                        return result;
                    }
                }

                else if ( message.Type == ReceiverReportPacket.PacketType )
                {
                    if ( ReceiverReportPacket.TryCreateFrom( message , out var result ) )
                    {
                        return result;
                    }
                }

                else if ( message.Type == SenderReportPacket.PacketType )
                {
                    if ( SenderReportPacket.TryCreateFrom( message , out var result ) )
                    {
                        return result;
                    }
                }

                else if ( message.Type == SourceDescriptionPacket.PacketType )
                {
                    if ( SourceDescriptionPacket.TryCreateFrom( message , out var result ) )
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

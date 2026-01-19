using System;

namespace RabbitOM.Streaming.Net.Rtcp.Packets.Serialization
{
    public static class RtcpSerializer
    {
        public static RtcpPacket Deserialize( in ArraySegment<byte> buffer )
        {
            if ( RtcpMessage.TryParse( buffer , out var message ) )
            {
                if ( message.Version < RtcpPacket.DefaultVersion )
                {
                    throw new NotSupportedException( $"Invalid version: {message.Version}" );
                }

                if ( message.Type == RtcpApplicationPacket.Type )
                {
                    if ( RtcpApplicationPacket.TryCreateFrom( message , out var result ) )
                    {
                        return result;
                    }
                }

                else if ( message.Type == RtcpByePacket.Type )
                {
                    if ( RtcpByePacket.TryCreateFrom( message , out var result ) )
                    {
                        return result;
                    }
                }

                else if ( message.Type == RtcpReceiverReportPacket.Type )
                {
                    if ( RtcpReceiverReportPacket.TryCreateFrom( message , out var result ) )
                    {
                        return result;
                    }
                }

                else if ( message.Type == RtcpSenderReportPacket.Type )
                {
                    if ( RtcpSenderReportPacket.TryCreateFrom( message , out var result ) )
                    {
                        return result;
                    }
                }

                else if ( message.Type == RtcpSourceDescriptionPacket.Type )
                {
                    if ( RtcpSourceDescriptionPacket.TryCreateFrom( message , out var result ) )
                    {
                        return result;
                    }
                }
                else
                {
                    throw new NotSupportedException( $"Unsupported message type: {message.Type}" );
                }
            }

            throw new FormatException( "the message can not be parsed" );
        }
    }
}

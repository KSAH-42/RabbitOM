using System;

namespace RabbitOM.Streaming.Rtp
{
    public sealed class RtpPacket
    {
        public byte Version { get; private set; }

        public bool HasPadding { get; private set; }

        public bool Marker { get; private set; }

        public RtpPacketType Type { get; private set; }

        public ushort SequenceNumber { get; private set; }

        public uint TimeStamp { get; private set; }

        public bool HasExtension { get; private set; }

        public ushort NumberOfCSRC { get; private set; }

        public uint SSRC { get; private set; }

        public uint Extension { get; private set; }

        public int[] CSRCIdentifiers { get; private set; }

        public ArraySegment<byte> Payload { get; private set; }







        public bool TryValidate()
        {
            return Version == 2 && Payload.Array != null && Payload.Count > 0;
        }







        public static bool IsDynamicType( RtpPacket packet )
        {
            return packet != null && RtpPacketType.DYNAMIC_1 <= packet.Type && packet.Type <= RtpPacketType.DYNAMIC_32;
        }

        public static bool TryParse( byte[] buffer , out RtpPacket result )
        {
            result = null;

            if ( buffer == null || buffer.Length < 12 )
            {
                return false;
            }

            var packet = new RtpPacket();

            packet.Version         = (byte) ( ( buffer[ 0 ] >> 6 ) & 0x3 );
            packet.HasPadding      = (byte) ( ( buffer[ 0 ] >> 5 ) & 0x1 ) >= 1;
            packet.HasExtension    = (byte) ( ( buffer[ 0 ] >> 4 ) & 0x1 ) == 1;
            packet.NumberOfCSRC    = (ushort) ( buffer[ 0 ] & 0x0F );

            packet.Marker          = (byte)        ( ( buffer[ 1 ] >> 7   ) & 0x1 ) != 0;
            packet.Type            = (RtpPacketType) ( buffer[ 1 ] & 0x7F );
            packet.SequenceNumber  = (ushort)        ( buffer[ 2 ] << 8   );
            packet.SequenceNumber |= (ushort)        ( buffer[ 3 ]        );
            packet.TimeStamp       = (uint) ( buffer[ 4 ] << 24  );
            packet.TimeStamp      |= (uint) ( buffer[ 5 ] << 16  );
            packet.TimeStamp      |= (uint) ( buffer[ 6 ] << 8   );
            packet.TimeStamp      |= (uint) ( buffer[ 7 ] << 0   );
            packet.SSRC            = (uint) ( buffer[ 8 ] << 24  );
            packet.SSRC           |= (uint) ( buffer[ 9 ] << 16  );
            packet.SSRC           |= (uint) ( buffer[ 10 ]<< 8   );
            packet.SSRC           |=          buffer[ 11 ];

            packet.SequenceNumber = (ushort) ( packet.SequenceNumber % ( ushort.MaxValue + 1 ) );

            var offset = 12;

            var limit = offset + 4 * packet.NumberOfCSRC + (packet.HasExtension ? 4 : 0 );

            if ( limit >= buffer.Length )
            {
                return false;
            }

            if ( packet.NumberOfCSRC > 0 )
            {
                packet.CSRCIdentifiers = new int[ packet.NumberOfCSRC ];

                for ( uint i = 0 ; i < packet.CSRCIdentifiers.Length ; ++i )
                {
                    packet.CSRCIdentifiers[ i ] += buffer[ i + offset ++ ] << 24;
                    packet.CSRCIdentifiers[ i ] += buffer[ i + offset ++ ] << 16;
                    packet.CSRCIdentifiers[ i ] += buffer[ i + offset ++ ] << 8;
                    packet.CSRCIdentifiers[ i ] += buffer[ i + offset ++ ];
                }
            }

            if ( packet.HasExtension )
            {
                packet.Extension = ( (uint) buffer[ offset ] << 8 ) + (uint) ( buffer[ ++ offset ] << 0 );

                offset += ( buffer[ ++ offset ] << 8 ) + ( buffer[ ++ offset ] << 0 ) * 4;
            }

            if ( offset >= buffer.Length )
            {
                return false;
            }

            var payloadSize = buffer.Length - offset;

            if ( packet.HasPadding )
            {
                payloadSize -= buffer[ buffer.Length - 1 ];
            }

            packet.Payload = new ArraySegment<byte>( buffer , offset , payloadSize );

            result = packet;

            return true;
        }
    }
}
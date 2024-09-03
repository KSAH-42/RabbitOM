using System;

namespace RabbitOM.Streaming.Rtp
{
    /// <summary>
    /// Represent the packet class
    /// </summary>
    public sealed class RtpPacket
    {
        /// <summary>
        /// Gets / Sets the version
        /// </summary>
        public byte Version { get; set; }

        /// <summary>
        /// Gets / Sets the padding state
        /// </summary>
        public bool HasPadding { get; set; }

        /// <summary>
        /// Gets / Sets the extensions usage state
        /// </summary>
        public bool HasExtension { get; set; }

        /// <summary>
        /// Gets / Sets the the number of contributing sources
        /// </summary>
        public ushort NumberOfCSRC { get; set; }

        /// <summary>
        /// Gets / Sets the marker state
        /// </summary>
        public bool Marker { get; set; }

        /// <summary>
        /// Gets / Sets the type
        /// </summary>
        public PacketType Type { get; set; }

        /// <summary>
        /// Gets / Sets the sequence number
        /// </summary>
        public ushort SequenceNumber { get; set; }

        /// <summary>
        /// Gets / Sets the timestamp
        /// </summary>
        public uint TimeStamp { get; set; }

        /// <summary>
        /// Gets / Sets the sequence source
        /// </summary>
        public uint SSRC { get; set; }

        /// <summary>
        /// Gets / Sets the extension
        /// </summary>
        public uint Extension { get; set; }

        /// <summary>
        /// Gets / Sets the contributors sources identifiers
        /// </summary>
        public int[] CSRCIdentifiers { get; set; }

        /// <summary>
        /// Gets / Sets the payload
        /// </summary>
        public ArraySegment<byte> Payload { get; set; }







        /// <summary>
        /// Try to validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryValidate()
        {
            return Version == 2 && Payload.Array != null  && Payload.Count > 0;
        }







        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="buffer">the input buffer</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( byte[] buffer , out RtpPacket result )
        {
            result = null;

            if ( buffer == null || buffer.Length < 12 )
            {
                return false;
            }
            
            var packet = new RtpPacket();

            packet.Version         = (byte) (   buffer[ 0 ] >> 6 );
            packet.HasPadding      = (byte) ( ( buffer[ 0 ] >> 5 ) & 0x1 ) == 1;
            packet.HasExtension    = (byte) ( ( buffer[ 0 ] >> 4 ) & 0x1 ) == 1;
            packet.NumberOfCSRC    = (ushort) ( buffer[ 0 ] & 0x0F );

            packet.Marker          = (byte)       ((buffer[ 1 ] >> 7   ) & 0x1 ) != 0;
            packet.Type            = (PacketType) ( buffer[ 1 ] & 0x7F );
            packet.SequenceNumber  = (ushort)     ( buffer[ 2 ] << 8   );
            packet.SequenceNumber += (ushort)     ( buffer[ 3 ]        );
            packet.TimeStamp       = (uint) ( buffer[ 4 ] << 24  );
            packet.TimeStamp      += (uint) ( buffer[ 5 ] << 16  );
            packet.TimeStamp      += (uint) ( buffer[ 6 ] << 8   );
            packet.TimeStamp      += (uint) ( buffer[ 7 ] << 0   );
            packet.SSRC            = (uint) ( buffer[ 8 ] << 24  );
            packet.SSRC           += (uint) ( buffer[ 9 ] << 16  );
            packet.SSRC           += (uint) ( buffer[ 10 ]<< 8   );
            packet.SSRC           +=          buffer[ 11 ];

            packet.SequenceNumber = (ushort) ( packet.SequenceNumber % ( ushort.MaxValue + 1 ) );

            int offset = 12;
            
            int limit = offset + 4 * packet.NumberOfCSRC + (packet.HasExtension ? 4 : 0 );

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

            int payloadSize = buffer.Length - offset;

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
﻿using System;

namespace RabbitOM.Streaming.Rtp
{
    /// <summary>
    /// Represent the packet class
    /// </summary>
    public sealed class RTPPacket
    {
        /// <summary>
        /// Disable the constructor
        /// </summary>
        private RTPPacket() { }




        /// <summary>
        /// Gets the version
        /// </summary>
        public byte Version { get; private set; }

        /// <summary>
        /// Gets the padding state
        /// </summary>
        public bool HasPadding { get; private set; }

        /// <summary>
        /// Gets the extensions usage state
        /// </summary>
        public bool HasExtension { get; private set; }

        /// <summary>
        /// Gets the the number of contributing sources
        /// </summary>
        public ushort NumberOfCSRC { get; private set; }

        /// <summary>
        /// Gets the marker state
        /// </summary>
        public bool Marker { get; private set; }

        /// <summary>
        /// Gets the type
        /// </summary>
        public byte Type { get; private set; }

        /// <summary>
        /// Gets the sequence number
        /// </summary>
        public uint SequenceNumber { get; private set; }

        /// <summary>
        /// Gets the timestamp
        /// </summary>
        public uint Timestamp { get; private set; }

        /// <summary>
        /// Gets the sequence source
        /// </summary>
        public uint SSRC { get; private set; }

        /// <summary>
        /// Gets the extension
        /// </summary>
        public uint Extension { get; private set; }

        /// <summary>
        /// Gets the contributors sources identifiers
        /// </summary>
        public int[] CSRCIdentifiers { get; private set; }

        /// <summary>
        /// Gets the extension data
        /// </summary>
        public byte[] ExtensionData { get; private set; }

        /// <summary>
        /// Gets the payload
        /// </summary>
        public ArraySegment<byte> Payload { get; private set; }




        /// <summary>
        /// Try to validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryValidate()
        {
            return Type >= 0 && Version == 2 && Payload.Count > 0;
        }




        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="buffer">the input buffer</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( byte[] buffer , out RTPPacket result )
        {
            result = null;

            if ( buffer == null || buffer.Length < 12 )
            {
                return false;
            }
            
            var packet = new RTPPacket();

            packet.Version         = (byte) (   buffer[ 0 ] >> 6 );
            packet.HasPadding      = (byte) ( ( buffer[ 0 ] >> 5 ) & 0x1 ) == 1;
            packet.HasExtension    = (byte) ( ( buffer[ 0 ] >> 4 ) & 0x1 ) == 1;
            packet.NumberOfCSRC    = (ushort) ( buffer[ 0 ] & 0x0F );

            packet.Marker          = (byte) ((buffer[ 1 ] >> 7   ) & 0x1 ) != 0;
            packet.Type            = (byte) ( buffer[ 1 ] & 0x7F );
            packet.SequenceNumber += (uint) ( buffer[ 2 ] << 8   );
            packet.SequenceNumber += (uint) ( buffer[ 3 ]        );
            packet.Timestamp       = (uint) ( buffer[ 4 ] << 24  );
            packet.Timestamp      += (uint) ( buffer[ 5 ] << 16  );
            packet.Timestamp      += (uint) ( buffer[ 6 ] << 8   );
            packet.Timestamp      += (uint) ( buffer[ 7 ] << 0   );
            packet.SSRC            = (uint) ( buffer[ 8 ] << 24  );
            packet.SSRC           += (uint) ( buffer[ 9 ] << 16  );
            packet.SSRC           += (uint) ( buffer[ 10 ]<< 8   );
            packet.SSRC           +=          buffer[ 11 ];

            packet.SequenceNumber = packet.SequenceNumber % ( ushort.MaxValue + 1 );

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

                int extenstionSize = ( buffer[ ++ offset ] << 8 ) + ( buffer[ ++ offset ] << 0 ) * 4;

                offset += extenstionSize;
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
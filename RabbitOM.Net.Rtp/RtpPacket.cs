using System;
using System.Text;

namespace RabbitOM.Net.Rtp
{
    /// <summary>
    /// Represent a rtp packet
    /// </summary>
    public sealed class RtpPacket
    {
        /// <summary>
        /// The default header size
        /// </summary>
        private const int DefaultHeaderSize = 12;

        /// <summary>
        /// The default protocol version
        /// </summary>
        private const int DefaultVersion = 2;








        private uint _version;

        private bool _hasPadding;

        private bool _hasExtension;

        private int _numberOfcsrc;

        private bool _marker;

        private int _payloadType;

        private ushort _sequenceNumber;

        private uint _timestamp;

        private uint _ssrc;

        private int _extensionHeaderId;

        private ArraySegment<byte> _data;








        /// <summary>
        /// Gets the version
        /// </summary>
        public uint Version
        {
            get => _version;
        }

        /// <summary>
        /// Gets the padding flag
        /// </summary>
        public bool HasPadding
        {
            get => _hasPadding;
        }

        /// <summary>
        /// Gets the extensions flag
        /// </summary>
        public bool HasExtension
        {
            get => _hasExtension;
        }

        /// <summary>
        /// Gets the contributing source count
        /// </summary>
        public int NumberOfCSRC
        {
            get => _numberOfcsrc;
        }

        /// <summary>
        /// Gets the marker bit
        /// </summary>
        public bool Marker
        {
            get => _marker;
        }

        /// <summary>
        /// Gets the payload type
        /// </summary>
        public int PayloadType
        {
            get => _payloadType;
        }

        /// <summary>
        /// Gets the sequence number
        /// </summary>
        public ushort SequenceNumber
        {
            get => _sequenceNumber;
        }

        /// <summary>
        /// Gets the timestamp
        /// </summary>
        public uint Timestamp
        {
            get => _timestamp;
        }

        /// <summary>
        /// Gets the synchronization source identifier
        /// </summary>
        public uint SSRC
        {
            get => _ssrc;
        }

        /// <summary>
        /// Gets the extension header identifier
        /// </summary>
        public int ExtensionHeaderId
        {
            get => _extensionHeaderId;
        }

        /// <summary>
        /// Gets the payload
        /// </summary>
        public ArraySegment<byte> Data
        {
            get => _data;
        }








        /// <summary>
        /// Returns minimal informations about the rtp packet like data size, payload type
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat( "Marker: {0}" , _marker );
            builder.AppendFormat( "SequenceNumber: {0}" , _sequenceNumber );
            builder.AppendFormat( "PayloadType: {0}" , _payloadType );
            builder.AppendFormat( "DataSize: {0}" , _data.Count );

            return builder.ToString();
        }








        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( byte[] buffer , out RtpPacket result )
        {
            result = default;

            if ( buffer == null || buffer.Length < RtpPacket.DefaultHeaderSize )
            {
                return false;
            }

            int offset = 0;

            uint version = (uint) buffer[ offset ] >> 6;

            if ( version != RtpPacket.DefaultVersion )
            {
                return false;
            }

            result = new RtpPacket();

            result._version = version;

            result._hasPadding = ( ( buffer[ offset ] >> 5 ) & 0x1 ) != 0;
            result._hasExtension = ( ( buffer[ offset ] >> 4 ) & 0x1 ) != 0;
            result._numberOfcsrc = buffer[ offset++ ] & 0xF;

            result._marker = ( ( buffer[ offset ] >> 7 ) & 0x1 ) != 0;
            result._payloadType = buffer[ offset++ ] & 0x7F;

            result._sequenceNumber = (ushort) RtpDataConverter.ConvertToInt16( buffer , offset );
            offset += 2;

            result._timestamp = RtpDataConverter.ConvertToUInt( buffer , offset );
            offset += 4;

            result._ssrc = RtpDataConverter.ConvertToUInt( buffer , offset );
            offset += 4 + 4 * result.NumberOfCSRC;

            if ( result._hasExtension )
            {
                result._extensionHeaderId = RtpDataConverter.ConvertToInt16( buffer , offset );

                offset += 4;
                offset += RtpDataConverter.ConvertToInt16( buffer , offset ) * 4;
            }

            int payloadSize = buffer.Length - offset;

            if ( result._hasPadding )
            {
                payloadSize -= buffer[ buffer.Length - 1 ];
            }

            result._data = new ArraySegment<byte>( buffer , offset , payloadSize );

            return true;
        }
    }
}
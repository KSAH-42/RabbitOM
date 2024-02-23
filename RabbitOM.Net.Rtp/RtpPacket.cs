using System;

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
        public const int DefaultHeaderSize = 12;

        /// <summary>
        /// The default protocol version
        /// </summary>
        public const int DefaultVersion = 2;




        private uint _version;

        private bool _hasPadding;

        private bool _hasExtension;

        private int _csrcCount;

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
            private set => _version = value;
        }

        /// <summary>
        /// Gets the padding flag
        /// </summary>
        public bool HasPadding
        {
            get => _hasPadding;
            private set => _hasPadding = value;
        }

        /// <summary>
        /// Gets the extensions flag
        /// </summary>
        public bool HasExtension
        {
            get => _hasExtension;
            private set => _hasExtension = value;
        }

        /// <summary>
        /// Gets the contributing source count
        /// </summary>
        public int NumberOfCSRC
        {
            get => _csrcCount;
            private set => _csrcCount = value;
        }

        /// <summary>
        /// Gets the marker bit
        /// </summary>
        public bool Marker
        {
            get => _marker;
            private set => _marker = value;
        }

        /// <summary>
        /// Gets the payload type
        /// </summary>
        public int PayloadType
        {
            get => _payloadType;
            private set => _payloadType = value;
        }

        /// <summary>
        /// Gets the sequence number
        /// </summary>
        public ushort SequenceNumber
        {
            get => _sequenceNumber;
            private set => _sequenceNumber = value;
        }

        /// <summary>
        /// Gets the timestamp
        /// </summary>
        public uint Timestamp
        {
            get => _timestamp;
            private set => _timestamp = value;
        }

        /// <summary>
        /// Gets the synchronization source identifier
        /// </summary>
        public uint SSRC
        {
            get => _ssrc;
            private set => _ssrc = value;
        }

        /// <summary>
        /// Gets the extension header identifier
        /// </summary>
        public int ExtensionHeaderId
        {
            get => _extensionHeaderId;
            private set => _extensionHeaderId = value;
        }

        /// <summary>
        /// Gets the payload
        /// </summary>
        public ArraySegment<byte> Data
        {
            get => _data;
            private set => _data = value;
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

            if ( buffer == null )
            {
                return false;
            }

            result = new RtpPacket();

            if ( buffer.Length < DefaultHeaderSize )
            {
                return false;
            }

            int offset = 0;

            result.Version = (uint) buffer[ offset ] >> 6;

            if ( result.Version != DefaultVersion )
            {
                return false;
            }

            result.HasPadding = ( ( buffer[ offset ] >> 5 ) & 0x1 ) != 0;
            result.HasExtension = ( ( buffer[ offset ] >> 4 ) & 0x1 ) != 0;
            result.NumberOfCSRC = buffer[ offset++ ] & 0xF;

            result.Marker = ( ( buffer[ offset ] >> 7 ) & 0x1 ) != 0;
            result.PayloadType = buffer[ offset++ ] & 0x7F;

            result.SequenceNumber = (ushort) RtpDataConverter.ConvertToInt16( buffer , offset );
            offset += 2;

            result.Timestamp = RtpDataConverter.ConvertToUInt( buffer , offset );
            offset += 4;

            result.SSRC = RtpDataConverter.ConvertToUInt( buffer , offset );
            offset += 4 + 4 * result.NumberOfCSRC;

            if ( result.HasExtension )
            {
                result.ExtensionHeaderId = RtpDataConverter.ConvertToInt16( buffer , offset );

                offset += 4;
                offset += RtpDataConverter.ConvertToInt16( buffer , offset ) * 4;
            }

            int payloadSize = buffer.Length - offset;

            if ( result.HasPadding )
            {
                payloadSize -= buffer[ buffer.Length - 1 ];
            }

            result.Data = new ArraySegment<byte>( buffer , offset , payloadSize );

            return true;
        }
    }
}

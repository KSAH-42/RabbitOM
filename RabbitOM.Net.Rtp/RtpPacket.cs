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
            internal set => _version = value;
        }

        /// <summary>
        /// Gets the padding flag
        /// </summary>
        public bool HasPadding
        {
            get => _hasPadding;
            internal set => _hasPadding = value;
        }

        /// <summary>
        /// Gets the extensions flag
        /// </summary>
        public bool HasExtension
        {
            get => _hasExtension;
            internal set => _hasExtension = value;
        }

        /// <summary>
        /// Gets the contributing source count
        /// </summary>
        public int NumberOfCSRC
        {
            get => _csrcCount;
            internal set => _csrcCount = value;
        }

        /// <summary>
        /// Gets the marker bit
        /// </summary>
        public bool Marker
        {
            get => _marker;
            internal set => _marker = value;
        }

        /// <summary>
        /// Gets the payload type
        /// </summary>
        public int PayloadType
        {
            get => _payloadType;
            internal set => _payloadType = value;
        }

        /// <summary>
        /// Gets the sequence number
        /// </summary>
        public ushort SequenceNumber
        {
            get => _sequenceNumber;
            internal set => _sequenceNumber = value;
        }

        /// <summary>
        /// Gets the timestamp
        /// </summary>
        public uint Timestamp
        {
            get => _timestamp;
            internal set => _timestamp = value;
        }

        /// <summary>
        /// Gets the synchronization source identifier
        /// </summary>
        public uint SSRC
        {
            get => _ssrc;
            internal set => _ssrc = value;
        }

        /// <summary>
        /// Gets the extension header identifier
        /// </summary>
        public int ExtensionHeaderId
        {
            get => _extensionHeaderId;
            internal set => _extensionHeaderId = value;
        }

        /// <summary>
        /// Gets the payload
        /// </summary>
        public ArraySegment<byte> Data
        {
            get => _data;
            internal set => _data = value;
        }




        /// <summary>
        /// Returns minimal informations about the rtp packet like data size, payload type
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            return RtpPacketFormatter.Format( this );
        }




        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( byte[] buffer , out RtpPacket result )
        {
            return RtpPacketFormatter.TryParse( buffer , out result );
        }
    }
}
using RabbitOM.Streaming.Net.Sdp.Serialization.Formatters;
using System;

namespace RabbitOM.Streaming.Net.Sdp
{
    /// <summary>
    /// Represent the fmtp info
    /// </summary>
    public sealed class FormatAttributeValue : AttributeValue, ICopyable<FormatAttributeValue>
    {
        /// <summary>
        /// Represent field name
        /// </summary>
        public const string TypePacketizationMode = "packetization-mode";

        /// <summary>
        /// Represent field name
        /// </summary>
        public const string TypeProfileLevelId = "profile-level-id";

        /// <summary>
        /// Represent field name
        /// </summary>
        public const string TypeSPropParmeterSets = "sprop-parameter-sets";

        /// <summary>
        /// Represent field name
        /// </summary>
        public const string TypeSPropPps = "sprop-pps";
        
        /// <summary>
        /// Represent field name
        /// </summary>
        public const string TypeSPropSps = "sprop-sps";
        
        /// <summary>
        /// Represent field name
        /// </summary>
        public const string TypeSPropVps = "sprop-vps";

        /// <summary>
        /// Represent field name
        /// </summary>
        public const string TypeMode = "mode";

        /// <summary>
        /// Represent field name
        /// </summary>
        public const string TypeSizeLength = "sizelength";

        /// <summary>
        /// Represent field name
        /// </summary>
        public const string TypeIndexLength = "indexLength";

        /// <summary>
        /// Represent field name
        /// </summary>
        public const string TypeIndexDeltaLength = "indexdeltalength";

        /// <summary>
        /// Represent field name
        /// </summary>
        public const string TypeConfiguration = "config";

        /// <summary>
        /// Represent the default SPS for H264 encoder
        /// </summary>
        public const string Default_H264_SPS = "Z00AH5pkAoAt/4C1AQEBQAAA+gAAJxAh";

        /// <summary>
        /// Represent the default PPS for H264 encoder
        /// </summary>
        public const string Default_H264_PPS = "aO48gA==";





        private byte _payloadType = 0;

        private string _profileLevelId = string.Empty;

        private long _packetizationMode = 0;

        private string _sps = string.Empty;

        private string _pps = string.Empty;

        private string _vps = string.Empty;

        private string _mode = string.Empty;

        private long? _sizeLength = null;

        private long? _indexLength = null;

        private long? _indexDeltaLength = null;

        private string _configuration = string.Empty;

        private readonly StringList _extensions = new StringList();





        /// <summary>
        /// Gets / Sets the payload type
        /// </summary>
        public byte PayloadType
        {
            get => _payloadType;
            set => _payloadType = value;
        }

        /// <summary>
        /// Gets / Sets the profile level identifier
        /// </summary>
        public string ProfileLevelId
        {
            get => _profileLevelId;
            set => _profileLevelId = DataConverter.Filter(value);
        }

        /// <summary>
        /// Gets / Sets the packetization mode
        /// </summary>
        public long PacketizationMode
        {
            get => _packetizationMode;
            set => _packetizationMode = value;
        }

        /// <summary>
        /// Gets / Sets the sequence parameter sets value
        /// </summary>
        public string SPS
        {
            get => _sps;
            set => _sps = DataConverter.Filter(value);
        }

        /// <summary>
        /// Gets / Sets the picture parameter sets value
        /// </summary>
        public string PPS
        {
            get => _pps;
            set => _pps = DataConverter.Filter(value);
        }

        /// <summary>
        /// Gets / Sets the picture parameter sets value
        /// </summary>
        public string VPS
        {
            get => _vps;
            set => _vps = DataConverter.Filter(value);
        }

        /// <summary>
        /// Gets / Sets the mode
        /// </summary>
        public string Mode
        {
            get => _mode;
            set => _mode = DataConverter.Filter(value);
        }

        /// <summary>
        /// Gets / Sets the length
        /// </summary>
        public long? SizeLength
        {
            get => _sizeLength;
            set => _sizeLength = value;
        }

        /// <summary>
        /// Gets / Sets the index length
        /// </summary>
        public long? IndexLength
        {
            get => _indexLength;
            set => _indexLength = value;
        }

        /// <summary>
        /// Gets / Sets the index delta length
        /// </summary>
        public long? IndexDeltaLength
        {
            get => _indexDeltaLength;
            set => _indexDeltaLength = value;
        }

        /// <summary>
        /// Gets / Sets the configuration
        /// </summary>
        public string Configuration
        {
            get => _configuration;
            set => _configuration = DataConverter.Filter(value);
        }

        /// <summary>
        /// Gets the extensions data
        /// </summary>
        public StringList Extensions
        {
            get => _extensions;
        }



        /// <summary>
        /// Validate
        /// </summary>
        public override void Validate()
        {
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <remarks>
        ///   <para>a payload value with a null value is considered as valid value</para>
        /// </remarks>
        public override bool TryValidate()
        {
            return true;
        }

        /// <summary>
        /// Copy from
        /// </summary>
        /// <param name="info">the object</param>
        public void CopyFrom(FormatAttributeValue info)
        {
            if (info == null )
            {
                return;
            }

            _payloadType = info._payloadType;
            _profileLevelId = info._profileLevelId;
            _packetizationMode = info._packetizationMode;
            _sps = info._sps;
            _pps = info._pps;
            _vps = info._vps;
            _mode = info._mode;
            _sizeLength = info._sizeLength;
            _indexLength = info._indexLength;
            _indexDeltaLength = info._indexDeltaLength;
            _configuration = info._configuration;

            _extensions.Clear();
            _extensions.TryAddRange(info.Extensions);
        }

        /// <summary>
        /// Format the field
        /// </summary>
        /// <returns>retuns a value</returns>
        public override string ToString()
        {
            return FormatAttributeValueFormatter.Format( this );
        }





        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns an instance</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="FormatException"/>
        public static FormatAttributeValue Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return FormatAttributeValueFormatter.TryParse(value, out FormatAttributeValue result) ? result : throw new FormatException();
        }

        /// <summary>
        /// Try parse the value
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse(string value, out FormatAttributeValue result)
        {
            return FormatAttributeValueFormatter.TryParse(value, out result);
        }
    }
}

using System;
using System.Text;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the fmtp info
    /// </summary>
    public sealed class FormatAttributeValue : AttributeValue , ICopyable<FormatAttributeValue>
    {
        /// <summary>
        /// Represent field name
        /// </summary>
        public const string              TypePacketizationMode  = "packetization-mode";

        /// <summary>
        /// Represent field name
        /// </summary>
        public const string              TypeProfileLevelId     = "profile-level-id";

        /// <summary>
        /// Represent field name
        /// </summary>
        public const string              TypeSPropParmeterSets  = "sprop-parameter-sets";

        /// <summary>
        /// Represent field name
        /// </summary>
        public const string              TypeMode               = "mode";

        /// <summary>
        /// Represent field name
        /// </summary>
        public const string              TypeSizeLength         = "sizelength";

        /// <summary>
        /// Represent field name
        /// </summary>
        public const string              TypeIndexLength        = "indexLength";

        /// <summary>
        /// Represent field name
        /// </summary>
        public const string              TypeIndexDeltaLength   = "indexdeltalength";

        /// <summary>
        /// Represent field name
        /// </summary>
        public const string              TypeConfiguration      = "config";





        private byte                     _payloadType           = 0;

        private string                   _profileLevelId        = string.Empty;

        private long                     _packetizationMode     = 0;

        private string                   _sps                   = string.Empty;

        private string                   _pps                   = string.Empty;

        private string                   _mode                  = string.Empty;

        private long?                    _sizeLength            = null;

        private long?                    _indexLength           = null;

        private long?                    _indexDeltaLength      = null;

        private string                   _configuration         = string.Empty;

        private readonly ExtensionList   _extensions            = new ExtensionList();





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
            set => _profileLevelId = SessionDescriptorDataConverter.Trim( value );
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
            set => _sps = SessionDescriptorDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the picture parameter sets value
        /// </summary>
        public string PPS
        {
            get => _pps;
            set => _pps = SessionDescriptorDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the mode
        /// </summary>
        public string Mode
        {
            get => _mode;
            set => _mode = SessionDescriptorDataConverter.Trim( value );
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
            set => _configuration = SessionDescriptorDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets the extensions data
        /// </summary>
        public ExtensionList Extensions
        {
            get => _extensions;
        }



        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <remarks>
        ///   <para>a payload value with a null value is considered as valid value</para>
        /// </remarks>
        public override bool Validate()
        {
            return true;
        }

        /// <summary>
        /// Copy from
        /// </summary>
        /// <param name="info">the object</param>
        public void CopyFrom( FormatAttributeValue info )
        {
            if ( info == null || object.ReferenceEquals( this , info ) )
            {
                return;
            }

            _payloadType = info._payloadType;
            _profileLevelId = info._profileLevelId;
            _packetizationMode = info._packetizationMode;
            _sps = info._sps;
            _pps = info._pps;
            _mode = info._mode;
            _sizeLength = info._sizeLength;
            _indexLength = info._indexLength;
            _indexDeltaLength = info._indexDeltaLength;
            _configuration = info._configuration;

            _extensions.Clear();
            _extensions.AddRange( info.Extensions );
        }

        /// <summary>
        /// Format to a string 
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append( _payloadType );
            builder.Append( " " );
            builder.Append( TypePacketizationMode );
            builder.Append( "=" );
            builder.Append( _packetizationMode );
            builder.Append( ";" );

            if ( ! string.IsNullOrWhiteSpace( _profileLevelId ) )
            {
                builder.Append( " " );
                builder.Append( TypeProfileLevelId );
                builder.Append( "=" );
                builder.Append( _profileLevelId );
                builder.Append( ";" );
            }

            if ( ! string.IsNullOrWhiteSpace( _pps ) )
            {
                builder.Append( " " );
                builder.Append( TypeSPropParmeterSets );
                builder.Append( "=" );
                builder.Append( _sps );
                builder.Append( "," );
                builder.Append( _pps );
                builder.Append( ";" );
            }

            if ( ! string.IsNullOrWhiteSpace( _mode ) )
            {
                builder.Append( " " );
                builder.Append( TypeMode );
                builder.Append( "=" );
                builder.Append( _mode );
                builder.Append( ";" );
            }

            if ( _sizeLength.HasValue )
            {
                builder.Append( " " );
                builder.Append( TypeSizeLength );
                builder.Append( "=" );
                builder.Append( _sizeLength );
                builder.Append( ";" );
            }

            if ( _indexLength.HasValue )
            {
                builder.Append( " " );
                builder.Append( TypeIndexLength );
                builder.Append( "=" );
                builder.Append( _indexLength );
                builder.Append( ";" );
            }

            if ( _indexDeltaLength.HasValue )
            {
                builder.Append( " " );
                builder.Append( TypeIndexDeltaLength );
                builder.Append( "=" );
                builder.Append( _indexDeltaLength );
                builder.Append( ";" );
            }

            if ( ! string.IsNullOrWhiteSpace( _configuration ) )
            {
                builder.Append( " " );
                builder.Append( TypeConfiguration );
                builder.Append( "=" );
                builder.Append( _configuration );
                builder.Append( ";" );
            }

            if ( ! _extensions.IsEmpty )
            {
                foreach ( var extension in _extensions )
                {
                    builder.Append( " " );
                    builder.Append( extension );
                }
            }

            return builder.ToString();
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

            if (!TryParse(value, out FormatAttributeValue result) || result == null)
            {
                throw new FormatException();
            }

            return result;
        }

        /// <summary>
        /// Try parse the value
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( string value , out FormatAttributeValue result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var tokens = value.Trim().Split( new char[] { ' ' , ';' } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens == null || tokens.Length <= 0 )
            {
                return false;
            }

            result = new FormatAttributeValue();

            foreach ( var token in tokens )
            {
                StringPair pair = null;

                if ( ! SessionDescriptorDataConverter.TryExtractField( token , new char[] { '=' , ':' } , out pair ) )
                {
                    continue;
                }

                if ( string.Compare( pair.First , AttributeNames.FormatPayload, true ) == 0 )
                {
                    result.PayloadType = SessionDescriptorDataConverter.ConvertToByte( pair.Second );
                }

                else
                
                if ( string.Compare( pair.First , TypeProfileLevelId , true ) == 0 )
                {
                    result.ProfileLevelId = pair.Second;
                }
                
                else
                
                if ( string.Compare( pair.First , TypePacketizationMode , true ) == 0 )
                {
                    result.PacketizationMode = SessionDescriptorDataConverter.ConvertToLong( pair.Second );
                }
                
                else
                
                if ( string.Compare( pair.First , TypeSPropParmeterSets , true ) == 0 )
                {
                    if ( SessionDescriptorDataConverter.TryExtractField( pair.Second , ',' , out StringPair pairParameter ) )
                    {
                        result.SPS = pairParameter.First;
                        result.PPS = pairParameter.Second;
                    }
                }
                
                else

                if ( string.Compare( pair.First , TypeMode , true ) == 0 )
                {
                    result.Mode = pair.Second;
                }

                else

                if ( string.Compare( pair.First , TypeSizeLength , true ) == 0 )
                {
                    result.SizeLength = SessionDescriptorDataConverter.ConvertToNullableLong( pair.Second );
                }

                else

                if ( string.Compare( pair.First , TypeIndexLength , true ) == 0 )
                {
                    result.IndexLength = SessionDescriptorDataConverter.ConvertToNullableLong( pair.Second );
                }
                
                else

                if ( string.Compare( pair.First , TypeIndexDeltaLength , true ) == 0 )
                {
                    result.IndexDeltaLength = SessionDescriptorDataConverter.ConvertToNullableLong( pair.Second );
                }

                else
                
                if ( string.Compare( pair.First , TypeConfiguration , true ) == 0 )
                {
                    result.Configuration = pair.Second;
                }
                
                else
                {
                    result.Extensions.TryAdd( token );
                }
            }
            
            return true;
        }
    }
}

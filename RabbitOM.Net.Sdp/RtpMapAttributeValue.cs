using System;
using System.Text;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the rtp map
    /// </summary>
    public sealed class RtpMapAttributeValue : AttributeValue, ICopyable<RtpMapAttributeValue>
    {
        private string                   _encoding     = string.Empty;

        private byte                     _payloadType  = 0;

        private uint                     _clockRate    = 0;

        private readonly ExtensionList   _extensions   = new ExtensionList();



        /// <summary>
        /// Gets / Sets the encoding
        /// </summary>
        public string Encoding
        {
            get => _encoding;
            set => _encoding = SessionDescriptorDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the payload type
        /// </summary>
        public byte PayloadType
        {
            get => _payloadType;
            set => _payloadType = value;
        }

        /// <summary>
        /// Gets / Sets the clock rate
        /// </summary>
        public uint ClockRate
        {
            get => _clockRate;
            set => _clockRate = value;
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
            if ( string.IsNullOrWhiteSpace( _encoding ) )
            {
                return false;
            }

            if ( _clockRate <= 0 )
            {
                return false;
            }

            // Don't test the payload because it can be equal to zero

            return true;
        }

        /// <summary>
        /// Copy from
        /// </summary>
        /// <param name="info">the object</param>
        public void CopyFrom( RtpMapAttributeValue info )
        {
            if ( info == null || object.ReferenceEquals( this , info ) )
            {
                return;
            }

            _clockRate = info._clockRate;
            _payloadType = info._payloadType;
            _encoding = info._encoding;

            _extensions.Clear();
            _extensions.TryAddRange( info.Extensions );
        }

        /// <summary>
        /// Format to a string 
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append( _clockRate );

            if ( !string.IsNullOrWhiteSpace( _encoding ) )
            {
                builder.Append( " " );
                builder.Append( _encoding );
                builder.Append( "/" );
                builder.Append( _clockRate );
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
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="FormatException"/>
        public static RtpMapAttributeValue Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(value));
            }

            if (!TryParse(value, out RtpMapAttributeValue result) || result == null)
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
        public static bool TryParse( string value , out RtpMapAttributeValue result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var tokens = value.Trim().Split( new char[]{ ' ' } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens == null || tokens.Length <= 0 )
            {
                return false;
            }

            result = new RtpMapAttributeValue()
            {
                PayloadType = SessionDescriptorDataConverter.ConvertToByte( tokens[0] )
            };

            if ( tokens.Length > 1 )
            {
                var elements = tokens[ 1 ].Split( new char[] { '/' } );

                if ( elements != null )
                {
                    if ( elements.Length > 0 )
                    {
                        result.Encoding = elements[0];
                    }

                    if ( elements.Length > 1 )
                    {
                        result.ClockRate = SessionDescriptorDataConverter.ConvertToUInt( elements[1] );
                    }
                }
            }

            for ( int i = 2 ; i < tokens.Length ; ++i )
            {
                result.Extensions.TryAdd( tokens[i] );
            }

            return true;
        }
    }
}

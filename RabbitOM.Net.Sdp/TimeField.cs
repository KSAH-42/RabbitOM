using System;
using System.Text;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the sdp field
    /// </summary>
    public sealed class TimeField : BaseField
    {
        /// <summary>
        /// Represent the type name
        /// </summary>
        public const string       TypeNameValue          = "t";




        private long              _startTime             = 0;

        private long              _stopTime              = 0;




        /// <summary>
        /// Construct
        /// </summary>
        public TimeField()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="startTime">the start time</param>
        /// <param name="stopTime">the stop time</param>
        public TimeField( long startTime , long stopTime )
        {
            _startTime = startTime;
            _stopTime = stopTime;
        }




        /// <summary>
        /// Gets the type name
        /// </summary>
        public override string TypeName
        {
            get => TypeNameValue;
        }

        /// <summary>
        /// Gets / Sets the start time
        /// </summary>
        public long StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        /// <summary>
        /// Gets / Sets the stop time
        /// </summary>
        public long StopTime
        {
            get => _stopTime;
            set => _stopTime = value;
        }




        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return 0 <= _startTime && _startTime <= _stopTime;
        }

        /// <summary>
        /// Format the field
        /// </summary>
        /// <returns>retuns a value</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append( _startTime );
            builder.Append( " " );

            builder.Append( _stopTime );

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
        public static TimeField Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(value));
            }

            if (!TryParse(value, out TimeField result) || result == null)
            {
                throw new FormatException();
            }

            return result;
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the field result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( string value , out TimeField result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            string[] tokens = value.Split( new char[]{ ' ' } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens == null || tokens.Length <= 0 )
            {
                return false;
            }

            result = new TimeField()
            {
                StartTime = SessionDescriptorDataConverter.ConvertToLong( tokens.Length > 0 ? tokens[0] : string.Empty ) ,
                StopTime = SessionDescriptorDataConverter.ConvertToLong( tokens.Length > 1 ? tokens[1] : string.Empty ) ,
            };

            return true;
        }
    }
}

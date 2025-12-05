using System;
using System.Linq;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent a string data pair
    /// </summary>
    public struct RtspStringPair
    {
        /// <summary>
        /// Represent an empty value
        /// </summary>
        public static RtspStringPair Empty = new RtspStringPair( string.Empty , string.Empty );




        private readonly string _first;

        private readonly string _second;




        /// <summary>
        /// Initialize an new instance
        /// </summary>
        /// <param name="first">the first</param>
        /// <param name="second">the second</param>
        public RtspStringPair( string first , string second )
        {
            _first  = RtspDataConverter.Trim( first );
            _second = RtspDataConverter.Trim( second );
        }




        /// <summary>
        /// Gets the first value
        /// </summary>
        public string First
        {
            get => _first;
        }

        /// <summary>
        /// Gets the second value
        /// </summary>
        public string Second
        {
            get => _second;
        }




        /// <summary>
        /// Check if the current instance is null or empty
        /// </summary>
        /// <param name="pair">the pair</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool IsNullOrEmpty( RtspStringPair pair )
        {
            return string.IsNullOrEmpty( pair.First ) && string.IsNullOrEmpty( pair.Second );
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="pair">the pair</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse(string value , out RtspStringPair result )
        {
            return TryParse( value , '-' , out result );
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the input value</param>
        /// <param name="separator">the separator</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( string value , char separator , out RtspStringPair result )
        {
            result = default;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var tokens = value.Trim().Split( new char[] { separator } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens.Length <= 0 )
            {
                return false;
            }

            result = new RtspStringPair( tokens[0] , tokens.ElementAtOrDefault(1) );

            return true;
        }




        /// <summary>
        /// Format to a string
        /// </summary>
        /// <returns>return a string</returns>
        public override string ToString()
        {
            return $"{_first}-{_second}";
        }
    }
}

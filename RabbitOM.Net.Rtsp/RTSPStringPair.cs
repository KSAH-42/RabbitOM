using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a string data pair
    /// </summary>
    public sealed class RTSPStringPair
    {
        private string _first  = string.Empty;

        private string _second = string.Empty;





        /// <summary>
        /// Gets / Sets the first value
        /// </summary>
        public string First
        {
            get => _first;
            set => _first = RTSPDataFilter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the second value
        /// </summary>
        public string Second
        {
            get => _second;
            set => _second = RTSPDataFilter.Trim( value );
        }





        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _first = string.Empty;
            _second = string.Empty;
        }

        /// <summary>
        /// Format to a string
        /// </summary>
        /// <returns>return a string</returns>
        public override string ToString()
        {
            return $"{_first}-{_second}";
        }

        /// <summary>
        /// Perform a validation
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        internal bool Validate()
        {
            return !string.IsNullOrWhiteSpace( _first ) || !string.IsNullOrWhiteSpace( _second );
        }

        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="value">the input value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        internal bool Decode( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var tokens = value.Trim().Split( new char[] { '-' } );

            if ( tokens == null || tokens.Length <= 0 )
            {
                return false;
            }

            First = tokens[0];
            Second = tokens.Length > 1 ? tokens[1] : string.Empty;

            return true;
        }
    }
}

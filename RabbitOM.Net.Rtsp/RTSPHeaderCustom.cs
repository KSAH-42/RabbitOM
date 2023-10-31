using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderCustom : RTSPHeader, IRTSPHeaderValue<string>
    {
        private readonly string _name  = string.Empty;

        private string          _value = string.Empty;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">the name</param>
        /// <param name="value">the value</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPHeaderCustom( string name , string value )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentNullException( nameof( name ) );
            }

            _name  = RTSPDataFilter.Trim( name );
            _value = RTSPDataFilter.Trim( value );
        }



        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => _name;
        }

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public string Value
        {
            get => _value;
            set => _value = RTSPDataFilter.Trim( value );
        }



        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return !string.IsNullOrWhiteSpace( _value );
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            return _value;
        }
    }
}

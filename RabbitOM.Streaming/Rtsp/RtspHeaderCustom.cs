using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderCustom : RtspHeader, IRtspHeaderValue<string>
    {
        private readonly string _name  = string.Empty;

        private string          _value = string.Empty;









        /// <summary>
        /// Initialize a new instance of a header class
        /// </summary>
        /// <param name="name">the name</param>
        /// <param name="value">the value</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspHeaderCustom( string name , string value )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentNullException( nameof( name ) );
            }

            _name  = RtspDataConverter.Trim( name );
            _value = RtspDataConverter.Trim( value );
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
            set => _value = RtspDataConverter.Trim( value );
        }






        /// <summary>
        /// Try validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return ! string.IsNullOrWhiteSpace( _value );
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

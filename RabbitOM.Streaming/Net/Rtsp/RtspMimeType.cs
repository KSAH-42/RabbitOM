using System;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent the mime types
    /// </summary>
    public class RtspMimeType
    {
        /// <summary>
        /// Represent an empty value
        /// </summary>
        public readonly static RtspMimeType Empty            = new RtspMimeType();

        /// <summary>
        /// Represent the sdp mime type
        /// </summary>
        public readonly static RtspMimeType ApplicationSdp   = new RtspMimeType( "application" , "sdp" );

        /// <summary>
        /// Represent the text mime type
        /// </summary>
        public readonly static RtspMimeType ApplicationText  = new RtspMimeType( "application" , "text" );

        /// <summary>
        /// Represent the text mime type
        /// </summary>
        public readonly static RtspMimeType TextPlain        = new RtspMimeType( "text" , "plain" );

        /// <summary>
        /// Represent the text mime type
        /// </summary>
        public readonly static RtspMimeType TextParameters   = new RtspMimeType( "text" , "parameters" );






        private readonly string _type    = string.Empty;

        private readonly string _subType = string.Empty;






        /// <summary>
        /// Constructor
        /// </summary>
        private RtspMimeType()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">the type</param>
        /// <param name="subType">the sub type</param>
        public RtspMimeType( string type , string subType )
        {
            _type    = RtspDataConverter.Trim( type );
            _subType = RtspDataConverter.Trim( subType );
        }






        /// <summary>
        /// Gets the type
        /// </summary>
        public string Type
        {
            get => _type;
        }

        /// <summary>
        /// Gets the sub type
        /// </summary>
        public string SubType
        {
            get => _subType;
        }

        /// <summary>
        /// Format to a string value
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( _type ) )
            {
                return string.Empty;
            }

            if ( string.IsNullOrWhiteSpace( _subType ) )
            {
                return _type;
            }

            return $"{_type}/{_subType}";
        }

        /// <summary>
        /// Try create a mime type object from an input string value
        /// </summary>
        /// <param name="value">the value to be parsed</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( string value , out RtspMimeType result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var tokens = value.Trim().Split( new char[] { '/' } );

            if ( tokens == null || tokens.Length <= 0 )
            {
                return false;
            }

            if ( string.IsNullOrWhiteSpace( tokens[0] ) )
            {
                return false;
            }

            result = new RtspMimeType( tokens[0] , tokens.Length > 1 ? tokens[1] : string.Empty );

            return true;
        }

    }
}

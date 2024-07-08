using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent the mime types
    /// </summary>
    public class RTSPMimeType
    {
        /// <summary>
        /// Represent an empty value
        /// </summary>
        public readonly static RTSPMimeType Empty            = new RTSPMimeType();

        /// <summary>
        /// Represent the sdp mime type
        /// </summary>
        public readonly static RTSPMimeType ApplicationSdp   = new RTSPMimeType( "application" , "sdp" );

        /// <summary>
        /// Represent the text mime type
        /// </summary>
        public readonly static RTSPMimeType ApplicationText  = new RTSPMimeType( "application" , "text" );

        /// <summary>
        /// Represent the text mime type
        /// </summary>
        public readonly static RTSPMimeType TextPlain        = new RTSPMimeType( "text" , "plain" );

        /// <summary>
        /// Represent the text mime type
        /// </summary>
        public readonly static RTSPMimeType TextParameters   = new RTSPMimeType( "text" , "parameters" );






        private readonly string _type    = string.Empty;

        private readonly string _subType = string.Empty;






        /// <summary>
        /// Constructor
        /// </summary>
        private RTSPMimeType()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">the type</param>
        /// <param name="subType">the sub type</param>
        public RTSPMimeType( string type , string subType )
        {
            _type    = RTSPDataConverter.Trim( type );
            _subType = RTSPDataConverter.Trim( subType );
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
        public static bool TryParse( string value , out RTSPMimeType result )
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

            result = new RTSPMimeType( tokens[0] , tokens.Length > 1 ? tokens[1] : string.Empty );

            return true;
        }

    }
}

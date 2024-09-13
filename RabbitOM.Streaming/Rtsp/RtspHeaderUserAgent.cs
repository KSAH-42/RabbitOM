using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderUserAgent : RtspHeader
    {
        private string _product  = string.Empty;

        private string _version  = string.Empty;

        private string _comments = string.Empty;






        /// <summary>
        /// Constructor
        /// </summary>
        private RtspHeaderUserAgent()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="product">the product</param>
        public RtspHeaderUserAgent( string product )
            : this( product , "1.0" , string.Empty )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="product">the product</param>
        /// <param name="version">the version</param>
        public RtspHeaderUserAgent( string product , string version )
            : this( product , version , string.Empty )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="product">the product</param>
        /// <param name="version">the version</param>
        /// <param name="comments">the comments</param>
        public RtspHeaderUserAgent( string product , string version , string comments )
        {
            Product  = product;
            Version  = version;
            Comments = comments;
        }






        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.UserAgent;
        }

        /// <summary>
        /// Gets / Sets the product name
        /// </summary>
        public string Product
        {
            get => _product;
            set => _product = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the product version
        /// </summary>
        public string Version
        {
            get => _version;
            set => _version = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the comments
        /// </summary>
        public string Comments
        {
            get => _comments;
            set => _comments = RtspDataConverter.Trim( value );
        }






        /// <summary>
        /// Try validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return ! string.IsNullOrWhiteSpace( _product )
                && ! string.IsNullOrWhiteSpace( _version );
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            var writer = new RtspHeaderWriter( RtspSeparator.Slash );

            writer.Write( _product );
            writer.WriteSeparator();
            writer.Write( _version );

            if ( !string.IsNullOrWhiteSpace( _comments ) )
            {
                writer.WriteSpace();
                writer.Write( _comments );
            }

            return writer.Output;
        }






        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderUserAgent result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var tokens = value.Trim().Split( new char[] { '/' } );

            if ( tokens == null || tokens.Length <= 1 )
            {
                return false;
            }

            var parts = tokens[ 1 ].Split( new char[] { ' ' } , StringSplitOptions.RemoveEmptyEntries );

            if ( parts == null || parts.Length < 1 )
            {
                return false;
            }

            result = new RtspHeaderUserAgent()
            {
                Product  = tokens[ 0 ] ,
                Version  = parts [ 0 ] ,
                Comments = parts.Length > 1 ? string.Join( " " , parts , 1 , parts.Length - 1 ) : string.Empty ,
            };

            return true;
        }
    }
}

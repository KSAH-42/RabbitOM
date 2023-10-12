using System;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderUserAgent : RTSPHeader
    {
        private string _product  = string.Empty;

        private string _version  = string.Empty;

        private string _comments = string.Empty;




        /// <summary>
        /// Constructor
        /// </summary>
        private RTSPHeaderUserAgent()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="product">the product</param>
        public RTSPHeaderUserAgent( string product )
            : this( product , "1.0" , string.Empty )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="product">the product</param>
        /// <param name="version">the version</param>
        public RTSPHeaderUserAgent( string product , string version )
            : this( product , version , string.Empty )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="product">the product</param>
        /// <param name="version">the version</param>
        /// <param name="comments">the comments</param>
        public RTSPHeaderUserAgent( string product , string version , string comments )
        {
            Product = product;
            Version = version;
            Comments = comments;
        }




        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RTSPHeaderNames.UserAgent;
        }

        /// <summary>
        /// Gets / Sets the product name
        /// </summary>
        public string Product
        {
            get => _product;
            set => _product = RTSPDataFilter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the product version
        /// </summary>
        public string Version
        {
            get => _version;
            set => _version = RTSPDataFilter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the comments
        /// </summary>
        public string Comments
        {
            get => _comments;
            set => _comments = RTSPDataFilter.Trim( value );
        }



        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool Validate()
        {
            return !string.IsNullOrWhiteSpace( _product )
                && !string.IsNullOrWhiteSpace( _version );
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            var writer = new RTSPHeaderWriter( RTSPSeparator.Slash );

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
        public static bool TryParse( string value , out RTSPHeaderUserAgent result )
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

            result = new RTSPHeaderUserAgent()
            {
                Product = tokens[0] ,
                Version = parts[0] ,
                Comments = parts.Length > 1 ? string.Join( " " , parts , 1 , parts.Length - 1 ) : string.Empty ,
            };

            return true;
        }
    }
}

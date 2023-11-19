using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a parser class
    /// </summary>
    internal sealed class RTSPParser
    {
        private RTSPSeparator                 _headerSeparator     = RTSPSeparator.Comma;

        private RTSPOperator                  _fieldOperator       = RTSPOperator.Equality;

        private string                        _text                = string.Empty;

        private string                        _firstElement        = string.Empty;

        private readonly RTSPStringCollection _headers             = new RTSPStringCollection();





        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPParser()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">the text</param>
        /// <param name="headerSepartor">the header separator</param>
        public RTSPParser( string text , RTSPSeparator headerSepartor )
        {
            Text = text;
            HeaderSeparator = headerSepartor;
        }





        /// <summary>
        /// Gets / Sets the header separator
        /// </summary>
        public RTSPSeparator HeaderSeparator
        {
            get => _headerSeparator;
            set => _headerSeparator = value;
        }

        /// <summary>
        /// Gets / Sets the field operator
        /// </summary>
        public RTSPOperator FieldOperator
        {
            get => _fieldOperator;
            set => _fieldOperator = value;
        }

        /// <summary>
        /// Gets / Sets the text
        /// </summary>
        public string Text
        {
            get => _text;
            set => _text = RTSPDataFilter.Trim( value );
        }





        /// <summary>
        /// Gets the first parsed element
        /// </summary>
        /// <returns>returns a string</returns>
        public string GetFirstElementParsed()
        {
            return _firstElement;
        }

        /// <summary>
        /// Gets the first parsed element
        /// </summary>
        /// <returns>returns a string</returns>
        public RTSPAuthenticationType FirstAuthenticationTypeOrDefault()
        {
            return RTSPDataConverter.ConvertToEnum<RTSPAuthenticationType>( _firstElement );
        }

        /// <summary>
        /// Gets the parsed headers
        /// </summary>
        /// <returns>returns a collection of headers</returns>
        public RTSPStringCollection GetParsedHeaders()
        {
            return _headers;
        }

        /// <summary>
        /// Parse the header
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool ParseHeaders()
        {
            _headers.Clear();

            if ( string.IsNullOrWhiteSpace( _text ) )
            {
                return false;
            }

            var tokens = _text.Split( new char[] { (char) _headerSeparator } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens == null || tokens.Length <= 0 )
            {
                return false;
            }

            foreach ( string token in tokens )
            {
                _headers.TryAdd( token.Trim() );
            }

            return _headers.Any();
        }

        /// <summary>
        /// Parse the header
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool ParseFirstElement()
        {
            _firstElement = string.Empty;

            if ( string.IsNullOrWhiteSpace( _text ) )
            {
                return false;
            }

            var tokens = _text.Split( new char[] { ' ' } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens == null || tokens.Length <= 1 )
            {
                return false;
            }

            _firstElement = tokens[0].Trim();

            return !string.IsNullOrWhiteSpace( _firstElement );
        }

        /// <summary>
        /// Remove the first sequence of characters from the input text
        /// </summary>
        /// <param name="sequence">the sequence</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool RemoveFirstSequence( string sequence )
        {
            if ( string.IsNullOrWhiteSpace( sequence ) )
            {
                return false;
            }

            if ( string.IsNullOrWhiteSpace( _text ) )
            {
                return false;
            }

            var index = _text.IndexOf( sequence , 0 , StringComparison.OrdinalIgnoreCase );

            if ( index < 0 )
            {
                return false;
            }

            Text = _text.Remove( 0 , index + sequence.Length );

            return true;
        }
    }
}

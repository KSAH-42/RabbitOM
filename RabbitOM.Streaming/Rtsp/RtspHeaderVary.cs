using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderVary : RtspHeader
    {
        private readonly HashSet<string> _headersNames = new HashSet<string>();






        /// <summary>
        /// Gets the maximum allowed headers names
        /// </summary>
        public static int MaximumOfHeadersNames 
        {
            get => 500;
        }

        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.Vary;
        }

        /// <summary>
        /// Gets the headers names
        /// </summary>
        public IReadOnlyCollection<string> HeadersNames
        {
            get => _headersNames;
        }






        /// <summary>
        /// Can add a header name
        /// </summary>
        /// <param name="name">the header name</param>
        /// <exception cref="ArgumentException"/>
        public bool CanAddHeaderName( string name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                return false;
            }

            return ! _headersNames.Contains( name ) && _headersNames.Count < MaximumOfHeadersNames;
        }

        /// <summary>
        /// Add a new header name
        /// </summary>
        /// <param name="name">the header name</param>
        /// <exception cref="InvalidOperationException"/>
        public void AddHeaderName( string name )
        {
            if ( ! CanAddHeaderName( name ) || ! _headersNames.Add( name ) )
            {
                throw new InvalidOperationException( nameof( name ) );
            }
        }

        /// <summary>
        /// Add a new header name
        /// </summary>
        /// <param name="name">the name</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddHeaderName( string name )
        {
            return CanAddHeaderName( name ) && _headersNames.Add( name );
        }

        /// <summary>
        /// Remove an existing header name
        /// </summary>
        /// <param name="name">the name</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool RemoveHeaderName( string name )
        {
            return _headersNames.Remove( name );
        }

        /// <summary>
        /// Remove all header names
        /// </summary>
        public void RemoveAllHeadersNames()
        {
            _headersNames.Clear();
        }
        





        /// <summary>
        /// Try validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _headersNames.Count > 0;
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            var writer = new RtspHeaderWriter( RtspSeparator.Comma );

            foreach ( var header in _headersNames )
            {
                if ( string.IsNullOrWhiteSpace( header ) )
                {
                    continue;
                }

                if ( writer.IsAppended )
                {
                    writer.WriteSeparator();
                    writer.WriteSpace();
                }

                writer.Write( header );
            }

            return writer.Output;
        }






        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderVary result )
        {
            result = null;

            var parser = new RtspParser( value , RtspSeparator.Comma );

            if ( ! parser.ParseHeaders() )
            {
                return false;
            }

            using ( var reader = new RtspHeaderReader( parser.GetParsedHeaders() ) )
            {
                result = new RtspHeaderVary();

                while ( reader.Read() )
                {
                    result.TryAddHeaderName( reader.GetElement() );
                }

                return true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    /// <summary>
    /// Represent a rtsp header
    /// </summary>
    public sealed class ConnectionRtspHeader 
    {
        /// <summary>
        /// The type name
        /// </summary>
        public static readonly string TypeName = "Connection";





        private readonly HashSet<string> _directives = new HashSet<string>( StringComparer.OrdinalIgnoreCase );
        




        /// <summary>
        /// Gets the directives
        /// </summary>
        public IReadOnlyCollection<string> Directives
        {
            get => _directives;
        }
        




        /// <summary>
        /// Try to add an element
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool AddDirective( string value )
        {
            var directive = RtspHeaderValueNormalizer.Normalize( value );

            if ( ! char.IsLetter( directive.FirstOrDefault() ) || ! char.IsLetterOrDigit( directive.LastOrDefault() ) )
            {
                return false;
            }

            if ( directive.Any( c => char.IsSeparator( c ) ) )
            {
                return false;
            }

            if ( directive.Any( c => char.IsPunctuation( c ) && c != '-' && c != '_' ) )
            {
                return false;
            }

            return _directives.Add( directive );
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="value">the value</param>
        public bool RemoveDirective( string value )
        {
            return _directives.Remove( RtspHeaderValueNormalizer.Normalize( value ) );
        }

        /// <summary>
        /// Remove all elements
        /// </summary>
        public void RemoveDirectives()
        {
            _directives.Clear();
        }

        /// <summary>
        /// Format to string
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            return string.Join( ", " , _directives );
        }






        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( string input , out ConnectionRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                var header = new ConnectionRtspHeader();

                foreach ( var token in tokens )
                {
                    header.AddDirective( token );
                }

                if ( header.Directives.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}

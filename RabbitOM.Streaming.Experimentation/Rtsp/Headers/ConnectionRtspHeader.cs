using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    /// <summary>
    /// Represent a rtsp header
    /// </summary>
    public class ConnectionRtspHeader : RtspHeader 
    {
        /// <summary>
        /// The type name
        /// </summary>
        public const string TypeName = "Connection";





        private readonly HashSet<string> _directives = new HashSet<string>();
        




        /// <summary>
        /// Gets the directives
        /// </summary>
        public IReadOnlyCollection<string> Directives
        {
            get => _directives;
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

            if ( RtspHeaderParser.TryParse( StringRtspNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                var header = new ConnectionRtspHeader();

                foreach ( var token in tokens )
                {
                    header.TryAddDirective( token );
                }

                if ( header.Directives.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }





        /// <summary>
        /// Try to validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _directives.Count > 0 && _directives.All( StringRtspValidator.TryValidate );
        }

        /// <summary>
        /// Try to add an element
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddDirective( string value )
        {
            var text = StringRtspNormalizer.Normalize( value );

            if ( StringRtspValidator.TryValidateAsContentSTD( text ) )
            {
                return _directives.Add( text );
            }

            return false;
        }

        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="value">the value</param>
        /// <exception cref="ArgumentException"/>
        public void AddDirective( string value )
        {
            if ( ! TryAddDirective( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="value">the value</param>
        public void RemoveDirective( string value )
        {
            _directives.Remove( StringRtspNormalizer.Normalize( value ) );
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
    }
}

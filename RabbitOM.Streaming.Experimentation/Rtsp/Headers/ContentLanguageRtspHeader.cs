using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    /// <summary>
    /// Represent a rtsp header
    /// </summary>
    public sealed class ContentLanguageRtspHeader : RtspHeader 
    {
        public const string TypeName = "Content-Language";
        




        private readonly HashSet<string> _languages = new HashSet<string>( StringComparer.OrdinalIgnoreCase );
        




        /// <summary>
        /// Gets the languages
        /// </summary>
        public IReadOnlyCollection<string> Languages
        {
            get => _languages;
        }
        




        /// <summary>
        /// Try to parse 
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( string input , out ContentEncodingRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( StringRtspNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                var header = new ContentEncodingRtspHeader();

                foreach ( var token in tokens )
                {
                    header.TryAddLanguage( token );
                }

                if ( header.Encodings.Count > 0 )
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
            return _languages.Count > 0;
        }

        /// <summary>
        /// Try to an element
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddLanguage( string value )
        {
            var language = StringRtspNormalizer.Normalize( value );

            if ( ! string.IsNullOrWhiteSpace( language ) )
            {
                return _languages.Add( language );
            }

            return false;
        }


        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="value">the value</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public void AddEncoding( string value )
        {
            if ( ! TryAddLanguage( value ?? throw new ArgumentNullException( nameof( value ) ) ) )
            {
                throw new ArgumentException( nameof( value ) );
            }
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="value">the value</param>
        public void RemoveLanguage( string value )
        {
            _languages.Remove( StringRtspNormalizer.Normalize( value ) );
        }

        /// <summary>
        /// Remove all elements
        /// </summary>
        public void RemoveLanguages()
        {
            _languages.Clear();
        }

        /// <summary>
        /// Format to string
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            return string.Join( ", " , _languages );
        }
    }
}

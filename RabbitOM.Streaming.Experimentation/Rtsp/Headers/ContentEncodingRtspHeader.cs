using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    /// <summary>
    /// Represent a rtsp header
    /// </summary>
    public class ContentEncodingRtspHeader : RtspHeader 
    {
        /// <summary>
        /// The name
        /// </summary>
        public const string TypeName = "Content-Encoding";
        




        private readonly HashSet<string> _encodings = new HashSet<string>( StringComparer.OrdinalIgnoreCase );
        




        /// <summary>
        /// Gets the encoding
        /// </summary>
        public IReadOnlyCollection<string> Encodings
        {
            get => _encodings;
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
            return _encodings.Count > 0;
        }

        /// <summary>
        /// Try to an element
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddLanguage( string value )
        {
            var encoding = StringRtspNormalizer.Normalize( value );

            if ( StringRtspValidator.TryValidateAsContentTD( encoding ) )
            {
                return _encodings.Add( encoding );
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
        public void RemoveEncoding( string value )
        {
            _encodings.Remove( StringRtspNormalizer.Normalize( value ) );
        }

        /// <summary>
        /// Remove all elements
        /// </summary>
        public void RemoveEncodings()
        {
            _encodings.Clear();
        }

        /// <summary>
        /// Format to string
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            return string.Join( ", " , _encodings );
        }
    }
}

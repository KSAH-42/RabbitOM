using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    /// <summary>
    /// Represent an rtsp header
    /// </summary>
    public class AcceptLanguageRtspHeader : RtspHeader 
    {
        /// <summary>
        /// The type name
        /// </summary>
        public const string TypeName = "Accept-Language";





        private readonly List<StringWithQualityRtspHeaderValue> _languages = new List<StringWithQualityRtspHeaderValue>();




        /// <summary>
        /// Gets the languages
        /// </summary>
        public IReadOnlyList<StringWithQualityRtspHeaderValue> Languages
        {
            get => _languages;
        }




        public static bool TryParse( string input , out AcceptLanguageRtspHeader result )
        {
            result = null;

            if ( ! RtspHeaderParser.TryParse( StringRtspNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                return false;
            }

            var header = new AcceptLanguageRtspHeader();

            foreach ( var token in tokens )
            {
                if ( StringWithQualityRtspHeaderValue.TryParse( token , out var encoding ) )
                {
                    header.TryAddLanguage( encoding );
                }
            }
            
            if ( header.Languages.Count <= 0 )
            {
                return false;
            }

            result = header;

            return true;
        }




        /// <summary>
        /// Try to validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false.</returns>
        public override bool TryValidate()
        {
            return _languages.Count > 0;
        }

        /// <summary>
        /// Try to add an element
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool TryAddLanguage( StringWithQualityRtspHeaderValue value )
        {
            if ( StringWithQualityRtspHeaderValue.IsNullOrEmpty( value ) )
            {
                return false;
            }

            _languages.Add( value );

            return true;
        }

        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="value">the value</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public void AddLanguage( StringWithQualityRtspHeaderValue value )
        {
            if ( value == null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            if ( string.IsNullOrWhiteSpace( value.Name ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            _languages.Add( value );
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="value">the value</param>
        public void RemoveLanguage( StringWithQualityRtspHeaderValue value )
        {
            _languages.Remove( value );
        }

        /// <summary>
        /// Remove all elements
        /// </summary>
        public void RemoveLanguages()
        {
            _languages.Clear();
        }

        /// <summary>
        /// Format to a string
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            return string.Join( ", " , _languages );
        }
    } 
}

using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    /// <summary>
    /// Represent an rtsp header
    /// </summary>
    public sealed class AcceptEncodingRtspHeader : RtspHeader 
    {
        /// <summary>
        /// The type name
        /// </summary>
        public const string TypeName = "Accept-Encoding";





        private readonly List<StringWithQualityRtspHeaderValue> _encodings = new List<StringWithQualityRtspHeaderValue>();
        




        /// <summary>
        /// Gets the encodings
        /// </summary>
        public IReadOnlyList<StringWithQualityRtspHeaderValue> Encodings
        {
            get => _encodings;
        }
        




        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( string input , out AcceptEncodingRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( StringRtspNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                var header = new AcceptEncodingRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( StringWithQualityRtspHeaderValue.TryParse( token , out var encoding ) )
                    {
                        header.TryAddEncoding( encoding );
                    }
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
        /// Try add an element
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddEncoding( StringWithQualityRtspHeaderValue value )
        {
            if ( StringWithQualityRtspHeaderValue.IsNullOrEmpty( value ) )
            {
                return false;
            }
            
            foreach ( var encoding in _encodings )
            {
                if ( StringWithQualityRtspHeaderValue.Equals( encoding , value ) )
                {
                    return false;
                }
            }

            _encodings.Add( value );

            return true;
        }

        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="value">the value</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public void AddEncoding( StringWithQualityRtspHeaderValue value )
        {
            if ( ! TryAddEncoding( value ?? throw new ArgumentNullException( nameof( value ) ) ) )
            {
                throw new ArgumentException( nameof( value ) );
            }
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="value">the value</param>
        public void RemoveEncoding( StringWithQualityRtspHeaderValue value )
        {
            _encodings.Remove( value );
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

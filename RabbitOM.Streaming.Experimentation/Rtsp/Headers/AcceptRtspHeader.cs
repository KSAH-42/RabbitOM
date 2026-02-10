using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    /// <summary>
    /// Represent an rtsp header
    /// </summary>
    public sealed class AcceptRtspHeader : RtspHeader 
    {
        /// <summary>
        /// The type name
        /// </summary>
        public const string TypeName = "Accept";
        




        private readonly List<StringWithQualityRtspHeaderValue> _mimes = new List<StringWithQualityRtspHeaderValue>();
        




        /// <summary>
        /// Gets the mimes
        /// </summary>
        public IReadOnlyList<StringWithQualityRtspHeaderValue> Mimes
        {
            get => _mimes;
        }
        




        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( string input , out AcceptRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( StringRtspNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                var header = new AcceptRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( StringWithQualityRtspHeaderValue.TryParse( token , out var mime ) )
                    {
                        header.TryAddMime( mime );
                    }
                }
            
                if ( header.Mimes.Count > 0 )
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
            return _mimes.Count > 0;
        }

        /// <summary>
        /// Try add an element
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddMime( StringWithQualityRtspHeaderValue value )
        {
            if ( StringWithQualityRtspHeaderValue.IsNullOrEmpty( value ) )
            {
                return false;
            }

            _mimes.Add( value );

            return true;
        }

        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="value">the value</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public void AddMime( StringWithQualityRtspHeaderValue value )
        {
            if ( value == null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            if ( string.IsNullOrWhiteSpace( value.Name ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            _mimes.Add( value );
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="value">the value</param>
        public void RemoveMime( StringWithQualityRtspHeaderValue value )
        {
            _mimes.Remove( value );
        }

        /// <summary>
        /// Remove all elements
        /// </summary>
        public void RemoveMimes()
        {
            _mimes.Clear();
        }

        /// <summary>
        /// Format to string
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            return string.Join( ", " , _mimes );
        }
    }
}

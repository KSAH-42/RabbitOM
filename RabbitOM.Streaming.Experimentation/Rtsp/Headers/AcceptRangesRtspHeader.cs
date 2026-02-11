using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    /// <summary>
    /// Represent an rtsp header
    /// </summary>
    public class AcceptRangesRtspHeader : RtspHeader 
    {
        /// <summary>
        /// The type name
        /// </summary>
        public const string TypeName = "Accept-Ranges";
        




        private readonly HashSet<string> _units = new HashSet<string>();
        




        /// <summary>
        /// Gets the units
        /// </summary>
        public IReadOnlyCollection<string> Units
        {
            get => _units;
        }
        




        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( string input , out AcceptRangesRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( StringRtspNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                var header = new AcceptRangesRtspHeader();

                foreach ( var token in tokens )
                {
                    header.TryAddUnit( token );
                }

                if ( header.Units.Count > 0 )
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
            return _units.Count > 0 && _units.All( StringRtspValidator.TryValidate );
        }

        /// <summary>
        /// Try to add an element
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddUnit( string value )
        {
            var text = StringRtspNormalizer.Normalize( value );

            if ( StringRtspValidator.TryValidateAsContentSTD( text ) )
            {
                return _units.Add( text );
            }
            
            return false;
        }

        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="value">the value</param>
        /// <exception cref="ArgumentException"/>
        public void AddUnit( string value )
        {
            if ( ! TryAddUnit( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="value">the value</param>
        public void RemoveUnit( string value )
        {
            _units.Remove( StringRtspNormalizer.Normalize( value ) );
        }

        /// <summary>
        /// Remove all elements
        /// </summary>
        public void RemoveUnits()
        {
            _units.Clear();
        }

        /// <summary>
        /// Format to string
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            return string.Join( ", " , _units );
        }
    }
}

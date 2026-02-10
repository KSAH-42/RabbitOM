using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    /// <summary>
    /// Represent a rtsp header
    /// </summary>
    public class AllowRtspHeader : RtspHeader 
    {
        /// <summary>
        /// The type name
        /// </summary>
        public const string TypeName = "Allow";





        private readonly HashSet<RtspMethod> _methods = new HashSet<RtspMethod>();
        




        /// <summary>
        /// Gets the methods
        /// </summary>
        public IReadOnlyCollection<RtspMethod> Methods
        {
            get => _methods;
        }
        




        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( string input , out AllowRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( StringRtspNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                var header = new AllowRtspHeader();

                foreach( var token in tokens )
                {
                    if ( RtspMethod.TryParse( token , out var method ) )
                    {
                        header.TryAddMethod( method );
                    }
                }

                if ( header.Methods.Count > 0 )
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
            return _methods.Count > 0;
        }

        /// <summary>
        /// Try add an element
        /// </summary>
        /// <param name="method">the element</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddMethod( RtspMethod method )
        {
            if ( method == null )
            {
                return false;
            }

            return _methods.Add( method );
        }

        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="method">the element</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public void AddMethod( RtspMethod method )
        {
            if ( ! _methods.Add( method ?? throw new ArgumentNullException( nameof( method ) ) ) )
            {
                throw new ArgumentException( "the method is already added" , nameof( method ) );
            }
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="method">the element</param>
        public void RemoveMethod( RtspMethod method )
        {
            _methods.Remove( method );
        }

        /// <summary>
        /// Remove all elements
        /// </summary>
        public void RemoveMethods()
        {
            _methods.Clear();
        }
        
        /// <summary>
        /// Format to string
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            return string.Join( ", " , _methods );
        }
    }
}

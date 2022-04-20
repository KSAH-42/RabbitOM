using System;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the data field token
    /// </summary>
    public sealed class StringPair
    {
        private readonly string _first  = string.Empty;

        private readonly string _second = string.Empty;



        /// <summary>
        /// Constructor
        /// </summary>
        private StringPair()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="first">the first element</param>
        public StringPair( string first )
            : this( first , string.Empty )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="first">the first element</param>
        /// <param name="second">the second element</param>
        public StringPair( string first , string second )
        {
            _first  = first  ?? string.Empty;
            _second = second ?? string.Empty;
        }




        /// <summary>
        /// Represent a empty value
        /// </summary>
        public readonly static StringPair Empty = new StringPair();



        /// <summary>
        /// Gets the first element
        /// </summary>
        public string First
        {
            get => _first;
        }

        /// <summary>
        /// Gets the second element
        /// </summary>
        public string Second
        {
            get => _second;
        }
    }
}

using System;
using System.Linq;

namespace RabbitOM.Streaming.Sdp.Serialization.Formatters
{
    /// <summary>
    /// Represent a class used to split a string into a array of string based on a filter
    /// </summary>
    public sealed class Tokenizer
    {
        private static readonly string[] EmptyTokens = new string[0];

        private static readonly char[] DefaultSperators = new char[] { ' ' };





        private readonly object _lock = new object(); 
        
        private Func<string,string> _filter;

         





        /// <summary>
        /// Split a string into an array
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a string[]</returns>

        public string[] Split( string value )
        {
            return Split( value , DefaultSperators );
        }

        /// <summary>
        /// Split a string into an array
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="separator">the separator</param>
        /// <returns>returns a string[]</returns>
        public string[] Split( string value , char separator )
        {
            return Split( value , new char[] { separator } );
        }

        /// <summary>
        /// Split a string into an array
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="separators">the separators</param>
        /// <returns>returns a string[]</returns>
        public string[] Split( string value , char[] separators )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return EmptyTokens;
            }

            if ( separators == null || separators.Length == 0 )
            {
                return EmptyTokens;
            }

            lock ( _lock )
            {
                string text = _filter?.Invoke( value ) ?? value;

                return text.Split( separators , StringSplitOptions.RemoveEmptyEntries ).ToArray();
            }
        }

        /// <summary>
        /// Set the filter
        /// </summary>
        /// <param name="filter">the filter</param>
        public void SetFilter( Func<string,string> filter )
        {
            lock ( _lock )
            {
                _filter = filter;
            }
        }

        /// <summary>
        /// Remove the filter
        /// </summary>
        public void RemoveFilter()
        {
            lock ( _lock )
            {
                _filter = null;
            }
        }
    }
}

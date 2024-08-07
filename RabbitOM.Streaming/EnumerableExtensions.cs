using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitOM.Streaming
{
    /// <summary>
    /// Represent an extension class of enumerable collection
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Execute an action on each element of a collection
        /// </summary>
        /// <typeparam name="TSource">the type of source</typeparam>
        /// <param name="source">the source</param>
        /// <param name="action">the action</param>
        /// <exception cref="ArgumentNullException"/>
        public static void ForEach<TSource>( this IEnumerable<TSource> source , Action<TSource> action )
        {
            if ( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            if ( action == null )
            {
                throw new ArgumentNullException( nameof( action ) );
            }

            foreach ( TSource element in source )
            {
                action( element );
            }
        }

        /// <summary>
        /// Format to string a collection
        /// </summary>
        /// <typeparam name="TSource">the type of source</typeparam>
        /// <param name="source">the source</param>
        /// <returns>returns a string</returns>
        /// <exception cref="ArgumentNullException"/>
        public static string Dump<TSource>( this IEnumerable<TSource> source )
        {
            if ( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            StringBuilder builder = new StringBuilder();

            foreach ( var element in source )
            {
                string text = element.ToString() ?? string.Empty;

                if ( ! string.IsNullOrEmpty( text ) )
                {
                    builder.AppendLine( text );
                }
            }

            return builder.ToString();
        }
    }
}

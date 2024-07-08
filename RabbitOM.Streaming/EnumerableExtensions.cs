using System;
using System.Collections.Generic;

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
    }
}

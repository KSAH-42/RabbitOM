using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Tests.ConsoleApp
{
    public static class ArrayExtensions
    {
        public static int IndexAfter<TSource>( this IEnumerable<TSource> source , Func<TSource,bool> predicate)
        {
            int index = IndexOf( source , predicate );

            return index >= 0 ? ++ index : -1;
        }

        public static int IndexOf<TSource>( this IEnumerable<TSource> source , Func<TSource,bool> predicate )
        {
            if ( source == null )
                throw new ArgumentNullException( nameof(source) );

            if ( predicate == null )
                throw new ArgumentNullException(nameof(predicate));

            int index = -1;

            foreach ( var element in source )
            {
                index++;

                if ( predicate( element ) )
                    return index;
            }

            return -1;
        }
    }
}

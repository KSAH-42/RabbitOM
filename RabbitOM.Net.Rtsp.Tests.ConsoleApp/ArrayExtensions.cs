using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.Rtsp.Tests.ConsoleApp
{
	public static class ArrayExtensions
    {
        public static int NextIndexOf<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            int index = IndexOf( source , predicate );

            return index >= 0 ? index + 1 : -1;
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

using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitOM
{
    public static class EnumerableExtensions
    {
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

        public static string Dump<TSource>( this IEnumerable<TSource> source )
        {
            if ( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            var builder = new StringBuilder();

            foreach ( var element in source )
            {
                var text = element?.ToString();

                if ( ! string.IsNullOrEmpty( text ) )
                {
                    builder.AppendLine( text );
                }
            }

            return builder.ToString();
        }
    }
}

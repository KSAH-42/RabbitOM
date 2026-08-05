using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RabbitOM.Player.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>( this ObservableCollection<T> source , IEnumerable<T> values )
        {
            if ( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            if ( values == null )
            {
                throw new ArgumentNullException( nameof( values ) );
            }

            foreach ( var value in values )
            {
                source.Add( value );
            }
        }

        public static void RemoveRange<T>( this ObservableCollection<T> source , IEnumerable<T> values )
        {
            if ( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            if ( values == null )
            {
                throw new ArgumentNullException( nameof( values ) );
            }

            foreach ( var value in values )
            {
                source.Remove( value );
            }
        }

        public static void MoveUp<T>( this ObservableCollection<T> source , IEnumerable<T> values )
        {
            if ( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            if ( values == null )
            {
                throw new ArgumentNullException( nameof( values ) );
            }

            if ( source.Count == 0 )
            {
                return;
            }

            foreach ( var value in values )
            {
                var index = source.IndexOf( value );

                if ( index <= 0 )
                {
                    continue;
                }

                source.RemoveAt( index );
                source.Insert( index - 1 , value );
            }
        }

        public static void MoveDown<T>( this ObservableCollection<T> source , IEnumerable<T> values )
        {
            if ( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            if ( values == null )
            {
                throw new ArgumentNullException( nameof( values ) );
            }

            if ( source.Count == 0)
            {
                return;
            }

            foreach ( var value in values.Reverse() )
            {
                var index = source.IndexOf( value );

                if ( index < 0 || source.Count - 1 <= index )
                {
                    continue;
                }

                source.RemoveAt( index );
                source.Insert( index + 1 , value );
            }
        }
    }
}

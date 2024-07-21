using System;
using System.IO;

namespace RabbitOM.Streaming
{
    /// <summary>
    /// Represent a collection extensions class
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Add a 16 bit value
        /// </summary>
        /// <param name="source">the source</param>
        /// <param name="value">the value</param>
        /// <exception cref="ArgumentNullException"/>
        public static void WriteAsInt16( this MemoryStream source , Int16 value )
        {
            if ( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            source.WriteByte( (byte) (( value >> 8 ) & 0xFF ));
            source.WriteByte( (byte) (( value      ) & 0xFF ));
        }

        /// <summary>
        /// Write a buffer
        /// </summary>
        /// <param name="source">the source</param>
        /// <param name="value">the buffer</param>
        public static void WriteAsBinary( this MemoryStream source , byte[] value )
        {
            if ( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            if ( value == null || value.Length == 0 )
            {
                return;
            }

            source.Write( value , 0 , value.Length );
        }

        /// <summary>
        /// Write a buffer
        /// </summary>
        /// <param name="source">the source</param>
        /// <param name="value">the buffer</param>
        public static void WriteAsBinary( this MemoryStream source , ArraySegment<byte> value )
        {
            if ( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            if ( value.Array == null || value.Count == 0 )
            {
                return;
            }

            source.Write( value.Array , value.Offset , value.Count );
        }

        /// <summary>
        /// Add a 16 bit value
        /// </summary>
        /// <param name="source">the source</param>
        /// <param name="value">the value</param>
        /// <exception cref="ArgumentNullException"/>
        public static void WriteAsString( this MemoryStream source , string value )
        {
            if ( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return;
            }

            var buffer = Encoding.UTF8.GetBytes( value );

            if ( buffer == null || buffer.Length == 0 )
            {
                return;
            }

            source.Write( buffer , 0 , buffer.Length );
        }
    }
}

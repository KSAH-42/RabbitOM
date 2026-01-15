using System;
using System.IO;

namespace RabbitOM.Streaming.IO
{
    /// <summary>
    /// Represent a tolerant memory stream wrapper
    /// </summary>
    public sealed class MemoryStreamWriter : IDisposable
    {
        private readonly MemoryStream _stream = new MemoryStream();



        /// <summary>
        /// Gets the capacity
        /// </summary>
        public long Capacity { get => _stream.Capacity; }

        /// <summary>
        /// Gets the length
        /// </summary>
        public long Length { get => _stream.Length; }

        /// <summary>
        /// Gets the position
        /// </summary>
        public long Position { get => _stream.Position; }
        
        /// <summary>
        /// Check if the stream is empty
        /// </summary>
        public bool IsEmpty { get => _stream.Length <= 0; }
        






        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _stream.SetLength( 0 );
        }
        
        /// <summary>
        /// Close
        /// </summary>
        public void Close()
        {
            _stream.Close();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _stream.Dispose();
        }

        /// <summary>
        /// Set the length
        /// </summary>
        /// <param name="value">a positive value</param>
        public void SetLength( long value ) 
        {
            _stream.SetLength( value > 0 ? value : 0 );
        }

        /// <summary>
        /// Sets the position
        /// </summary>
        /// <param name="value">the value</param>
        public void SetPosition( long value )
        { 
            _stream.Position = Numerics.Clamp( value , 0 , _stream.Length );
        }

        /// <summary>
        /// Write a buffer
        /// </summary>
        /// <param name="buffer">the buffer</param>
        public void Write( byte[] buffer )
        {
            if ( buffer == null || buffer.Length <= 0 )
            {
                return;
            }

            _stream.Write( buffer , 0 , buffer.Length );
        }

        /// <summary>
        /// Write
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="offset">the offset</param>
        /// <param name="count">the count</param>
        public void Write( byte[] buffer , int offset , int count )
        {
            if ( buffer == null || buffer.Length <= 0 || offset < 0 || count <= 0 )
            {
                return;
            }

            if ( buffer.Length - offset < count )
            {
                return;
            }

            _stream.Write( buffer , offset , count );
        }

        /// <summary>
        /// Write a buffer
        /// </summary>
        /// <param name="buffer">the buffer</param>
        public void Write( in ArraySegment<byte> buffer )
        {
            if ( buffer.Array == null || buffer.Count <= 0 )
            {
                return;
            }

            _stream.Write( buffer.Array , buffer.Offset , buffer.Count );
        }
        
        /// <summary>
        /// Append
        /// </summary>
        /// <param name="memoryStream">the stream</param>
        public void Write( MemoryStreamWriter memoryStream ) 
        {
            if ( memoryStream == null || memoryStream.IsEmpty )
            {
                return;
            }

            if ( memoryStream._stream.TryGetBuffer( out var buffer ) )
            {
                _stream.Write( buffer.Array , buffer.Offset , buffer.Count );
            }
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteByte( byte value ) 
        {
            _stream.WriteByte( value );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">a value</param>
        public void WriteUInt16( int value )
        {
            _stream.WriteByte( (byte) (( value >> 8 ) & 0xFF ) );
            _stream.WriteByte( (byte) (  value & 0xFF ) );
        }

        /// <summary>
        /// Write a string
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteString( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return;
            }

            var buffer = System.Text.Encoding.UTF8.GetBytes( value );

            if ( buffer.Length > 0 )
            {
                _stream.Write( buffer , 0 , buffer.Length );
            }
        }

        /// <summary>
        /// Create a byte array from the stream
        /// </summary>
        /// <returns>returns a bytes array</returns>
        public byte[] ToArray() 
        {
            return _stream.ToArray();
        }
    }
}

using System;
using System.IO;

namespace RabbitOM.Streaming.Rtp.Framing
{
    /// <summary>
    /// Represent a tolerant memory stream
    /// </summary>
    public sealed class RtpMemoryStream : IDisposable
    {
        private readonly MemoryStream _stream = new MemoryStream( 1024 * 10 );






        /// <summary>
        /// Gets the capacity
        /// </summary>
        public long Capacity 
        { 
            get => _stream.Capacity; 
        }

        /// <summary>
        /// Gets the length
        /// </summary>
        public long Length
        { 
            get => _stream.Length; 
        }

        /// <summary>
        /// Gets the position
        /// </summary>
        public long Position
        { 
            get => _stream.Position; 
        }

        /// <summary>
        /// Check if the stream is empty
        /// </summary>
        public bool IsEmpty
        {
            get => _stream.Length <= 0; 
        }
        






        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose() 
        {
            _stream.Dispose();
        }
        
        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _stream.SetLength( 0 );
        }
        
        /// <summary>
        /// Set the length
        /// </summary>
        /// <param name="value">a positive value</param>
        public void SetLength( long value ) 
        {
            _stream.SetLength( value );
        }

        /// <summary>
        /// Sets the position
        /// </summary>
        /// <param name="value">the value</param>
        public void SetPosition( long value )
        { 
            _stream.Position = value;
        }
        
        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteAsByte( byte value ) 
        {
            _stream.WriteByte( value );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">a value</param>
        public void WriteAsUInt16( int value )
        {
            _stream.WriteByte( (byte) (( value >> 8 ) & 0xFF ) );
            _stream.WriteByte( (byte) (  value & 0xFF ) );
        }

        /// <summary>
        /// Write a buffer
        /// </summary>
        /// <param name="buffer">the buffer</param>
        public void WriteAsBinary( byte[] buffer )
        {
            if ( buffer == null || buffer.Length <= 0 )
            {
                return;
            }

            _stream.Write( buffer , 0 , buffer.Length );
        }

        /// <summary>
        /// Write a buffer
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="offset">the offset</param>
        /// <param name="count">the count</param>
        public void WriteAsBinary( byte[] buffer , int offset , int count )
        {
            if ( buffer == null || buffer.Length <= 0 || count <= 0 || offset < 0 )
            {
                return;
            }

            int delta = buffer.Length - offset;

            if ( 0 < delta && count <= delta )
            {
                _stream.Write( buffer , offset , count );
            }
        }
        
        /// <summary>
        /// Write a buffer
        /// </summary>
        /// <param name="buffer">the buffer</param>
        public void WriteAsBinary( ArraySegment<byte> buffer )
        {
            if ( buffer.Count > 0 )
            {
                _stream.Write( buffer.Array , buffer.Offset , buffer.Count );
            }
        }

        /// <summary>
        /// Write a string
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteAsString( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return;
            }

            var buffer = System.Text.Encoding.ASCII.GetBytes( value );

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

using System;
using System.IO;

namespace RabbitOM.Streaming.IO
{
    /// <summary>
    /// Represent a tolerant memory stream wrapper
    /// </summary>
    public sealed class MemoryStreamBuffer : Stream
    {
        private readonly MemoryStream _stream = new MemoryStream();






        /// <summary>
        /// Gets the capacity
        /// </summary>
        public long Capacity { get => _stream.Capacity; }

        /// <summary>
        /// Gets the length
        /// </summary>
        public override long Length { get => _stream.Length; }

        /// <summary>
        /// Gets the position
        /// </summary>
        public override long Position { get => _stream.Position; set => _stream.Position = MathHelper.Clamp( value , 0 , _stream.Length ); }
        
        /// <summary>
        /// Check the read operation is supported
        /// </summary>
        public override bool CanRead { get => _stream.CanRead; }

        /// <summary>
        /// Check the seek operation is supported
        /// </summary>
        public override bool CanSeek { get => _stream.CanSeek; }

        /// <summary>
        /// Check the write operation is supported
        /// </summary>
        public override bool CanWrite { get => _stream.CanWrite; }

        /// <summary>
        /// Check if the stream is empty
        /// </summary>
        public bool IsEmpty { get => _stream.Length <= 0; }
        






        /// <summary>
        /// Flush the stream - this method do nothing
        /// </summary>
        public override void Flush()
        {
        }

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
        public override void Close()
        {
            _stream.Close();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">the dispose</param>
        protected override void Dispose( bool disposing ) 
        {
            if ( disposing )
            {
                _stream.Dispose();
            }
        }
        
        /// <summary>
        /// Set the length
        /// </summary>
        /// <param name="value">a positive value</param>
        public override void SetLength( long value ) 
        {
            _stream.SetLength( value > 0 ? value : 0 );
        }

        /// <summary>
        /// Sets the position
        /// </summary>
        /// <param name="value">the value</param>
        public void SetPosition( long value )
        { 
            _stream.Position = MathHelper.Clamp( value , 0 , _stream.Length );
        }

        /// <summary>
        /// Seek to a new position
        /// </summary>
        /// <param name="offset">the offset</param>
        /// <param name="origin">the origin</param>
        /// <returns>returns the new position</returns>
        public override long Seek( long offset , SeekOrigin origin )
        {
            return _stream.Seek( offset , origin );
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="offset">the offset</param>
        /// <param name="count">the count</param>
        /// <returns>returns the number of bytes readen</returns>
        public override int Read( byte[] buffer , int offset , int count )
        {
            if ( buffer == null || buffer.Length <= 0 || offset < 0 || count < 0 )
            {
                return -1;
            }

            if ( buffer.Length - offset < count )
            {
                return -1;
            }

            if ( count == 0 )
            {
                return 0;
            }

            return _stream.Read( buffer , offset , count );
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
        public override void Write( byte[] buffer , int offset , int count )
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
        public void Write( MemoryStreamBuffer memoryStream ) 
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
        public override void WriteByte( byte value ) 
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

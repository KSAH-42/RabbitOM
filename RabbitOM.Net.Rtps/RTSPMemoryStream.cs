using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a tolerant memory stream
    /// </summary>
    public sealed class RTSPMemoryStream : IDisposable
    {
        private const int              DefaultSize =  8096 * 8 * 2;

        private MemoryStream           _stream     = null;

        private byte[]                 _buffer     = null;

        private readonly StringBuilder _builder    = null;




        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPMemoryStream()
        {
            _buffer  = new byte[DefaultSize];
            _stream  = new MemoryStream(DefaultSize);
            _builder = new StringBuilder();
        }




        /// <summary>
        /// Check if seek operations are supported
        /// </summary>
        public bool CanSeek
        {
            get
            {
                try
                {
                    return _stream?.CanSeek ?? false;
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }

                return false;
            }
        }

        /// <summary>
        /// Check if read operations are supported
        /// </summary>
        public bool CanRead
        {
            get
            {
                try
                {
                    return _stream?.CanRead ?? false;
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }

                return false;
            }
        }

        /// <summary>
        /// Check if write operations are supported
        /// </summary>
        public bool CanWrite
        {
            get
            {
                try
                {
                    return _stream?.CanWrite ?? false;
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the length
        /// </summary>
        public long Length
        {
            get
            {
                try
                {
                    return _stream?.Length ?? 0;
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets / Sets the position
        /// </summary>
        public long Position
        {
            get
            {
                try
                {
                    return _stream?.Position ?? 0;
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }

                return 0;
            }

            set
            {
                try
                {
                    if ( _stream != null )
                    {
                        _stream.Position = value;
                    }
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }
            }
        }

        /// <summary>
        /// Gets the capacity
        /// </summary>
        public int Capacity
        {
            get
            {
                try
                {
                    return _stream?.Capacity ?? 0;
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }

                return -1;
            }

            set
            {
                try
                {
                    if ( _stream != null )
                    {
                        _stream.Capacity = value;
                    }
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }
            }
        }

        /// <summary>
        /// Gets the capacity
        /// </summary>
        public bool IsEnded
        {
            get
            {
                if ( _stream == null )
                {
                    return true;
                }

                try
                {
                    var length = _stream.Length;

                    if ( length > 0 )
                    {
                        var position = _stream.Position;

                        if ( 0 <= position && position < length )
                        {
                            return false;
                        }
                    }
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }

                return true;
            }
        }






        /// <summary>
        /// Check if the stream has been created
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool IsCreated()
        {
            return _stream != null;
        }

        /// <summary>
        /// Create the stream
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Create()
        {
            if ( _stream != null )
            {
                return false;
            }

            try
            {
                _stream = new MemoryStream();

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return true;
        }

        /// <summary>
        /// Create the stream
        /// </summary>
        /// <param name="capacity">the capacity</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Create( int capacity )
        {
            if ( _stream != null || capacity <= 0 )
            {
                return false;
            }

            try
            {
                _stream = new MemoryStream( capacity );

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return true;
        }

        /// <summary>
        /// Close the stream
        /// </summary>
        public void Close()
        {
            _builder.Clear();

            try
            {
                _stream?.Close();
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }
            finally
            {
                _stream = null;
                _buffer = null;
            }
        }

        /// <summary>
        /// Release internal resources
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// Discard the previous bytes at the current position
        /// </summary>
        public void Discard()
        {
            if ( _stream == null )
            {
                return;
            }

            try
            {
                long position = _stream.Position;


                if ( position == _stream.Length )
                {
                    _stream.SetLength(0);
                    return;
                }
                
                if (0 < position && position < _stream.Length)
                {
                    int bufferSize = (int)(_stream.Length - position);

                    if (_buffer == null || _buffer.Length < bufferSize )
					{
                        _buffer = new byte[bufferSize];
                    }

                    _stream.Read(_buffer, 0, bufferSize);
                    _stream.SetLength(0);
                    _stream.Write(_buffer, 0, bufferSize);
                    _stream.Seek(0, SeekOrigin.Begin);
				}
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }
        }

        /// <summary>
        /// Move to the begining
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool SeekToBegin()
        {
            return Seek( 0 , SeekOrigin.Begin );
        }

        /// <summary>
        /// Move to the End
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool SeekToEnd()
        {
            return Seek( 0 , SeekOrigin.End );
        }

        /// <summary>
        /// Move backward
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool SeekBackward()
        {
            return Seek( -1 , SeekOrigin.Current );
        }

        /// <summary>
        /// Move forward
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool SeekForward()
        {
            return Seek( 1 , SeekOrigin.Current );
        }

        /// <summary>
        /// Move to a position
        /// </summary>
        /// <param name="position">the position</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool SeekRelative( int position )
        {
            return Seek( position , SeekOrigin.Current );
        }

        /// <summary>
        /// Move to the begining
        /// </summary>
        /// <param name="offset">the offset</param>
        /// <param name="origin">the origin</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Seek( long offset , SeekOrigin origin )
        {
            if ( _stream == null )
            {
                return false;
            }

            try
            {
                _stream.Seek( offset , origin );

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Move until the value has been found
        /// </summary>
        /// <param name="value">the value</param>
        public bool Search( char value )
        {
            return Search( (byte) value );
        }

        /// <summary>
        /// Move until the value has been found
        /// </summary>
        /// <param name="value">the value</param>
        public bool Search( byte value )
        {
            if ( _stream == null )
            {
                return false;
            }

            try
            {
                int byteRead;

                while ( true )
                {
                    byteRead = _stream.ReadByte();

                    if ( byteRead < 0 )
                    {
                        return false;
                    }

                    if ( 0 <= byteRead && byteRead == (int) value )
                    {
                        return true;
                    }
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Move until the value has been found
        /// </summary>
        /// <param name="value">the value</param>
        public bool Search( string value )
        {
            return Search( value , false );
        }

        /// <summary>
        /// Move until the value has been found
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="ignoreCase">set true to ignore the case</param>
        public bool Search( string value , bool ignoreCase )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            if ( _stream == null )
            {
                return false;
            }

            try
            {
                int byteRead  = -1;
                int charIndex = -1;

                while ( true )
                {
                    byteRead = _stream.ReadByte();

                    if ( byteRead < 0 )
                    {
                        break;
                    }

                    charIndex++;

                    if ( Compare( (char) byteRead , value[charIndex] , ignoreCase ) )
                    {
                        if ( charIndex == ( value.Length - 1 ) )
                        {
                            return true;
                        }
                    }
                    else
                    {
                        charIndex = -1;
                    }
                }

                if ( byteRead < 0 )
                {
                    if ( charIndex >= 0 )
                    {
                        return true;
                    }
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Move until a some of theses input characters has been found
        /// </summary>
        /// <param name="characters">the characters</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool SearchAny( IEnumerable<char> characters )
        {
            return SearchAny( characters , false );
        }

        /// <summary>
        /// Move until a some of theses input characters has been found
        /// </summary>
        /// <param name="characters">the characters</param>
        /// <param name="ignoreCase">set true ignore case</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool SearchAny( IEnumerable<char> characters , bool ignoreCase )
        {
            if ( _stream == null || characters == null || !characters.Any() )
            {
                return false;
            }

            try
            {
                int byteRead = -1;

                while ( true )
                {
                    byteRead = _stream.ReadByte();

                    if ( byteRead < 0 )
                    {
                        return false;
                    }

                    foreach ( var character in characters )
                    {
                        if ( Compare( character , (char) byteRead , ignoreCase ) )
                        {
                            return true;
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Read values
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <returns>returns the number of bytes read</returns>
        public int Read( byte[] buffer )
        {
            if ( buffer == null )
            {
                return -1;
            }

            return Read( buffer , 0 , buffer.Length );
        }

        /// <summary>
        /// Read values
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="offset">the offset</param>
        /// <param name="count">the number of byte to read</param>
        /// <returns>returns the number of bytes read</returns>
        public int Read( byte[] buffer , int offset , int count )
        {
            if ( _stream == null || buffer == null || buffer.Length <= 0 || count <= 0 || count > buffer.Length )
            {
                return -1;
            }

            try
            {
                return _stream.Read( buffer , offset , count );
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return -1;
        }

        /// <summary>
        /// Read a byte
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public int ReadByte()
        {
            if ( _stream == null )
            {
                return -1;
            }

            try
            {
                return _stream.ReadByte();
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return -1;
        }

        /// <summary>
        /// Read a byte
        /// </summary>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool ReadByte( out byte result )
        {
            result = default;

            if ( _stream == null )
            {
                return false;
            }

            try
            {
                var value = _stream.ReadByte();

                if ( value >= 0 )
                {
                    result = (byte) value;

                    return true;
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Read a char
        /// </summary>
        /// <param name="result">the output</param>
        /// <returns>returns a value</returns>
        public bool ReadChar( out char result )
        {
            result = char.MinValue;

            if ( _stream == null )
            {
                return false;
            }

            try
            {
                var value = _stream.ReadByte();

                if ( value >= 0 )
                {
                    result = (char) value;

                    return true;
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Read a byte
        /// </summary>
        /// <param name="result">the result</param>
        /// <returns>returns a value</returns>
        public bool ReadUInt16( out UInt16 result )
        {
            result = 0;

            if ( _stream == null )
            {
                return false;
            }

            try
            {
                var valueMsb = _stream.ReadByte();

                if ( valueMsb >= 0 )
                {
                    var valueLsb = _stream.ReadByte();

                    if ( valueLsb < 0 )
                    {
                        result = (UInt16) valueMsb;

                        return true;
                    }

                    result = (UInt16) ( valueMsb * 0x100 + valueLsb );

                    return true;
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Read a string
        /// </summary>
        /// <param name="count">the number of caracter</param>
        /// <param name="result">the result</param>
        /// <returns>returns a value</returns>
        public bool ReadString( int count , out string result )
        {
            result = string.Empty;

            if ( _stream == null || count <= 0 )
            {
                return false;
            }

            _builder.Clear();

            try
            {
                var buffer = new byte[ 1 ];

                while (_builder.Length < count )
                {
                    var byteReads = _stream.Read( buffer , 0 , buffer.Length );

                    if ( byteReads <= 0 )
                    {
                        break;
                    }

                    _builder.Append( Encoding.UTF8.GetString( buffer , 0 , byteReads ) );
                }

                if (_builder.Length > 0 )
                {
                    result = _builder.ToString();

                    return true;
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Read a line
        /// </summary>
        /// <param name="result">the result</param>
        /// <returns>returns a value</returns>
        public bool ReadLine( out string result )
        {
            return ReadLine( int.MaxValue , out result );
        }

        /// <summary>
        /// Read a line
        /// </summary>
        /// <param name="limit">the limit</param>
        /// <param name="result">the result</param>
        /// <returns>returns a value</returns>
        public bool ReadLine( int limit , out string result )
        {
            result = string.Empty;

            if ( _stream == null || limit <= 0 )
            {
                return false;
            }

            _builder.Clear();

            try
            {
                int currentValue   = -1;
                int previsousValue = -1;

                while ( true )
                {
                    previsousValue = currentValue;

                    currentValue = _stream.ReadByte();

                    if ( currentValue < 0 )
                    {
                        return false;
                    }

                    if ( currentValue != '\r' && currentValue != '\n' )
                    {
                        _builder.Append( (char) currentValue );
                    }

                    if (_builder.Length >= limit )
                    {
                        break;
                    }

                    if ( previsousValue == '\r' && currentValue == '\n' )
                    {
                        break;
                    }
                }

                result = _builder.ToString();

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="buffer">the value</param>
        /// <returns>returns true for a succes, otherwise false</returns>
        public bool Write( byte[] buffer )
        {
            if ( buffer == null )
            {
                return false;
            }

            return Write( buffer , 0 , buffer.Length );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="buffer">the value</param>
        /// <param name="offset">the offset</param>
        /// <param name="count">the count</param>
        /// <returns>returns true for a succes, otherwise false</returns>
        public bool Write( byte[] buffer , int offset , int count )
        {
            if ( _stream == null || buffer == null || buffer.Length <= 0 || buffer.Length < count || count <= 0 )
            {
                return false;
            }

            try
            {
                _stream.Write( buffer , offset , count );

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a succes, otherwise false</returns>
        public bool WriteChar( char value )
        {
            if ( _stream == null )
            {
                return false;
            }

            try
            {
                _stream.WriteByte( (byte) value );

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a succes, otherwise false</returns>
        public bool WriteByte( byte value )
        {
            if ( _stream == null )
            {
                return false;
            }

            try
            {
                _stream.WriteByte( value );

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a succes, otherwise false</returns>
        public bool WriteUInt16( UInt16 value )
        {
            if ( _stream == null )
            {
                return false;
            }

            try
            {
                _stream.WriteByte( (byte) ( value >> 8 & 0xFF ) );
                _stream.WriteByte( (byte) ( value & 0xFF ) );

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <returns>returns true for a succes, otherwise false</returns>
        public bool WriteLine()
        {
            return WriteString( "\r\n" );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a succes, otherwise false</returns>
        public bool WriteLine( string value )
        {
            return WriteString( value + "\r\n" );
        }
        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a succes, otherwise false</returns>
        public bool WriteString( string value )
        {
            if ( _stream == null || string.IsNullOrEmpty( value ) )
            {
                return false;
            }

            try
            {
                var buffer = Encoding.UTF8.GetBytes( value );

                if ( buffer == null || buffer.Length <= 0 )
                {
                    return false;
                }

                _stream.Write( buffer , 0 , buffer.Length );

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Flush
        /// </summary>
        public void Flush()
        {
            try
            {
                _stream?.Flush();
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }
        }




        /// <summary>
        /// Compare value
        /// </summary>
        /// <param name="a">a value</param>
        /// <param name="b">a value</param>
        /// <param name="ignoreCase">set true to ignore case</param>
        /// <returns>returns true for a success, otherwise false</returns>
        private static bool Compare( char a , char b , bool ignoreCase )
        {
            return ignoreCase ? char.ToUpperInvariant( a ) == char.ToUpperInvariant( b )
                              : a == b;
        }




        /// <summary>
        /// Occurs when an exception has been dected
        /// </summary>
        /// <param name="exception">the exception</param>
        private void OnError( Exception exception )
        {
            if ( exception == null )
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine( exception );
        }
    }
}

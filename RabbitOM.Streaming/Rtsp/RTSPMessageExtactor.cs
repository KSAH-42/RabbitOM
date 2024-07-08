using System;
using System.Text;

// TODO: this class must be refactor when to handle very large streams

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the message stream extractor
    /// </summary>
    internal sealed class RTSPMessageExtactor : IDisposable
    {
        private readonly object           _lock              = null;

        private readonly long             _limit             = 0;

        private readonly RTSPMemoryStream _stream            = null;

        private readonly StringBuilder    _valueString       = null;

        private RTSPPacket                _interleavedPacket = null;

        private RTSPMessageResponse       _response          = null;

        private int                       _valueByte         = -1; // The name contains byte keywork but this is int value and not a byte value type. Please refer to microsoft stream implementation and definition of the method called ReadByte which returns a int and a byte. For only this case, strongly prefer this instead of using nullable value




        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPMessageExtactor()
            : this( short.MaxValue )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="limit">the limit</param>
        public RTSPMessageExtactor( long limit )
        {
            if ( limit <= 0 )
            {
                throw new ArgumentException( nameof( limit ) );
            }

            _lock = new object();
            _limit = limit;
            _stream = new RTSPMemoryStream();
            _valueString = new StringBuilder();
        }





        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Check if the internal memory stream has reach the limi
        /// </summary>
        public bool HasReachSizeLimit
        {
            get
            {
                lock ( _lock )
                {
                    return _limit <= _stream.Length;
                }
            }
        }

        /// <summary>
        /// Check if the current value is well known protocol char
        /// </summary>
        public bool HasValueProtocolChar
        {
            get => CompareValue( '$' ) || CompareValueAsProtocolChar();
        }

        /// <summary>
        /// Check if the current value is a magic char
        /// </summary>
        public bool IsInterleavedSequence
        {
            get => CompareValue( '$' );
        }

        /// <summary>
        /// Check if we have detected a message 
        /// </summary>
        public bool IsMessageSequence
        {
            get => CompareValueString( "RTSP" );
        }




        /// <summary>
        /// Compare the value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CompareValue( char value )
        {
            return CompareValue( (byte) value );
        }

        /// <summary>
        /// Compare the value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CompareValue( byte value )
        {
            lock ( _lock )
            {
                return _valueByte >= 0 && _valueByte == value;
            }
        }

        /// <summary>
        /// Check if the value read is protocol char
        /// </summary>
        /// <returns>returns true for a success,</returns>
        public bool CompareValueAsProtocolChar()
        {
            lock ( _lock )
            {
                return char.IsLetterOrDigit((char)_valueByte);
            }
        }

        /// <summary>
        /// Compare the value read
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CompareValueString( string value )
        {
            return CompareValueString( value , true );
        }

        /// <summary>
        /// Compare
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="ignoreCase">the ignore case</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CompareValueString( string value , bool ignoreCase )
        {
            lock ( _lock )
            {
                return string.Compare(_valueString.ToString(), value, ignoreCase) == 0;
            }
        }

        /// <summary>
        /// Clear values
        /// </summary>
        public void ClearValues()
        {
            lock ( _lock )
            {
                _valueByte   = -1;
                _valueString.Clear();
            }
        }

        /// <summary>
        /// Check if the decoder has been initialized
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool IsInitialized()
        {
            lock ( _lock )
            {
                return _stream.IsCreated();
            }
        }

        /// <summary>
        /// Initialize internal resources
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Initialize()
        {
            lock ( _lock )
            {
                return _stream.Create();
            }
        }


        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            UnInitialize( true );
        }

        /// <summary>
        /// Un initialize
        /// </summary>
        public void UnInitialize()
        {
            UnInitialize( false );
        }

        /// <summary>
        /// Un initialize
        /// </summary>
        /// <param name="dispose">dispose</param>
        private void UnInitialize( bool dispose )
        {
            lock ( _lock )
            {
                _stream.Close();
                
                if ( dispose )
                {
                    _stream.Dispose();
                }

                _valueString.Clear();
                _interleavedPacket = null;
                _response = null;
            }
        }

        /// <summary>
        /// Discard
        /// </summary>
        public void Discard()
        {
            lock ( _lock )
            {
                _stream.Discard();
            }
        }

        /// <summary>
        /// Prepare write operation
        /// </summary>
        public void PrepareWrite()
        {
            lock ( _lock )
            {
                _stream.SeekToEnd();
            }
        }

        /// <summary>
        /// Write
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="offset">the offset</param>
        /// <param name="count">the count</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Write( byte[] buffer , int offset , int count )
        {
            lock ( _lock )
            {
                return _stream.Write(buffer, offset, count);
            }
        }

        /// <summary>
        /// Prepare
        /// </summary>
        public void PrepareRead()
        {
            lock ( _lock )
            {
                ClearValues();

                _stream.SeekToBegin();
            }
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <returns>returns the value</returns>
        public bool Read()
        {
            lock ( _lock )
            {
                _valueByte = _stream.ReadByte();

                if (_valueByte > 0)
                {
                    _valueString.Append( (char)_valueByte );
                }

                return _valueByte >= 0;
            }
        }

        /// <summary>
        /// Extract the interleaved packet
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryExtractInterleaved()
        {
            lock ( _lock )
            {
                _interleavedPacket = null;

                if (!_stream.ReadByte(out byte channel))
                {
                    return false;
                }

                if (!_stream.ReadUInt16(out ushort length))
                {
                    return false;
                }

                if (_stream.Length < length )
                {
                    _stream.SeekBackward();
                    return false;
                }
                
                var buffer = new byte[length];

                int bytesRead = _stream.Read(buffer, 0, buffer.Length);

                if (bytesRead != buffer.Length)
                {
                    return false;
                }

                _interleavedPacket = new RTSPInterleavedPacket(channel, buffer);

                return true; 
            }
        }

        /// <summary>
        /// Extract a response
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryExtractResponse()
        {
            lock (_lock)
            {
                _response = null;

                if (!_stream.SeekRelative("RTSP".Length * -1))
                {
                    return false;
                }

                var builder = new StringBuilder();

                long   length = 0;
                string body   = string.Empty;

                while (_stream.ReadLine(out string line))
                {
                    builder.AppendLine(line);

                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    
                    if (RTSPHeaderFactory.CanCreateHeader(line, RTSPHeaderNames.ContentLength))
                    {
                        var header = RTSPHeaderFactory.CreateHeader(line) as RTSPHeaderContentLength;

                        if (header != null)
                        {
                            length = header.Value;
                        }
                    }
                }

                string messageHead = builder.ToString();

                if (string.IsNullOrWhiteSpace(messageHead) || !messageHead.Contains("\r\n\r\n"))
                {
                    return false;
                }

                if (length > 0 && !_stream.ReadString((int)length, out body))
                {
                    return false;
                }

                if (length != body.Length)
                {
                    return false;
                }

                builder.Append(body);

                _response = RTSPMessageSerializer.Deserialize(builder.ToString());

                return _response != null; 
            }
        }

        /// <summary>
        /// Gets the interleaved packet
        /// </summary>
        /// <returns>returns an instance, otherwise null</returns>
        public RTSPPacket GetInterleavedPacket()
        {
            lock (_lock)
            {
                return _interleavedPacket; 
            }
        }

        /// <summary>
        /// Gets the response
        /// </summary>
        /// <returns>returns an instance, otherwise null</returns>
        public RTSPMessageResponse GetResponse()
        {
            lock ( _lock )
            {
                return _response; 
            }
        }
    }
}

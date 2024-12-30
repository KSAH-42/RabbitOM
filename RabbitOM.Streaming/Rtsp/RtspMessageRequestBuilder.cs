﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message request builder
    /// </summary>
    public class RtspMessageRequestBuilder
    {
        private readonly object                  _lock            = new object();

        private readonly RtspMethod              _method          = RtspMethod.UnDefined;

        private readonly string                  _uri             = string.Empty;

        private string                           _controlUri      = string.Empty;

        private string                           _sessionId       = string.Empty;

        private int                              _sequenceId      = 0;

        private RtspDeliveryMode?                _deliveryMode    = null;

        private int                              _unicastPort     = 0;

        private string                           _multicastAddress= string.Empty;

        private int                              _multicastPort   = 0;

        private byte                             _ttl             = 0;

        private string                           _acceptHeader    = string.Empty;

        private string                           _contentType     = string.Empty;

        private readonly StringBuilder           _body            = new StringBuilder();

        private readonly RtspHeaderCollection    _headers         = new RtspHeaderCollection();




        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="method">the method</param>
        /// <param name="uri">the uri</param>
        /// <exception cref="ArgumentException"/>
        public RtspMessageRequestBuilder( RtspMethod method , string uri )
        {
            if ( method == RtspMethod.UnDefined )
            {
                throw new ArgumentException( nameof( method ) );
            }

            _method = method;
            _uri = uri ?? string.Empty;
        }





        /// <summary>
        /// Gets the method
        /// </summary>
        public RtspMethod Method
        {
            get
            {
                lock ( _lock )
                {
                    return _method;
                }
            }
        }

        /// <summary>
        /// Gets the uri
        /// </summary>
        public string Uri
        {
            get
            {
                lock ( _lock )
                {
                    return _uri;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the control uri
        /// </summary>
        public string ControlUri
        {
            get
            {
                lock ( _lock )
                {
                    return _controlUri;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _controlUri = value ?? string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the session identifier
        /// </summary>
        public string SessionId
        {
            get
            {
                lock ( _lock )
                {
                    return _sessionId;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _sessionId = value ?? string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the sequence identifier
        /// </summary>
        public int SequenceId
        {
            get
            {
                lock ( _lock )
                {
                    return _sequenceId;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _sequenceId = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the delivery type
        /// </summary>
        public RtspDeliveryMode? DeliveryMode
        {
            get
            {
                lock ( _lock )
                {
                    return _deliveryMode;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _deliveryMode = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the unicast port
        /// </summary>
        public int UnicastPort
        {
            get
            {
                lock ( _lock )
                {
                    return _unicastPort;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _unicastPort = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the multicast address
        /// </summary>
        public string MulticastAddress
        {
            get
            {
                lock ( _lock )
                {
                    return _multicastAddress;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _multicastAddress = value ?? string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the multicast port
        /// </summary>
        public int MulticastPort
        {
            get
            {
                lock ( _lock )
                {
                    return _multicastPort;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _multicastPort = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the ttl
        /// </summary>
        public byte TTL
        {
            get
            {
                lock ( _lock )
                {
                    return _ttl;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _ttl = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the accept
        /// </summary>
        public string AcceptHeader
        {
            get
            {
                lock (_lock)
                {
                    return _acceptHeader;
                }
            }

            set
            {
                lock (_lock)
                {
                    _acceptHeader = value ?? string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the content type
        /// </summary>
        public string ContentType
        {
            get
            {
                lock ( _lock )
                {
                    return _contentType;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _contentType = value ?? string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the body length
        /// </summary>
        public int BodyLength
        {
            get
            {
                lock ( _lock )
                {
                    return _body.Length;
                }
            }
        }

        /// <summary>
        /// Check if the output length is empty
        /// </summary>
        public bool IsBodyAppended
        {
            get
            {
                lock ( _lock )
                {
                    return _body.Length > 0;
                }
            }
        }






        /// <summary>
        /// Add a header
        /// </summary>
        /// <param name="header">the header</param>
        public bool CanAddHeader( RtspHeader header )
        {
            return header == null
                || header is RtspHeaderNull
                || header is RtspHeaderCSeq
                || header is RtspHeaderContentLength
                || header is RtspHeaderWWWAuthenticate
                || header is RtspHeaderAuthorization
                ? false
                : true;
        }

        /// <summary>
        /// Add a header
        /// </summary>
        /// <param name="header">the header</param>
        public void AddHeader( RtspHeader header )
        {
            _headers.TryAddOrUpdate( header );
        }

        /// <summary>
        /// Add a header
        /// </summary>
        /// <param name="name">the header name</param>
        /// <param name="value">the header value</param>
        public bool CanAddHeader( string name , string value )
        {
            return !string.IsNullOrWhiteSpace( name ) && !string.IsNullOrWhiteSpace( value );
        }

        /// <summary>
        /// Add a header
        /// </summary>
        /// <param name="name">the header name</param>
        /// <param name="value">the header value</param>
        public void AddHeader( string name , string value )
        {
            _headers.TryAddOrUpdate( new RtspHeaderCustom( name , value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( bool value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( char value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( sbyte value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToHexString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( byte value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToHexString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( short value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( ushort value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( int value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( uint value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( long value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( ulong value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( decimal value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( float value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( double value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( DateTime value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="format">the format</param>
        public void WriteBody( DateTime value , string format )
        {
            BaseWriteBody( RtspDataConverter.ConvertToString( value , format ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( TimeSpan value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( Guid value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToString( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBody( string value )
        {
            BaseWriteBody( value );
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="format">the format</param>
        /// <param name="parameters">the parameters</param>
        /// <returns>returns the current instance</returns>
        public void WriteBody( string format , params object[] parameters )
        {
            if ( string.IsNullOrWhiteSpace( format ) || parameters == null || parameters.Length <= 0 )
            {
                return;
            }

            BaseWriteBody( string.Format( format , parameters ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyAsBase64( string value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToBase64( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyAsBase64( byte[] value )
        {
            BaseWriteBody( RtspDataConverter.ConvertToBase64( value ) );
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        private void BaseWriteBody( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return;
            }

            lock ( _lock )
            {
                _body.Append( value );
            }
        }





        /// <summary>
        /// Write an new line
        /// </summary>
        public void WriteBodyLine()
        {
            InternalWriteBodyLine();
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(bool value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(char value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(sbyte value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToHexString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(byte value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToHexString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(short value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(ushort value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(int value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(uint value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(long value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(ulong value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(decimal value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(float value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(double value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(DateTime value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="format">the format</param>
        public void WriteBodyLine(DateTime value, string format)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToString(value, format));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(TimeSpan value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(Guid value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToString(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLine(string value)
        {
            InternalWriteBodyLine(value);
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="format">the format</param>
        /// <param name="parameters">the parameters</param>
        /// <returns>returns the current instance</returns>
        public void WriteBodyLine(string format, params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(format) || parameters == null || parameters.Length <= 0)
            {
                return;
            }

            InternalWriteBodyLine(string.Format(format, parameters));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLineAsBase64(string value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToBase64(value));
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        public void WriteBodyLineAsBase64(byte[] value)
        {
            InternalWriteBodyLine(RtspDataConverter.ConvertToBase64(value));
        }




















        /// <summary>
        /// Write an new line
        /// </summary>
        private void InternalWriteBodyLine()
        {
            lock (_lock)
            {
                _body.AppendLine();
            }
        }

        /// <summary>
        /// Write a value
        /// </summary>
        /// <param name="value">the value</param>
        private void InternalWriteBodyLine(string value)
        {
            lock (_lock)
            {
                if ( ! string.IsNullOrEmpty( value ) )
                {
                    _body.Append(value);
                }

                _body.AppendLine(value);
            }
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="name">the parameter name</param>
        public void AddBodyParameter( string name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                return;
            }

            lock ( _lock )
            {
                if ( _body.Length > 0 )
                {
                    _body.AppendLine();
                }

                _body.Append( name.Trim() ?? string.Empty );
            }
        }

        /// <summary>
        /// Write a parameter
        /// </summary>
        /// <param name="name">the parameter name</param>
        /// <param name="value">the value</param>
        public void AddBodyParameter( string name , string value )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                return;
            }

            lock ( _lock )
            {
                if ( _body.Length > 0 )
                {
                    _body.AppendLine();
                }

                _body.Append( $"{name.Trim() ?? string.Empty}: {value?.Trim() ?? string.Empty}" );
            }
        }

        /// <summary>
        /// Write parameters
        /// </summary>
        /// <param name="names">the paramater names</param>
        public void AddBodyParameters( IEnumerable<string> names )
        {
            if ( names == null )
            {
                return;
            }

            lock ( _lock )
            {
                foreach ( var name in names )
                {
                    if ( string.IsNullOrWhiteSpace( name ) )
                    {
                        continue;
                    }

                    if ( _body.Length > 0 )
                    {
                        _body.AppendLine();
                    }

                    _body.Append( name?.Trim() ?? string.Empty );
                }
            }
        }

        /// <summary>
        /// Write parameters
        /// </summary>
        /// <param name="parameters">the paramater list</param>
        public void WriteBodyParameters( IDictionary<string , string> parameters )
        {
            if ( parameters == null || parameters.Count <= 0 )
            {
                return;
            }

            lock ( _lock )
            {
                foreach ( var element in parameters )
                {
                    if ( string.IsNullOrWhiteSpace( element.Key ) )
                    {
                        continue;
                    }

                    if ( _body.Length > 0 )
                    {
                        _body.AppendLine();
                    }

                    _body.Append( $"{element.Key.Trim() ?? string.Empty}: {element.Value?.Trim() ?? string.Empty}" );
                }
            }
        }







        /// <summary>
        /// Build a request
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public RtspMessageRequest BuildRequest()
        {
            lock ( _lock )
            {
                if ( ! RtspUri.TryParse( _uri , out RtspUri uri ) )
                {
                    uri = new RtspUri();
                }
                else
                {
                    uri.RemoveCredentials();
                }

                RtspMessageRequest request = null;

                if ( string.IsNullOrWhiteSpace( _controlUri ) )
                {
                    request = new RtspMessageRequest( _method , uri.ToString() );
                }
                else
                {
                    request = new RtspMessageRequest( _method , RtspUri.CombineWith( uri , _controlUri ) );
                }

                OnBuildRequest( request );

                return request;
            }
        }






        /// <summary>
        /// Occurs when the request must be builded
        /// </summary>
        /// <param name="request">the request</param>
        protected virtual void OnBuildRequest( RtspMessageRequest request )
        {
            request.Headers.TryAddOrUpdate( new RtspHeaderCSeq( _sequenceId ) );

            if ( ! string.IsNullOrWhiteSpace( _sessionId ) )
            {
                request.Headers.TryAddOrUpdate( new RtspHeaderSession( _sessionId ) );
            }

            if ( _deliveryMode.HasValue )
            {
                if ( _deliveryMode == RtspDeliveryMode.Tcp )
                {
                    request.Headers.TryAddOrUpdate( RtspHeaderTransport.NewInterleavedTransportHeader() );
                }

                if ( _deliveryMode == RtspDeliveryMode.Udp )
                {
                    request.Headers.TryAddOrUpdate( RtspHeaderTransport.NewUnicastUdpTransportHeader( _unicastPort ) );
                }

                if ( _deliveryMode == RtspDeliveryMode.Multicast )
                {
                    request.Headers.TryAddOrUpdate( RtspHeaderTransport.NewMulticastUdpTransportHeader( _multicastAddress , _multicastPort , _ttl ) );
                }
            }

            if ( ! _headers.IsEmpty )
            {
                request.Headers.TryAddRange( _headers );
            }

            if ( _body.Length > 0 )
            {
                request.Headers.TryAddOrUpdate( new RtspHeaderContentLength( _body.Length ) );

                request.Body.Value = _body.ToString();
            }

            if ( ! string.IsNullOrWhiteSpace( _acceptHeader ) )
            {
                request.Headers.TryAddOrUpdate( RtspHeaderAccept.NewAcceptHeader( _acceptHeader ) );
            }

            if ( ! string.IsNullOrWhiteSpace( _contentType ) )
            {
                request.Headers.TryAddOrUpdate( new RtspHeaderContentType( _contentType ) );
            }
        }
    }
}

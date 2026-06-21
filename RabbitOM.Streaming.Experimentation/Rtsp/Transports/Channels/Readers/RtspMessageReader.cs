using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspMessageReader : IMessageReader
    {
        private readonly RtspMessageReaderGuardSettings _settings;

        private readonly RtspStreamReader _reader;





        public RtspMessageReader( IStream stream , RtspMessageReaderGuardSettings settings )
        {
            _settings = settings ?? throw new ArgumentNullException( nameof( settings ) );

            _reader = new RtspStreamReader( stream );
        }





        public byte? Peek()
        {
            var prefix = _reader.Peek();

            if ( prefix < 0 )
            {
                return null;
            }

            return (byte) prefix;
        }

        public RtspInterleavedMessage ReadInterleavedMessage()
        {
            var magicByte = _reader.ReadByte();

            if ( magicByte != '$' )
            {
                return null;
            }

            var channel = _reader.ReadByte();

            if ( channel < 0 )
            {
                return null;
            }

            var lengthMsb = _reader.ReadByte();

            if ( lengthMsb < 0 )
            {
                return null;
            }

            var lengthLsb = _reader.ReadByte();

            if ( lengthLsb < 0 )
            {
                return null;
            }

            var length = (ushort) ( (lengthMsb << 8 + lengthLsb) & 0xFFFF );
            var buffer = new byte[ length ];
            var offset = 0;

            while ( offset < buffer.Length )
            {
                var bytesRead = _reader.Read( buffer , offset , buffer.Length - offset );

                if ( bytesRead <= 0 )
                {
                    return null;
                }

                offset += bytesRead;
            }

            return new RtspInterleavedMessage() { Channel = (byte) ( channel & 0xFF ) , Length = length , Buffer = buffer };
        }

        public RtspMessage ReadControlMessage()
        {
            var startLine = _reader.ReadLine();

            if ( startLine == null )
            {
                return null;
            }

            var headers = new RtspMessageHeaderCollection();

            // a guard should be used here and not on the service class
            // it must be used here during receiving data and never after returning the message it can grow in terms of memory size

            var guard = new RtspMessageReaderGuard( _settings , headers );

            while ( true )
            {
                var header = _reader.ReadLine();

                if ( header == null )
                {
                    return null;
                }

                if ( header == "" )
                {
                    break;
                }

                headers.TryAddParse( header );
                
                guard.CheckForProtocolViolations( header );
            }

            // Before to continue, we must check the existance of mandatory header and it's content
            guard.EnsureCSeqHeader();

            byte[] body = null;

            if ( headers.ContentLength.HasValue && headers.ContentLength > 0 )
            {
                body = new byte[ headers.ContentLength.Value ];

                var offset = 0;

                while ( offset < body.Length )
                {
                    var bytesRead = _reader.Read( body , offset , body.Length - offset );

                    if ( bytesRead <= 0 )
                    {
                        return null;
                    }

                    offset += bytesRead;
                }
            }

            if ( RtspStatusLine.TryParse( startLine , out var statusLine ) )
            {
                return new RtspResponseMessage() { StatusLine = statusLine , Headers = headers , Body = body };
            }

            if ( RtspRequestLine.TryParse( startLine , out var requestLine ) )
            {
                return new RtspRequestMessage() { RequestLine = requestLine , Headers = headers , Body = body };
            }

            return null;
        }
    }
}



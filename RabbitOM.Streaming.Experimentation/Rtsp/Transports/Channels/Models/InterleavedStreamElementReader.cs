using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Models
{
    public sealed class InterleavedStreamElementReader : IStreamElementReader
    {
        private readonly Stream _stream;

        public InterleavedStreamElementReader( Stream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );;
        }

        public IStreamElement ReadElement()
        {
            var channel = _stream.ReadByte();

            if ( channel < 0 )
            {
                return null;
            }

            var sizeMsb = _stream.ReadByte();

            if ( sizeMsb < 0 )
            {
                return null;
            }

            var sizeLsb = _stream.ReadByte();

            if ( sizeLsb < 0 )
            {
                return null;
            }

            var buffer = new byte[ sizeMsb << 8  + sizeLsb ];
            var offset = 0;
                
            while ( offset < buffer.Length )
            {
                var bytesRead = _stream.Read( buffer , offset , buffer.Length - offset );

                if ( bytesRead <= 0 )
                {
                    return null;
                }

                offset += bytesRead;
            }

            return new InterleavedStreamElement() {  ChannelNumber = channel , Length = buffer.Length , Buffer = buffer };
        }
    }
}

using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    // The following implementation is subject to change or to be removed entirely

    public sealed class H265StreamWriter : IDisposable
    {
        private static readonly byte[] StartPrefix = { 0x00 , 0x00 , 0x00 , 0x01 };

        private readonly RtpMemoryStream _stream = new RtpMemoryStream();

        private byte[] _vps;

        private byte[] _sps;

        private byte[] _pps;





        public long Length
        {
            get => _stream.Length;
        }

        public byte[] VPS
        {
            get => _vps;
        }

        public byte[] SPS
        {
            get => _sps;
        }

        public byte[] PPS
        {
            get => _pps;
        }






        public void Configure( byte[] vps , byte[] sps , byte[] pps )
        {
            if ( _vps == null )
            {
                _vps = vps;
            }

            if ( _sps == null )
            {
                _sps = sps;
            }

            if ( _pps == null )
            {
                _pps = pps;
            }
        }

        public void Write( ArraySegment<byte> buffer )
        {
            if ( buffer.Count == 0 )
            {
                throw new ArgumentException( nameof( buffer ) );
            }

            _stream.WriteAsBinary( StartPrefix );
            _stream.WriteAsBinary( buffer );
        }

        public void WriteVPS( ArraySegment<byte> buffer )
        {
            if ( buffer.Count > 0 )
            {
                _vps = buffer.ToArray();

                _stream.WriteAsBinary( StartPrefix );
                _stream.WriteAsBinary( buffer );
            }
        }

        public void WriteSPS( ArraySegment<byte> buffer )
        {
            if ( buffer.Count > 0 )
            {
                _sps = buffer.ToArray();

                _stream.WriteAsBinary( StartPrefix );
                _stream.WriteAsBinary( buffer );
            }
        }

        public void WritePPS( ArraySegment<byte> buffer )
        {
            if ( buffer.Count > 0 )
            {
                _pps = buffer.ToArray();

                _stream.WriteAsBinary( StartPrefix );
                _stream.WriteAsBinary( buffer );
            }
        }

        public byte[] ToArray()
        {
            return _stream.ToArray();
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        public void Clear()
        {
            _stream.Clear();

            _vps = null;
            _sps = null;
            _pps = null;
        }
    }
}
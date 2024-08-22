using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    // The following implementation is subject to change or to be removed entirely

    public sealed class H265StreamBuilder : IDisposable
    {
        private static readonly byte[] StartCodePrefix = { 0x00 , 0x00 , 0x00 , 0x01 };

        private readonly RtpMemoryStream _stream = new RtpMemoryStream();

        private byte[] _vps;

        private byte[] _sps;

        private byte[] _pps;

        private bool _hasErrors;








        public long Length
        {
            get => _stream.Length;
        }

        public bool HasErrors
        {
            get => _hasErrors;
        }
        
        public byte[] VPS
        {
            get => _vps;
            set => _vps = value;
        }

        public byte[] SPS
        {
            get => _sps;
            set => _sps = value;
        }

        public byte[] PPS
        {
            get => _pps;
            set => _pps = value;
        }

        





        public bool CanBuild()
        {
            return _stream.Length > 0 && _hasErrors == false;
        }

        public void Write( ArraySegment<byte> buffer )
        {
            if ( buffer.Count == 0 )
            {
                throw new ArgumentException( nameof( buffer ) );
            }

            _stream.WriteAsBinary( StartCodePrefix );
            _stream.WriteAsBinary( buffer );
        }

        public byte[] Build()
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

            _hasErrors = false;

            _vps = null;
            _sps = null;
            _pps = null;
        }

        public void SetErrorStatus( bool status )
        {
            _hasErrors = true;
        }
    }
}
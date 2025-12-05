using System;

namespace RabbitOM.Streaming.Net.Rtp.Framing.H265
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

        public bool CanBuild()
        {
            return _stream.Length > 0 && _hasErrors == false;
        }

        public void Write( ArraySegment<byte> buffer )
        {
            if ( buffer.Count <= 0 )
            {
                throw new ArgumentException( nameof( buffer ) );
            }

            _stream.WriteAsBinary( StartCodePrefix );
            _stream.WriteAsBinary( buffer );
        }

        public void WriteAsUInt16( int value )
        {
            _stream.WriteAsUInt16( value );
        }

        public byte[] Build()
        {
            return _stream.ToArray();
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        public void Clear( bool clearParameterSet = true )
        {
            _stream.Clear();

            _hasErrors = false;

            if ( clearParameterSet )
            {
                _vps = null;
                _sps = null;
                _pps = null;
            }
        }

        public void SetErrorStatus( bool status )
        {
            _hasErrors = true;
        }
    }
}
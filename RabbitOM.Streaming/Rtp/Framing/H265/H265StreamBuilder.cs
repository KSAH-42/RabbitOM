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

        public bool HasErrors 
        {
            get => _hasErrors;
        }





        public bool CanBuild()
        {
            return _stream.Length > 0 && _hasErrors == false;
        }

        public void Setup( byte[] vps , byte[] sps , byte[] pps )
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

        public void WriteNal( ArraySegment<byte> buffer )
        {
            if ( buffer.Count == 0 )
            {
                throw new ArgumentException( nameof( buffer ) );
            }

            _stream.WriteAsBinary( StartCodePrefix );
            _stream.WriteAsBinary( buffer );
        }

        public void WriteNalAsVPS( ArraySegment<byte> buffer )
        {
            if ( buffer.Count > 0 )
            {
                _vps = buffer.ToArray();

                _stream.WriteAsBinary( StartCodePrefix );
                _stream.WriteAsBinary( buffer );
            }
        }

        public void WriteNalAsSPS( ArraySegment<byte> buffer )
        {
            if ( buffer.Count > 0 )
            {
                _sps = buffer.ToArray();

                _stream.WriteAsBinary( StartCodePrefix );
                _stream.WriteAsBinary( buffer );
            }
        }

        public void WriteNalAsPPS( ArraySegment<byte> buffer )
        {
            if ( buffer.Count > 0 )
            {
                _pps = buffer.ToArray();

                _stream.WriteAsBinary( StartCodePrefix );
                _stream.WriteAsBinary( buffer );
            }
        }

        public void WriteNalAsFuStart( ArraySegment<byte> buffer )
        {
            throw new NotImplementedException();
        }

        public void WriteNalAsFu( ArraySegment<byte> buffer )
        {
            throw new NotImplementedException();
        }
        public void WriteNalAsFuStop( ArraySegment<byte> buffer )
        {
            throw new NotImplementedException();
        }

        public byte[] Build()
        {
            return _stream.ToArray();
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        public void Clear( bool clearParameterSets = true )
        {
            _hasErrors = false;
            _stream.Clear();

            if ( clearParameterSets )
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
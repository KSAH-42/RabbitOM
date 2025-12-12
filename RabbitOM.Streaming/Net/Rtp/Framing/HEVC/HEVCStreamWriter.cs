using System;

namespace RabbitOM.Streaming.Net.Rtp.Framing.HEVC
{
    public sealed class HEVCStreamWriter : IDisposable
    {
        private static readonly byte[] StartCodePrefix = { 0x00 , 0x00 , 0x00 , 0x01 };



        private readonly RtpMemoryStream _streamOfPackets = new RtpMemoryStream();
        private readonly RtpMemoryStream _streamOfFragmentedPackets = new RtpMemoryStream();
       


        private byte[] _pps;
        private byte[] _sps;
        private byte[] _vps;


        
        
        public byte[] PPS
        {
            get => _pps;
            set => _pps = value;
        }

        public byte[] SPS
        {
            get => _sps;
            set => _sps = value;
        }

        public byte[] VPS
        {
            get => _vps;
            set => _vps = value;
        }

        public long Length
        {
            get => _streamOfPackets.Length;
        }

        public bool IsEmpty
        {
            get => _streamOfPackets.IsEmpty;
        }







        




        



        public void SetLength( int value )
        {
            _streamOfPackets.SetLength( value );
        }

        public void Clear()
        {
            _pps = null;
            _sps = null;
            _vps = null;

            _streamOfPackets.Clear();
        }

        public void Dispose()
        {
            Clear();
            
            _streamOfPackets.Dispose();
            _streamOfFragmentedPackets.Dispose();
        }

        public byte[] ToArray()
        {
            var output = new RtpMemoryStream();

            if ( _vps?.Length > 0 )
            {
                output.WriteAsBinary( StartCodePrefix );
                output.WriteAsBinary( _vps );
            }

            if ( _sps?.Length > 0 )
            {
                output.WriteAsBinary( StartCodePrefix );
                output.WriteAsBinary( _sps );
            }

            if ( _pps?.Length > 0 )
            {
                output.WriteAsBinary( StartCodePrefix );
                output.WriteAsBinary( _pps );
            }

            output.WriteAsBinary( _streamOfPackets );
            
            return output.ToArray();
        }

        public bool HasParametersSets()
        {
            return _pps?.Length > 0 && _sps?.Length > 0 && _vps?.Length > 0;
        }

        public void Write( RtpPacket packet )
        {
            throw new NotImplementedException();
        }

        public void WritePPS( RtpPacket packet )
        {
            throw new NotImplementedException();
        }

        public void WriteSPS( RtpPacket packet )
        {
            throw new NotImplementedException();
        }

        public void WriteVPS( RtpPacket packet )
        {
            throw new NotImplementedException();
        }

        public void WriteAggregation( RtpPacket packet )
        {
            throw new NotImplementedException();
        }

        public void WriteStartFragmentation( RtpPacket packet )
        {
            throw new NotImplementedException();
        }

        public void WriteStopFragmentation( RtpPacket packet )
        {
            throw new NotImplementedException();
        }

        public void WriteFragmentation( RtpPacket packet )
        {
            throw new NotImplementedException();
        }
    }
}

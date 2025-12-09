using System;

namespace RabbitOM.Streaming.Net.Rtp.Framing.HEVC
{
    public sealed class HEVCStreamWriter : IDisposable
    {
        private static readonly byte[] StartCodePrefix = { 0x00 , 0x00 , 0x00 , 0x01 };



        private readonly RtpMemoryStream _streamOfPackets = new RtpMemoryStream();
        private readonly RtpMemoryStream _streamOfFragmentedPackets = new RtpMemoryStream();
        private readonly RtpMemoryStream _output = new RtpMemoryStream();
       


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

        public long Position
        {
            get => _streamOfPackets.Position;
        }

        public long Length
        {
            get => _streamOfPackets.Length;
        }

        public bool IsEmpty
        {
            get => _streamOfPackets.IsEmpty;
        }




        



        public bool HasParametersSets()
        {
            return _pps?.Length > 0 && _sps?.Length > 0 && _vps?.Length > 0;
        }

        public void SetLength( int value )
        {
            _streamOfPackets.SetLength( value );
        }

        public void Clear()
        {
            _streamOfPackets.Clear();
            _streamOfFragmentedPackets.Clear();
            _output.Clear();
        }

        public void ClearParameters()
        {
            _pps = null;
            _sps = null;
            _vps = null;
        }

        public void Dispose()
        {
            Clear();
            ClearParameters();

            _streamOfPackets.Dispose();
            _streamOfFragmentedPackets.Dispose();
            _output.Dispose();
        }

        public byte[] ToArray()
        {
            _output.SetLength(0);

            if ( _vps?.Length > 0 )
            {
                _output.WriteAsBinary( StartCodePrefix );
                _output.WriteAsBinary( _vps );
            }

            if ( _sps?.Length > 0 )
            {
                _output.WriteAsBinary( StartCodePrefix );
                _output.WriteAsBinary( _sps );
            }

            if ( _pps?.Length > 0 )
            {
                _output.WriteAsBinary( StartCodePrefix );
                _output.WriteAsBinary( _pps );
            }

            _output.WriteAsBinary( _streamOfPackets );
            
            return _output.ToArray();
        }

        public void WritePPS( HEVCPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            _pps = packet.Payload.ToArray();
        }

        public void WriteSPS( HEVCPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            _sps = packet.Payload.ToArray();
        }

        public void WriteVPS( HEVCPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            _vps = packet.Payload.ToArray();
        }

        public void WriteAU( HEVCPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            foreach ( var aggregation in packet.GetAggregationUnits() )
            {
                _streamOfPackets.WriteAsBinary( StartCodePrefix );
                _streamOfPackets.WriteAsBinary( aggregation );
            }
        }

        public void WriteFU( HEVCPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( HEVCPacketFU.TryParse( packet.Payload , out var fragmentationUnit ) )
            {
                if ( HEVCPacketFU.IsStartPacket( ref fragmentationUnit ) )
                {
                    if ( ! _streamOfFragmentedPackets.IsEmpty )
                    {
                        throw new InvalidOperationException( "not start fu packet was already received" );
                    }

                    _streamOfFragmentedPackets.WriteAsUInt16( HEVCPacketFU.CreateHeader( ref fragmentationUnit , packet.Header ) );
                    _streamOfFragmentedPackets.WriteAsBinary( packet.Payload );
                }

                else if ( HEVCPacketFU.IsIntermediaryPacket( ref fragmentationUnit ) )
                {
                    if ( _streamOfFragmentedPackets.IsEmpty )
                    {
                        throw new InvalidOperationException( "not start fu packet has been received" );
                    }

                    _streamOfFragmentedPackets.WriteAsBinary( packet.Payload );
                }

                else if ( HEVCPacketFU.IsStopPacket( ref fragmentationUnit ) )
                {
                    if ( _streamOfFragmentedPackets.IsEmpty )
                    {
                        throw new InvalidOperationException( "not start fu packet has been received" );
                    }

                    _streamOfFragmentedPackets.WriteAsBinary( packet.Payload );

                    _streamOfPackets.WriteAsBinary( StartCodePrefix );
                    _streamOfPackets.WriteAsBinary( _streamOfFragmentedPackets );

                    _streamOfFragmentedPackets.Clear();
                }
            }
        }

        public void Write( HEVCPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            _streamOfPackets.WriteAsBinary( StartCodePrefix );
            _streamOfPackets.WriteAsBinary( packet.Payload );
        }
    }
}

using System;
using System.Net.Http.Headers;

namespace RabbitOM.Streaming.Net.Rtp.Framing.HEVC
{
    public sealed class HEVCStreamWriter : IDisposable
    {
        private static readonly byte[] StartCodePrefix = { 0x00 , 0x00 , 0x00 , 0x01 };



        private readonly RtpMemoryStream _stream = new RtpMemoryStream();
        private readonly RtpMemoryStream _fragmentedStream = new RtpMemoryStream();
        private int _fragmentationType;


        private byte[] _pps;
        private byte[] _sps;
        private byte[] _vps;



        public byte[] PPS
        {
            get => _pps;
        }

        public byte[] SPS
        {
            get => _sps;
        }

        public byte[] VPS
        {
            get => _vps;
        }

        public long Position
        {
            get => _stream.Position;
        }

        public long Length
        {
            get => _stream.Length;
        }

        public bool IsEmpty
        {
            get => _stream.IsEmpty;
        }

        

        public bool HasParametersSets()
        {
            return _pps != null && _pps.Length > 0
                && _sps != null && _sps.Length > 0
                && _vps != null && _vps.Length > 0;
        }

        public void SetLength( int value )
        {
            _stream.SetLength( value );
        }

        public void SetFragmentationType( int value )
        {
            _fragmentationType = value;
        }

        public void Clear()
        {
            _pps = null;
            _sps = null;
            _vps = null;

            _fragmentationType = 0;

            _fragmentedStream.Clear();
            _stream.Clear();
        }

        public void Dispose()
        {
            Clear();

            _fragmentedStream.Dispose();
            _stream.Dispose();
        }

        public byte[] ToArray()
        {
            return _stream.ToArray();
        }

        public void Write( HEVCPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            _stream.WriteAsBinary( StartCodePrefix );
            _stream.WriteAsBinary( packet.Payload );
        }

        public void WritePPS( HEVCPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            _stream.WriteAsBinary( StartCodePrefix );
            _stream.WriteAsBinary( packet.Payload );

            _pps = packet.Payload.ToArray();
        }


        public void WriteSPS( HEVCPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            _stream.WriteAsBinary( StartCodePrefix );
            _stream.WriteAsBinary( packet.Payload );

            _sps = packet.Payload.ToArray();
        }

        public void WriteVPS( HEVCPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            _stream.WriteAsBinary( StartCodePrefix );
            _stream.WriteAsBinary( packet.Payload );

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
                _stream.WriteAsBinary( StartCodePrefix );
                _stream.WriteAsBinary( aggregation );
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
                    if ( ! _fragmentedStream.IsEmpty )
                    {
                        throw new InvalidOperationException( "not start fu packet was already received" );
                    }

                    _fragmentedStream.WriteAsUInt16( HEVCPacketFU.CreateHeader( ref fragmentationUnit , packet.Header ) );
                    _fragmentedStream.WriteAsBinary( packet.Payload );
                    return;
                }

                if ( HEVCPacketFU.IsIntermediaryPacket( ref fragmentationUnit ) )
                {
                    if ( _fragmentedStream.IsEmpty )
                    {
                        throw new InvalidOperationException( "not start fu packet has been received" );
                    }

                    _fragmentedStream.WriteAsBinary( packet.Payload );
                    return;
                }

                if ( HEVCPacketFU.IsStopPacket( ref fragmentationUnit ) )
                {
                    if ( _fragmentedStream.IsEmpty )
                    {
                        throw new InvalidOperationException( "not start fu packet has been received" );
                    }

                    _fragmentedStream.WriteAsBinary( packet.Payload );

                    _stream.WriteAsBinary( StartCodePrefix );
                    _stream.WriteAsBinary( _fragmentedStream );

                    _fragmentedStream.Clear();
                    return;
                }
            }
        }
    }
}

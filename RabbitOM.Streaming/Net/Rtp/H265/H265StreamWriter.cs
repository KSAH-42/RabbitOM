using RabbitOM.Streaming.Net.Rtp.H265.Headers;
using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    public sealed class H265StreamWriter : IDisposable
    {
        private static readonly byte[] StartCodePrefix = { 0x00 , 0x00 , 0x00 , 0x01 };



        private readonly RtpMemoryStream _streamOfPackets = new RtpMemoryStream();
        private readonly RtpMemoryStream _streamOfFragmentedPackets = new RtpMemoryStream();


        private byte[] _pps;
        private byte[] _sps;
        private byte[] _vps;
        private byte[] _paramsBuffer;


        
        
        public byte[] PPS
        {
            get => _pps;
            set
            {
                _pps = value;
                _paramsBuffer = null;
            }
        }

        public byte[] SPS
        {
            get => _sps;
            set
            {
                _sps = value;
                _paramsBuffer = null; 
            }
        }

        public byte[] VPS
        {
            get => _vps;
            set
            {
                _vps = value;
                _paramsBuffer = null;
            }
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
            _paramsBuffer = null;

            _streamOfPackets.Clear();
        }

        public void Dispose()
        {
            _streamOfPackets.Dispose();
            _streamOfFragmentedPackets.Dispose();
        }

        public byte[] ToArray()
        {
            return _streamOfPackets.ToArray();
        }

        public bool HasParameters()
        {
            return _pps?.Length > 0 && _sps?.Length > 0 && _vps?.Length > 0;
        }

        public byte[] GetParamtersBuffer()
        {
            if ( _paramsBuffer?.Length > 0 )
            {
                return _paramsBuffer;
            }

            var result = new List<byte>();
            var sps_pps = new List<byte>();

            if ( _sps?.Length > 0 )
            {
                sps_pps.AddRange( StartCodePrefix );
                sps_pps.AddRange( _sps );
            }

            if ( _pps?.Length > 0 )
            {
                sps_pps.AddRange( StartCodePrefix );
                sps_pps.AddRange( _pps );
            }

            if ( sps_pps.Count > 0 )
            {
                result.AddRange( StartCodePrefix );
                result.AddRange( sps_pps );
            }

            if ( _vps?.Length > 0 )
            {
                result.AddRange( StartCodePrefix );
                result.AddRange( _vps );
            }

            _paramsBuffer = result.ToArray();

            return _paramsBuffer;
        }

        public void Write( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            _streamOfPackets.WriteAsBinary( StartCodePrefix );
            _streamOfPackets.WriteAsBinary( packet.Payload );
        }

        public void WritePPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( NalUnitHeader.TryParse( packet.Payload , out var header ) )
            {
                _streamOfPackets.WriteAsBinary( StartCodePrefix );
                _streamOfPackets.WriteAsBinary( packet.Payload );

                PPS = header.Payload.ToArray();
            }
        }

        public void WriteSPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( NalUnitHeader.TryParse( packet.Payload , out var header ) )
            {
                _streamOfPackets.WriteAsBinary( StartCodePrefix );
                _streamOfPackets.WriteAsBinary( packet.Payload );

                SPS = header.Payload.ToArray();
            }
        }

        public void WriteVPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( NalUnitHeader.TryParse( packet.Payload , out var header ) )
            {
                _streamOfPackets.WriteAsBinary( StartCodePrefix );
                _streamOfPackets.WriteAsBinary( packet.Payload );

                VPS = header.Payload.ToArray();
            }
        }

        public void WriteAggregation( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            foreach ( var aggregate in NalUnitHeader.ParseAggregates( packet.Payload ) )
            {
                _streamOfPackets.WriteAsBinary( StartCodePrefix );
                _streamOfPackets.WriteAsBinary( aggregate );
            }
        }

        public void WriteFragmentation( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( NalUnitFragmentationHeader.TryParse( packet.Payload , out var header ) )
            {
                if ( NalUnitFragmentationHeader.IsStartPacket( ref header ) )
                {
                    _streamOfFragmentedPackets.Clear();

                    _streamOfFragmentedPackets.WriteAsBinary( StartCodePrefix );
                    _streamOfFragmentedPackets.WriteAsUInt16( NalUnitFragmentationHeader.ParseNalHeader( packet.Payload ) );
                    _streamOfFragmentedPackets.WriteAsBinary( header.Payload );
                }
                else if ( NalUnitFragmentationHeader.IsIntermediaryPacket( ref header ) )
                {
                    _streamOfFragmentedPackets.WriteAsBinary( header.Payload );
                }

                else if ( NalUnitFragmentationHeader.IsStopPacket( ref header ) )
                {
                    _streamOfFragmentedPackets.WriteAsBinary( header.Payload );
                    
                    _streamOfPackets.WriteAsBinary( _streamOfFragmentedPackets );

                    _streamOfFragmentedPackets.Clear();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    public sealed class H264StreamWriterSettings
    {
        public static readonly byte[] StartCodePrefixV1 = { 0x00 , 0x00 , 0x01 };



        private byte[] _startCodePrefix = StartCodePrefixV1;
        private byte[] _pps;
        private byte[] _sps;
        private byte[] _paramsBuffer;


        

        public byte[] StartCodePrefix
        {
            get => _startCodePrefix;
            
            set
            {
                ValidateStartCodePrefix( value);
                
                _startCodePrefix = value;
                _paramsBuffer = null;
            }
        }

        
        public byte[] PPS
        {
            get
            {
                return _pps;
            }

            set
            {
                _pps = value;
                _paramsBuffer = null;
            }
        }

        public byte[] SPS
        {
            get
            {
                return _sps;
            }

            set
            {
                _sps = value;
                _paramsBuffer = null;
            }
        }




        public static void ValidateStartCodePrefix( byte[] prefix )
        {
            if ( prefix == null )
            {
                throw new ArgumentNullException( nameof( prefix ) );
            }

            if ( ! StartCodePrefixV1.SequenceEqual( prefix ) )
            {
                throw new InvalidOperationException( "the start code prefix is invalid" );
            }
        }

        public static void AssignParameters( H264StreamWriterSettings settings , byte[] pps , byte[] sps )
        {
            if ( settings == null )
            {
                throw new ArgumentNullException( nameof( settings ) );
            }

            if ( settings.PPS == null || settings.PPS.Length == 0 )
            {
                settings.PPS = pps;
            }

            if ( settings.SPS == null || settings.SPS.Length == 0 )
            {
                settings.SPS = pps;
            }
        }








        public bool TryValidate()
        {
            return _pps?.Length > 0 && _sps?.Length > 0 && _startCodePrefix?.Length > 0;
        }

        public byte[] BuildParamsBuffer()
        {
            if ( _paramsBuffer?.Length > 0 )
            {
                return _paramsBuffer;
            }

            Diagnostics.Debug.EnsureCondition( _startCodePrefix?.Length > 0 );

            var result = new List<byte>();

            if ( _sps?.Length > 0 )
            {
                result.AddRange( _startCodePrefix );
                result.AddRange( _sps );
            }

            if ( _pps?.Length > 0 )
            {
                result.AddRange( _startCodePrefix );
                result.AddRange( _pps );
            }

            _paramsBuffer = result.ToArray();

            return _paramsBuffer;
        }

        public void Clear()
        {
            _pps = null;
            _sps = null;
            _paramsBuffer = null;
        }
    }
}

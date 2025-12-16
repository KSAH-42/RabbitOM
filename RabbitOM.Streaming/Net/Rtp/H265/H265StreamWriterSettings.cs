using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    public sealed class H265StreamWriterSettings
    {
        private static readonly byte[] DefaultStartCodePrefix = { 0x00 , 0x00 , 0x00 , 0x01 };



        private byte[] _pps;
        private byte[] _sps;
        private byte[] _vps;
        private byte[] _paramsBuffer;


        

        public ArraySegment<byte> StartCodePrefix
        {
            get => new ArraySegment<byte>( DefaultStartCodePrefix );
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

        public byte[] VPS
        {
            get
            {
                return _vps;
            }

            set
            {
                _vps = value;
                _paramsBuffer = null;
            }
        }






        public static void AssignParameters( H265StreamWriterSettings settings , byte[] pps , byte[] sps , byte[] vps )
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

            if ( settings.VPS == null || settings.VPS.Length == 0 )
            {
                settings.VPS = pps;
            }
        }











        public bool HasParameters()
        {
            return _pps?.Length > 0 && _sps?.Length > 0 && _vps?.Length > 0;
        }


        public void Clear()
        {
            _pps = null;
            _sps = null;
            _vps = null;
            _paramsBuffer = null;
        }


        public byte[] GetParamsBuffer()
        {
            if ( _paramsBuffer?.Length > 0 )
            {
                return _paramsBuffer;
            }

            var result = new List<byte>();

            if ( _sps?.Length > 0 )
            {
                result.AddRange( StartCodePrefix );
                result.AddRange( _sps );
            }

            if ( _pps?.Length > 0 )
            {
                result.AddRange( StartCodePrefix );
                result.AddRange( _pps );
            }

            if ( _vps?.Length > 0 )
            {
                result.AddRange( StartCodePrefix );
                result.AddRange( _vps );
            }

            _paramsBuffer = result.ToArray();

            return _paramsBuffer;
        }
    }
}

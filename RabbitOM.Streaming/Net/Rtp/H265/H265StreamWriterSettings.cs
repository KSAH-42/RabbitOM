using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    /// <summary>
    /// Represent a H265 stream writer settings class
    /// </summary>
    public sealed class H265StreamWriterSettings
    {
        /// <summary>
        /// The start code prefix version with 3 remaing bytes equal to zero
        /// </summary>
        public static readonly byte[] StartCodePrefixV1 = { 0x00 , 0x00 , 0x00 , 0x01 };
        
        /// <summary>
        /// The start code prefix version with 4 remaing bytes equal to zero
        /// </summary>
        public static readonly byte[] StartCodePrefixV2 = { 0x00 , 0x00 , 0x00 , 0x00 , 0x01 };







        private byte[] _startCodePrefix = StartCodePrefixV1;
        
        private byte[] _pps;
        
        private byte[] _sps;
        
        private byte[] _vps;
        
        private byte[] _paramsBuffer;







        

        /// <summary>
        /// Gets / Sets the start code prefix
        /// </summary>
        /// <exception cref="InvalidOperationException">throw in case during validation of the prefic code</exception>
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

        /// <summary>
        /// Gets / Sets the pps
        /// </summary>
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

        /// <summary>
        /// Gets / Sets the SPS
        /// </summary>
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

        /// <summary>
        /// Gets / Sets the vps
        /// </summary>
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








        /// <summary>
        /// Validate the start code prefix code
        /// </summary>
        /// <param name="prefix">the prefix code</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        public static void ValidateStartCodePrefix( byte[] prefix )
        {
            if ( prefix == null )
            {
                throw new ArgumentNullException( nameof( prefix ) );
            }

            if ( ! StartCodePrefixV1.SequenceEqual( prefix ) || ! StartCodePrefixV2.SequenceEqual( prefix ) )
            {
                throw new InvalidOperationException( "the start code prefix is invalid" );
            }
        }

        /// <summary>
        /// Assign parameters (helper method)
        /// </summary>
        /// <param name="settings">the settings</param>
        /// <param name="pps">the pps</param>
        /// <param name="sps">the sps</param>
        /// <param name="vps">the vps</param>
        /// <exception cref="ArgumentNullException"></exception>
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








        /// <summary>
        /// Try to validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryValidate()
        {
            return _pps?.Length > 0 && _sps?.Length > 0 && _vps?.Length > 0 && _startCodePrefix?.Length > 0;
        }
        
        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _pps = null;
            _sps = null;
            _vps = null;
            _paramsBuffer = null;
        }

        /// <summary>
        /// Build the params buffers if some properties has been changed
        /// </summary>
        /// <returns>returns the buffer</returns>
        public byte[] CreateParamsBuffer()
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

            if ( _vps?.Length > 0 )
            {
                result.AddRange( _startCodePrefix );
                result.AddRange( _vps );
            }

            _paramsBuffer = result.ToArray();

            return _paramsBuffer;
        }
    }
}

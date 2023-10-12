using System;
using System.Linq;
using System.Collections.Generic;

namespace RabbitOM.Net.Rtps.Codecs
{
    /// <summary>
    /// Represent a video codec
    /// </summary>
    public sealed class H264Codec : VideoCodec
    {
        private readonly byte[] _sps_pps = null;

       



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sps_pps">the sequence parameter set</param>
        public H264Codec( byte[] sps_pps )
        {
            _sps_pps = sps_pps ?? new byte[ 0 ];
        }
        



        /// <summary>
        /// Gets the codec type
        /// </summary>
        public override CodecType Type
        {
            get => CodecType.H264;
        }
        
        /// <summary>
        /// Gets the sequence parameter bytes
        /// </summary>
        public byte[] SPS_PPS
        {
            get => _sps_pps;
        }
        



        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true</returns>
        public override bool Validate()
        {
            if ( _sps_pps == null || _sps_pps.Length <= 0 )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Create a codec descriptor
        /// </summary>
        /// <returns>returns an instance, otherwise null</returns>
        public static H264Codec Create()
        {
            return Create( CodecConstants.Default_H264_SPS , CodecConstants.Default_H264_PPS );
        }

        /// <summary>
        /// Create a codec descriptor
        /// </summary>
        /// <param name="sps">the sps value</param>
        /// <param name="pps">the pps value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static H264Codec Create( string sps , string pps )
        {
            if ( string.IsNullOrWhiteSpace( sps ) || string.IsNullOrWhiteSpace( pps ) )
            {
                return null;
            }

            try
            {
                var spsBuffer = Convert.FromBase64String( sps.Trim() );

                if ( spsBuffer == null || spsBuffer.Length <= 0 )
                {
                    return null;
                }

                var spsBytes = CodecConstants.H264StartMarker.Concat( spsBuffer );

                if ( spsBytes == null )
                {
                    return null;
                }                

                var ppsBuffer = Convert.FromBase64String( pps.Trim() );

                if ( ppsBuffer == null || ppsBuffer.Length <= 0 )
                {
                    return null;
                }

                var ppsBytes = CodecConstants.H264StartMarker.Concat( ppsBuffer );

                if ( ppsBytes == null )
                {
                    return null;
                }

                return new H264Codec( spsBytes.Concat( ppsBytes ).ToArray() );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return null;
        }
    }
}

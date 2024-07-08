using System;
using System.Linq;

namespace RabbitOM.Streaming.Codecs
{
    /// <summary>
    /// Represent a video codec
    /// </summary>
    public sealed class H264CodecInfo : VideoCodecInfo
    {
        private readonly byte[] _sps_pps = null;

       



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sps_pps">the sequence parameter set</param>
        public H264CodecInfo( byte[] sps_pps )
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
        public override bool TryValidate()
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
        public static H264CodecInfo Create()
        {
            return Create( CodecInfo.Default_H264_SPS , CodecInfo.Default_H264_PPS );
        }

        /// <summary>
        /// Create a codec descriptor
        /// </summary>
        /// <param name="sps">the sps value</param>
        /// <param name="pps">the pps value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static H264CodecInfo Create( string sps , string pps )
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

                var spsBytes = CodecInfo.H264StartMarker.Concat( spsBuffer );

                if ( spsBytes == null )
                {
                    return null;
                }                

                var ppsBuffer = Convert.FromBase64String( pps.Trim() );

                if ( ppsBuffer == null || ppsBuffer.Length <= 0 )
                {
                    return null;
                }

                var ppsBytes = CodecInfo.H264StartMarker.Concat( ppsBuffer );

                if ( ppsBytes == null )
                {
                    return null;
                }

                return new H264CodecInfo( spsBytes.Concat( ppsBytes ).ToArray() );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return null;
        }
    }
}

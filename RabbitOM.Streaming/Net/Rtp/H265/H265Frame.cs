using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    /// <summary>
    /// Represent a H265 frame
    /// </summary>
    public sealed class H265Frame : RtpFrame
    {
        /// <summary>
        /// Initialize an new instance of a H265 frame
        /// </summary>
        /// <param name="data">the data</param>
        /// <param name="startCodePrefix">the start code prefix</param>
        /// <param name="pps">the pps</param>
        /// <param name="sps">the sps</param>
        /// <param name="vps">the vps</param>
        public H265Frame( byte[] data , byte[] startCodePrefix , byte[] pps , byte[] sps , byte[] vps ) 
            : base ( data )
        {
            StartCodePrefix = startCodePrefix;
            PPS = pps;
            SPS = sps;
            VPS = vps;
        }





        /// <summary>
        /// Gets the start code prefix
        /// </summary>
        public byte[] StartCodePrefix { get; }

        /// <summary>
        /// Gets the PPS
        /// </summary>
        public byte[] PPS { get; }
        
        /// <summary>
        /// Gets the SPS
        /// </summary>
        public byte[] SPS { get; }

        /// <summary>
        /// Gets the VPS
        /// </summary>
        public byte[] VPS { get; }






        /// <summary>
        /// Build the params buffers if some properties has been changed
        /// </summary>
        /// <returns>returns the buffer</returns>
        /// <exception cref="ArgumentNullException"/>
        public static byte[] CreateParamsBuffer( H265Frame frame )
        {
            if ( frame == null )
            {
                throw new ArgumentNullException( nameof( frame ) );
            }

            return CreateParamsBuffer( frame.StartCodePrefix , frame.PPS , frame.SPS , frame.VPS );
        }


        /// <summary>
        /// Build the params buffers if some properties has been changed
        /// </summary>
        /// <returns>returns the buffer</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public static byte[] CreateParamsBuffer( byte[] startCodePrefix , byte[] pps , byte[] sps , byte[] vps )
        {
            if ( startCodePrefix == null )
            {
                throw new ArgumentNullException( nameof( startCodePrefix ) );
            }

            if ( startCodePrefix.Length == 0 )
            {
                throw new ArgumentException( nameof( startCodePrefix ) );
            }

            var result = new List<byte>();

            if ( sps?.Length > 0 )
            {
                result.AddRange( startCodePrefix );
                result.AddRange( sps );
            }

            if ( pps?.Length > 0 )
            {
                result.AddRange( startCodePrefix );
                result.AddRange( pps );
            }

            if ( vps?.Length > 0 )
            {
                result.AddRange( startCodePrefix );
                result.AddRange( vps );
            }

            return result.ToArray();
        }
    }
}
using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    /// <summary>
    /// Represent a H264 frame
    /// </summary>
    public sealed class H264Frame : MediaContent
    {
        /// <summary>
        /// Initialize a new instance of a H264 frame
        /// </summary>
        /// <param name="startCodePrefix">the start prefix</param>
        /// <param name="pps">the pps</param>
        /// <param name="sps">the sps</param>
        /// <param name="data">the data</param>
        public H264Frame( byte[] startCodePrefix , byte[] pps , byte[] sps , byte[] data ) : base ( data )
        {
            StartCodePrefix = startCodePrefix;
            PPS = pps;
            SPS = sps;
        }






        /// <summary>
        /// Gets the start code prefix
        /// </summary>
        public byte[] StartCodePrefix { get; }

        /// <summary>
        /// Gets the pps
        /// </summary>
        public byte[] PPS { get; }
        
        /// <summary>
        /// Gets the sps
        /// </summary>
        public byte[] SPS { get; }







        /// <summary>
        /// Combine params as buffer
        /// </summary>
        /// <param name="frame">the frame</param>
        /// <returns>returns a buffer</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static byte[] CreateParamsBuffer( H264Frame frame )
        {
            if ( frame == null )
            {
                throw new ArgumentNullException( nameof( frame ) );
            }

            return CreateParamsBuffer( frame.StartCodePrefix , frame.PPS , frame.SPS );
        }

        /// <summary>
        /// Combine parameters as a buffer
        /// </summary>
        /// <param name="startCodePrefix">the start code</param>
        /// <param name="pps">the pps</param>
        /// <param name="sps">the sps</param>
        /// <returns>returns a buffer</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] CreateParamsBuffer( byte[] startCodePrefix , byte[] pps , byte[] sps )
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

            return result.ToArray();
        }
    }
}
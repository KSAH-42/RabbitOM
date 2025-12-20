using System;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    /// <summary>
    /// Represent a H265 constants values
    /// </summary>
    public static class H265Constants
    {
        /// <summary>
        /// The start code prefix version with 3 remaing bytes equal to zero
        /// </summary>
        public static readonly byte[] StartCodePrefixV1 = { 0x00 , 0x00 , 0x00 , 0x01 };
        
        /// <summary>
        /// The start code prefix version with 4 remaing bytes equal to zero
        /// </summary>
        public static readonly byte[] StartCodePrefixV2 = { 0x00 , 0x00 , 0x00 , 0x00 , 0x01 };
    }
}
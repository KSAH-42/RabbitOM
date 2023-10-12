using System;

namespace RabbitOM.Net.Rtps.Codecs
{
    /// <summary>
    /// Represent the type of the codec
    /// </summary>
    public enum CodecType
    {
        /// <summary>
        /// Invalid type
        /// </summary>
        None = 0, 

        /// <summary>
        /// Video codec
        /// </summary>
        H264 ,

        /// <summary>
        /// Video codec
        /// </summary>
        H265,

        /// <summary>
        /// Video codec
        /// </summary>
        MJPEG ,

        /// <summary>
        /// Audio codec
        /// </summary>
        AAC,

        /// <summary>
        /// Audio codec
        /// </summary>
        G711A,
        
        /// <summary>
        /// Audio codec
        /// </summary>
        G711C,
        
        /// <summary>
        /// Audio codec
        /// </summary>
        G726,
        
        /// <summary>
        /// Audio codec
        /// </summary>
        PCM,
    }
}

using System;

namespace RabbitOM.Streaming.Codecs
{
    /// <summary>
    /// Represent the type of the codec
    /// </summary>
    public enum CodecType
    {
        /// <summary>
        /// Video codec
        /// </summary>
        Jpeg,

        /// <summary>
        /// Video codec
        /// </summary>
        H264,

        /// <summary>
        /// Video codec
        /// </summary>
        H265,

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

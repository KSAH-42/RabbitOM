using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a null codec info class
    /// </summary>
    public sealed class NullCodecInfo : CodecInfo
    {
        /// <summary>
        /// Represent a null value
        /// </summary>
        public readonly static NullCodecInfo Value = new NullCodecInfo();
        



        /// <summary>
        /// Gets the type
        /// </summary>
        public override CodecType Type
        {
            get => CodecType.None;
        }




        /// <summary>
        /// Perform a validation
        /// </summary>
        /// <returns>returns false</returns>
        public override bool TryValidate()
        {
            return false;
        }
    }
}

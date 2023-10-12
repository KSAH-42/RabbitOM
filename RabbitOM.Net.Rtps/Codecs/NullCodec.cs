using System;

namespace RabbitOM.Net.Rtps.Codecs
{
    /// <summary>
    /// Represent a null codec info class
    /// </summary>
    public sealed class NullCodec : Codec
    {
        /// <summary>
        /// Represent a null value
        /// </summary>
        public readonly static NullCodec Value = new NullCodec();
        



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
        public override bool Validate()
        {
            return false;
        }
    }
}

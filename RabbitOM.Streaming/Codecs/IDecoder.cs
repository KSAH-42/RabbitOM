using System;

namespace RabbitOM.Streaming.Codecs
{
    /// <summary>
    /// Represent the base decoder
    /// </summary>
    public interface IDecoder : IDisposable
    {
        /// <summary>
        /// Try to decode the data
        /// </summary>
        /// <param name="buffer">the compressed data</param>
        /// <param name="result">the uncompressed data</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        bool TryDecode( byte[] buffer , out byte[] result );
    }
}

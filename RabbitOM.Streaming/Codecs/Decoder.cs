using System;

namespace RabbitOM.Streaming.Codecs
{
    /// <summary>
    /// Represent the base decoder
    /// </summary>
    public abstract class Decoder : IDecoder
    {
        /// <summary>
        /// Finalizer, free unmanaged resources
        /// </summary>
        ~Decoder()
        {
            Dispose( false );
        }

        /// <summary>
        /// Try to decode the data
        /// </summary>
        /// <param name="buffer">the compressed data</param>
        /// <param name="result">the uncompressed data</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public abstract bool TryDecode( byte[] buffer , out byte[] result );

        /// <summary>
        /// Release internal resources
        /// </summary>
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        /// <summary>
        /// Release internal resources
        /// </summary>
        /// <param name="disposing">true when the disposing method is call explicitly</param>
        protected virtual void Dispose( bool disposing )
        {
        }
    }
}

using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent the pipe line data buffer class
    /// </summary>
    internal sealed class RTSPPipeLineDataBuffer : RTSPPipeLineData
    {
        private readonly byte[] _buffer;
        
        /// <summary>
        /// Initialize a new instance data class
        /// </summary>
        /// <param name="buffer"></param>
        public RTSPPipeLineDataBuffer( byte[] buffer )
        {
            if ( buffer == null )
            {
                throw new ArgumentNullException( nameof( buffer ) );
            }

            if ( buffer.Length == 0 )
            {
                throw new ArgumentException( nameof( buffer ) );
            }

            _buffer = buffer;
        }

        /// <summary>
        /// Gets the buffer
        /// </summary>
        public byte[] Buffer
        {
            get => _buffer;
        }
    }
}

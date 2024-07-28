using System;

namespace RabbitOM.Streaming.Rtp.Framing
{
    /// <summary>
    /// Represent base frame class
    /// </summary>
    public class RtpFrame
    {
        private readonly byte[] _data;

        /// <summary>
        /// Initialize an new instance of the frame class
        /// </summary>
        /// <param name="data">the data used for the decoder</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public RtpFrame( byte[] data )
        {
            if ( data == null )
            {
                throw new ArgumentNullException( nameof( data ) );
            }

            if ( data.Length == 0 )
            {
                throw new ArgumentException( nameof( data ) );
            }

            _data = data;
        }

        /// <summary>
        /// Gets the data
        /// </summary>
        public byte[] Data
        {
            get => _data;
        }
    }
}
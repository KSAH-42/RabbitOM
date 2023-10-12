using System;

namespace RabbitOM.Net.Rtps.Codecs
{
    /// <summary>
    /// Represent an audio codec
    /// </summary>
    public abstract class G711Codec : AudioCodec
    {
        private readonly int    _samplingRate   = 0;

        private readonly int    _channels       = 0;
        



        /// <summary>
        /// Constructor
        /// </summary>
        public G711Codec()
            : this ( 8000 , 1 )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="samplingRate">the sampling rate</param>
        /// <param name="channels">the channels</param>
        protected G711Codec( int samplingRate , int channels )
        {
            _samplingRate = samplingRate;
            _channels     = channels;
        }
        



        /// <summary>
        /// Gets the sampling rate
        /// </summary>
        public int SamplingRate
        {
            get => _samplingRate;
        }

        /// <summary>
        /// Gets the channels
        /// </summary>
        public int Channels
        {
            get => _channels;
        }




        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool Validate()
        {
            return _samplingRate > 0 && _channels >= 0;
        }
    }
}

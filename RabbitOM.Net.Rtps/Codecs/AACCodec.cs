using System;

namespace RabbitOM.Net.Rtps.Codecs
{
    /// <summary>
    /// Represent an audio codec
    /// </summary>
    public sealed class AACCodec : AudioCodec
    {
        private readonly byte[] _data           = null;

        private readonly int    _size           = 0;

        private readonly int    _indexLength    = 0;

        private readonly int    _deltaLength    = 0;




        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">the data</param>
        /// <param name="size">the size</param>
        /// <param name="index">the index</param>
        /// <param name="delta">the delta</param>
        public AACCodec( byte[] data , int size , int index , int delta )
        {
            _data  = data ?? new byte[ 0 ];
            _size  = size;
            _indexLength = index;
            _deltaLength = 0;
        }




        /// <summary>
        /// Gets the type
        /// </summary>
        public override CodecType Type
        {
            get => CodecType.AAC;
        }

        /// <summary>
        /// Gets the data
        /// </summary>
        public byte[] Data
        {
            get => _data;
        }

        /// <summary>
        /// Gets the size
        /// </summary>
        public int Size
        {
            get => _size;
        }

        /// <summary>
        /// Gets the index length
        /// </summary>
        public int IndexLength
        {
            get => _indexLength;
        }

        /// <summary>
        /// Gets the delta length
        /// </summary>
        public int DeltaLength
        {
            get => _deltaLength;
        }
        



        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool Validate()
        {
            if ( _data == null || _data.Length <= 0 )
            {
                return false;
            }

            if ( _size <= 0 )
            {
                return false;
            }

            if ( _indexLength <= 0 || _deltaLength <= 0 )
            {
                return false;
            }

            return true;
        }
    }
}

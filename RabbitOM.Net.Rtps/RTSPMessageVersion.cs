using System;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent the message version
    /// </summary>
    public sealed class RTSPMessageVersion
    {
        /// <summary>
        /// Represent the default version 1.0
        /// </summary>
        public readonly static RTSPMessageVersion   Version_1_0    = new RTSPMessageVersion( 1 , 0 );

        /// <summary>
        /// Represent the default version 1.1
        /// </summary>
        public readonly static RTSPMessageVersion   Version_1_1    = new RTSPMessageVersion( 1 , 1 );

        /// <summary>
        /// Represent the default version 1.2
        /// </summary>
        public readonly static RTSPMessageVersion   Version_1_2    = new RTSPMessageVersion( 1 , 2 );






        /// <summary>
        /// The major number
        /// </summary>
        private readonly int _majorNumber   = 0;

        /// <summary>
        /// The minor number
        /// </summary>
        private readonly int _minorNumber   = 0;






        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="majorNumber">the major version</param>
        /// <param name="minorNumber">the minor version</param>
        public RTSPMessageVersion( int majorNumber , int minorNumber )
        {
            _majorNumber = majorNumber;
            _minorNumber = minorNumber;
        }






        /// <summary>
        /// Gets the major version
        /// </summary>
        public int MajorNumber
        {
            get => _majorNumber;
        }

        /// <summary>
        /// Gets the minor version
        /// </summary>
        public int MinorNumber
        {
            get => _minorNumber;
        }






        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        internal bool Validate()
        {
            if ( _majorNumber == 0 && _minorNumber == 0 )
            {
                return false;
            }

            if ( _majorNumber < 0 || _minorNumber < 0 )
            {
                return false;
            }

            return true;
        }
    }
}

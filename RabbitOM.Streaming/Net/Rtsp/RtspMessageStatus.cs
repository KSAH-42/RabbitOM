using System;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent the message status
    /// </summary>
    public sealed class RtspMessageStatus
    {
        /// <summary>
        /// Represent a null object value
        /// </summary>
        public  readonly static RtspMessageStatus UnDefined = new RtspMessageStatus( RtspStatusCode.UnDefined , "Un defined" );






        /// <summary>
        /// The code
        /// </summary>
        private readonly RtspStatusCode           _code     = RtspStatusCode.UnDefined;

        /// <summary>
        /// The reson
        /// </summary>
        private readonly string                   _reason   = string.Empty;






        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">the status code</param>
        /// <param name="reason">the status details</param>
        public RtspMessageStatus( RtspStatusCode code , string reason )
        {
            _code   = code;
            _reason = RtspDataConverter.Trim( reason );
        }






        /// <summary>
        /// Gets the status code
        /// </summary>
        public RtspStatusCode Code
        {
            get => _code;
        }

        /// <summary>
        /// Gets the reason
        /// </summary>
        public string Reason
        {
            get => _reason;
        }






        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        internal bool TryValidate()
        {
            return _code != RtspStatusCode.UnDefined;
        }
    }
}

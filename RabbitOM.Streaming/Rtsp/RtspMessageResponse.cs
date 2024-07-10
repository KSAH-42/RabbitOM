using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message response
    /// </summary>
    public sealed class RtspMessageResponse : RtspMessage
    {
        private readonly RtspMessageStatus     _status      = null;

        private readonly RtspHeaderCollection  _headers     = null;

        private readonly RtspMessageBody       _body        = null;

        private readonly RtspMessageVersion    _version     = null;




        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="status">the status code</param>
        public RtspMessageResponse( RtspMessageStatus status )
            : this( status , RtspMessageVersion.Version_1_0 )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="status">the status code</param>
        /// <param name="version"></param>
        /// <exception cref="ArgumentNullException"/>
        public RtspMessageResponse( RtspMessageStatus status , RtspMessageVersion version )
        {
            _status = status ?? throw new ArgumentNullException( nameof( status ) );
            _version = version ?? throw new ArgumentNullException( nameof( version ) );
            _headers = new RtspHeaderCollection();
            _body = new RtspMessageBody();
        }




        /// <summary>
        /// Gets the message status
        /// </summary>
        public RtspMessageStatus Status
        {
            get => _status;
        }

        /// <summary>
        /// Gets the headers
        /// </summary>
        public override RtspHeaderCollection Headers
        {
            get => _headers;
        }

        /// <summary>
        /// Gets the body
        /// </summary>
        public override RtspMessageBody Body
        {
            get => _body;
        }

        /// <summary>
        /// Gets the message version
        /// </summary>
        public override RtspMessageVersion Version
        {
            get => _version;
        }




        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _status.TryValidate()
                && _version.TryValidate()
                && _headers.ContainsKey( RtspHeaderNames.CSeq );
        }

        /// <summary>
        /// Create an undefined message
        /// </summary>
        /// <returns>returns an instance</returns>
        internal static RtspMessageResponse CreateUnDefinedResponse()
        {
            return new RtspMessageResponse( RtspMessageStatus.UnDefined );
        }
    }
}

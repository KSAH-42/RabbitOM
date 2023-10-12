using System;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent the fields names
    /// </summary>
    public static class RTSPHeaderFieldNames
    {
        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Interleaved            = "interleaved";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Unicast                = "unicast";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Multicast              = "multicast";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Port                   = "port";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string ClientPort             = "client_port";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string ServerPort             = "server_port";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string SSRC                   = "ssrc";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Mode                   = "mode";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Basic                  = "Basic";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Digest                 = "Digest";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string UserName               = "username";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Realm                  = "realm";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Nonce                  = "nonce";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Response               = "response";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Algorithm              = "algorithm";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Stale                  = "stale";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Domain                 = "domain";

        /// <summary>
        /// Represent a field name used in header (this is also the equivalant of <see cref="RtpAvpUdp"/> because in majority of cases the camera didn't provide the lower transport type)
        /// </summary>
        public const string RtpAvp                 = "RTP/AVP";

        /// <summary>
        /// Represent a field name used in header (this is also the same of <see cref="RtpAvp"/> )
        /// </summary>
        public const string RtpAvpUdp              = "RTP/AVP/UDP";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string RtpAvpTcp              = "RTP/AVP/TCP";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Uri                    = "uri";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Url                    = "url";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Timeout                = "timeout";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Npt                    = "npt";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Time                   = "time";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Clock                  = "clock";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string RtpTime                = "rtptime";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Sequence               = "seq";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string TTL                    = "ttl";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Layers                 = "layers";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Destination            = "destination";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Source                 = "source";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Channel                = "channel";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Address                = "address";

        /// <summary>
        /// Represent a field name used in header
        /// </summary>
        public const string Opaque                 = "opaque";
    }
}

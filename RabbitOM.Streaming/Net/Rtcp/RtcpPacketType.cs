using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    /// <summary>
    /// Represent the packet type
    /// </summary>
    public enum RtcpPacketType : byte
    {
        /// <summary>
        /// Un defined
        /// </summary>
        UnDefined = 0,

        /// <summary>
        /// Sender report type (SR)
        /// </summary>
        SenderReport = 200 ,

        /// <summary>
        /// Report receiver type (RR)
        /// </summary>
        ReportReceiver = 201 ,

        /// <summary>
        /// Source description type (SDES)
        /// </summary>
        SourceDescription = 202 ,

        /// <summary>
        /// Byte type
        /// </summary>
        Bye = 203 ,

        /// <summary>
        /// Application Specific type (APP)
        /// </summary>
        Application = 204 ,
    }
}

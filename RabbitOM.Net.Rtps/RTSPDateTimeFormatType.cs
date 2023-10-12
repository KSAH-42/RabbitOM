using System;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent the datetime format types
    /// </summary>
    public static class RTSPDateTimeFormatType
    {
        /// <summary>
        /// Represent a greenwich mean time format
        /// </summary>
        public const string GmtFormat  = "ddd, dd MMM yyyy HH:mm:ss Z";

        /// <summary>
        /// Represent a greenwich mean time format
        /// </summary>
        public const string GmtFormat1 = "ddd, MMM dd yyyy HH:mm:ss Z";

        /// <summary>
        /// Represent a greenwich mean time format
        /// </summary>
        public const string GmtFormat2 = "ddd, yyyy MMM dd HH:mm:ss Z";

        /// <summary>
        /// Represent a greenwich mean time format
        /// </summary>
        public const string GmtFormat3 = "ddd, yyyy dd MMM HH:mm:ss Z";
    }
}

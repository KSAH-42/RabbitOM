using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the base header class used by a message class
    /// </summary>
    public abstract class RtspHeader
    {
        /// <summary>
        /// Gets the name of the header
        /// </summary>
        public abstract string Name
        {
            get;
        }

        /// <summary>
        /// Just perform a custom validation for a particular header object
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public abstract bool TryValidate();




        /// <summary>
        /// Check if the header has been defined. This method perform vital validation. And not business validation
        /// </summary>
        /// <param name="header">the header</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <remarks>
        ///  <para>This method must be mused by collection class, and called before to insert, or added any header</para>
        /// </remarks>
        public static bool IsUnDefined( RtspHeader header )
        {
            return !IsDefined( header );
        }

        /// <summary>
        /// Check if the header has been defined. This method perform vital validation. And not business validation
        /// </summary>
        /// <param name="header">the header</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <remarks>
        ///  <para>This method must be mused by collection class, and called before to insert, or added any header</para>
        /// </remarks>
        public static bool IsDefined( RtspHeader header )
        {
            return header != null && !string.IsNullOrWhiteSpace( header.Name );
        }
    }
}

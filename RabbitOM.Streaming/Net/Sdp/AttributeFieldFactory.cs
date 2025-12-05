using System;

namespace RabbitOM.Streaming.Net.Sdp
{
    /// <summary>
    /// Represent a sdp field factory
    /// </summary>
    public static class AttributeFieldFactory
    {
        /// <summary>
        /// Create an attribute
        /// </summary>
        /// <returns>returns an instance</returns>
        public static AttributeField NewSendReceiveAttribute()
        {
            return new AttributeField(AttributeNames.SendReceive);
        }

        /// <summary>
        /// Create an attribute
        /// </summary>
        /// <returns>returns an instance</returns>
        public static AttributeField NewSendOnlyAttribute()
        {
            return new AttributeField(AttributeNames.SendOnly);
        }

        /// <summary>
        /// Create an attribute
        /// </summary>
        /// <returns>returns an instance</returns>
        public static AttributeField NewReceiveOnlyAttribute()
        {
            return new AttributeField(AttributeNames.ReceiveOnly);
        }

        /// <summary>
        /// Create an attribute
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <returns>returns an instance</returns>
        /// <exception cref="ArgumentNullException"/>
        public static AttributeField NewControlAttribute(Uri uri)
        {
            if ( uri == null )
            {
                throw new ArgumentNullException( nameof(uri) );
            }

            return NewControlAttributeAsString( uri.ToString() );
        }

        /// <summary>
        /// Create an attribute
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <returns>returns an instance</returns>
        /// <exception cref="ArgumentNullException"/>
        public static AttributeField NewControlAttributeAsString(string uri)
        {
            var uriValue = ! string.IsNullOrWhiteSpace( uri ) ? uri.Trim() : "*";

            return new AttributeField(AttributeNames.Control, uriValue);
        }

        /// <summary>
        /// Create an attribute
        /// </summary>
        /// <param name="width">the width</param>
        /// <param name="heigth">the heigth</param>
        /// <returns>returns an instance</returns>
        /// <exception cref="ArgumentException"/>
        public static AttributeField NewDimensionsAttribute(long width, long heigth)
        {
            if (width <= 0)
            {
                throw new ArgumentException(nameof(width));
            }

            if (heigth <= 0)
            {
                throw new ArgumentException(nameof(heigth));
            }

            return new AttributeField(AttributeNames.Dimensions, $"{width},{heigth}");
        }

        /// <summary>
        /// Create an attribute
        /// </summary>
        /// <param name="payload">the payload</param>
        /// <param name="encoding">the encoding like H264</param>
        /// <param name="clockRate">the clock rate</param>
        /// <returns>returns an instance</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        public static AttributeField NewRTPMapAttribute(long payload, string encoding, long clockRate)
        {
            if (payload < 0)
            {
                throw new ArgumentException(nameof(payload));
            }

            if (string.IsNullOrWhiteSpace(encoding))
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (clockRate <= 0)
            {
                throw new ArgumentNullException(nameof(clockRate));
            }

            return new AttributeField(AttributeNames.RTPMap, $"{payload} {encoding}/{clockRate}");
        }
    }
}

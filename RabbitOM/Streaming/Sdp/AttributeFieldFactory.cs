using System;

namespace RabbitOM.Streaming.Sdp
{
    public static class AttributeFieldFactory
    {
        public static AttributeField NewSendReceiveAttribute()
        {
            return new AttributeField(AttributeNames.SendReceive);
        }

        public static AttributeField NewSendOnlyAttribute()
        {
            return new AttributeField(AttributeNames.SendOnly);
        }

        public static AttributeField NewReceiveOnlyAttribute()
        {
            return new AttributeField(AttributeNames.ReceiveOnly);
        }

        public static AttributeField NewControlAttribute(Uri uri)
        {
            if ( uri == null )
            {
                throw new ArgumentNullException( nameof(uri) );
            }

            return NewControlAttributeAsString( uri.ToString() );
        }

        public static AttributeField NewControlAttributeAsString(string uri)
        {
            var uriValue = ! string.IsNullOrWhiteSpace( uri ) ? uri.Trim() : "*";

            return new AttributeField(AttributeNames.Control, uriValue);
        }

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

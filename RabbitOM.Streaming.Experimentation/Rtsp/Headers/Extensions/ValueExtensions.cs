using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Extensions
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;

    internal static class ValueExtensions
    {
        public static uint? ToNullableUInt( this string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return null;
            }

            if ( uint.TryParse( value , out var result ) )
            {
                return result;
            }

            return null;
        }

        public static ushort? ToNullableUShort( this string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return null;
            }

            if ( ushort.TryParse( value , out var result ) )
            {
                return result;
            }

            return null;
        }

        public static float? ToNullableFloat( this string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return null;
            }

            if ( float.TryParse( value , out var result ) )
            {
                return result;
            }

            return null;
        }

        public static double? ToNullableDouble( this string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return null;
            }

            if ( double.TryParse( value , out var result ) )
            {
                return result;
            }

            return null;
        }

        public static string ToGmtDate( this DateTime value )
        {
            return ( value.Kind == DateTimeKind.Local ? value.ToUniversalTime() : value ).ToString( "r" , CultureInfo.InvariantCulture);
        }

        public static DateTime? ToNullableDateTime( this string value )
        {
            if ( DateTime.TryParse( StringValueNormalizer.TrimWithUnQuoteNormalizer.Normalize( value ) , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out var result ) )
            {
                return result;
            }

            return null;
        }

        public static Uri ToUri( this string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return null;
            }

            if ( Uri.TryCreate( value , UriKind.RelativeOrAbsolute , out var result ) )
            {
                return result;
            }

            return null;
        }
    }
}

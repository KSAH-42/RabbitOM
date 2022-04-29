using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a tolerant value converter
    /// </summary>
    public static class RTSPDataConverter
    {
        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( bool value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( char value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( sbyte value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( byte value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToHexString( byte value )
        {
            return string.Format( "{0:2X}" , value );
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToHexString( sbyte value )
        {
            return string.Format( "{0:2X}" , value );
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( short value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( ushort value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( int value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( uint value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( long value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( ulong value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( decimal value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( float value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( double value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( DateTime value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="format">the format</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( DateTime value , string format )
        {
            if ( string.IsNullOrWhiteSpace( format ) )
            {
                return value.ToString();
            }

            return value.ToString( format , CultureInfo.InvariantCulture );
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( TimeSpan value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString( Guid value )
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString<TEnum>( TEnum value ) where TEnum : struct
        {
            return value.ToString();
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToStringUTF8( byte[] value )
        {
            if ( value == null || value.Length == 0 )
            {
                return string.Empty;
            }

            try
            {
                return System.Text.Encoding.UTF8.GetString( value ) ?? string.Empty;
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return string.Empty;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static bool ConvertToBool( string value )
        {
            return bool.TryParse( value ?? string.Empty , out bool result ) && result;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static char ConvertToChar( string value )
        {
            return char.TryParse( value ?? string.Empty , out char result ) ? result : char.MinValue;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static sbyte ConvertToSByte( string value )
        {
            return sbyte.TryParse( value ?? string.Empty , out sbyte result ) ? result : (sbyte) 0;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static byte ConvertToByte( string value )
        {
            return byte.TryParse( value ?? string.Empty , out byte result ) ? result : byte.MinValue;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static short ConvertToShort( string value )
        {
            return short.TryParse( value ?? string.Empty , out short result ) ? result : (short) 0;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static ushort ConvertToUShort( string value )
        {
            return ushort.TryParse( value ?? string.Empty , out ushort result ) ? result : ushort.MinValue;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static int ConvertToInteger( string value )
        {
            return int.TryParse( value ?? string.Empty , out int result ) ? result : 0;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static uint ConvertToUInteger( string value )
        {
            return uint.TryParse( value ?? string.Empty , out uint result ) ? result : 0;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static long ConvertToLong( string value )
        {
            return long.TryParse( value ?? string.Empty , out long result ) ? result : 0;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static ulong ConvertToULong( string value )
        {
            return ulong.TryParse( value ?? string.Empty , out ulong result ) ? result : 0;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static decimal ConvertToDecimal( string value )
        {
            return decimal.TryParse( value ?? string.Empty , out decimal result ) ? result : 0;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static float ConvertToFloat( string value )
        {
            return float.TryParse( value ?? string.Empty , out float result ) ? result : 0;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="styles">the styles</param>
        /// <param name="provider">the provider</param>
        /// <returns>returns a value</returns>
        public static float ConvertToFloat( string value , NumberStyles styles , IFormatProvider provider )
        {
            return float.TryParse( value ?? string.Empty , styles , provider , out float result ) ? result : (float) 0;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static double ConvertToDouble( string value )
        {
            return double.TryParse( value ?? string.Empty , out double result ) ? result : 0;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static DateTime ConvertToDateTime( string value )
        {
            return DateTime.TryParse( value ?? string.Empty , out DateTime result ) ? result : DateTime.MinValue;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="format">the format</param>
        /// <returns>returns a value</returns>
        public static DateTime ConvertToDateTime( string value , string format )
        {
            if ( string.IsNullOrWhiteSpace( format ) )
            {
                return DateTime.MinValue;
            }

            return DateTime.TryParseExact( value?.Trim() ?? string.Empty , format , CultureInfo.InvariantCulture , DateTimeStyles.None , out DateTime result ) ? result : DateTime.MinValue;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static DateTime ConvertToDateTimeAsGMT( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return DateTime.MinValue;
            }

            var formats = new List<string>()
            {
                RTSPDateTimeFormatType.GmtFormat ,
                RTSPDateTimeFormatType.GmtFormat1 ,
                RTSPDateTimeFormatType.GmtFormat2 ,
                RTSPDateTimeFormatType.GmtFormat3 ,
            };

            var text = value.Trim() ?? string.Empty;

            foreach ( var format in formats )
            {
                if ( DateTime.TryParseExact( text , format , CultureInfo.InvariantCulture , DateTimeStyles.None , out DateTime result ) )
                {
                    return result;
                }
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static TimeSpan ConvertToTimeSpan( string value )
        {
            return TimeSpan.TryParse( value ?? string.Empty , out TimeSpan result ) ? result : TimeSpan.Zero;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static Guid ConvertToGuid( string value )
        {
            return Guid.TryParse( value ?? string.Empty , out Guid result ) ? result : Guid.Empty;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static TEnum ConvertToEnum<TEnum>( string value ) where TEnum : struct
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return default;
            }

            return Enum.TryParse<TEnum>( value.Trim() , true , out TEnum result ) ? result : default;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <typeparam name="TEnum">the type of enum</typeparam>
        /// <param name="values">the collection of values</param>
        /// <returns>returns a value</returns>
        public static IEnumerable<TEnum> ConvertToEnum<TEnum>( IEnumerable<string> values ) where TEnum : struct
        {
            if ( values == null )
            {
                yield break;
            }

            foreach ( var value in values )
            {
                yield return ConvertToEnum<TEnum>( value );
            }
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static byte[] ConvertToBytes( string value )
        {
            return ConvertToBytes( value , false );
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="isBase64">flag used to tell if the value is formated using base64 encoding</param>
        /// <returns>returns a value</returns>
        public static byte[] ConvertToBytes( string value , bool isBase64 )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return null;
            }

            try
            {
                if ( isBase64 )
                {
                    return Convert.FromBase64String( value );
                }

                return System.Text.Encoding.UTF8.GetBytes( value );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return null;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static byte[] ConvertToBytesUTF8( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return null;
            }

            try
            {
                return System.Text.Encoding.UTF8.GetBytes( value );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return null;
        }

        /// <summary>
        /// Convert a string in hex format to a byte array
        /// </summary>
        /// <param name="value">the input text</param>
        /// <returns>returns instance, otherwise null</returns>
        public static byte[] ConvertToBytesFromHex( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return null;

            }

            var text = value.Trim();

            if ( text.Length % 2 != 0 )
            {
                return null;
            }

            try
            {
                byte[] data = new byte[ text.Length / 2];

                for ( int index = 0 ; index < data.Length ; ++index )
                {
                    string byteValue = text.Substring( index * 2 , 2 );

                    if ( string.IsNullOrWhiteSpace( byteValue ) )
                    {
                        continue;
                    }

                    data[index] = byte.Parse( byteValue , NumberStyles.HexNumber , CultureInfo.InvariantCulture );
                }

                return data;
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return null;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToBase64( byte[] value )
        {
            if ( value == null || value.Length == 0 )
            {
                return string.Empty;
            }

            try
            {
                return Convert.ToBase64String( value ) ?? string.Empty;
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return string.Empty;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToBase64( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return string.Empty;
            }

            try
            {
                var buffer = Encoding.UTF8.GetBytes( value.Trim() );

                if ( buffer == null || buffer.Length == 0 )
                {
                    return string.Empty;
                }

                return Convert.ToBase64String( buffer ) ?? string.Empty;
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return string.Empty;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertFromBase64( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return string.Empty;
            }

            try
            {
                var buffer = Convert.FromBase64String( value.Trim() );

                if ( buffer == null || buffer.Length == 0 )
                {
                    return string.Empty;
                }

                return Encoding.UTF8.GetString( buffer ) ?? string.Empty;
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return string.Empty;
        }
    }
}

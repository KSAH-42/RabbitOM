using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a tolerant value converter
    /// </summary>
    public static class RTSPDataConverter
    {
        /// <summary>
        /// Check if a value can be converted
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool CanConvertToInteger(string value)
        {
            return int.TryParse(value ?? string.Empty, out int result) ;
        }

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
        public static string ConvertToStringUTF8( byte[] value )
        {
            if ( value == null || value.Length <= 0 )
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
        /// Convert to a string
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a string</returns>
        public static string ConvertToString(SecureString value)
        {
            if ( value == null || value.Length <= 0 )
            {
                return string.Empty;
            }

            IntPtr ptrString = IntPtr.Zero;

            try
            {
                ptrString = Marshal.SecureStringToBSTR(value);

                if (ptrString != IntPtr.Zero)
                {
                    return Marshal.PtrToStringBSTR( ptrString ) ?? string.Empty;
                }
            }
            finally
            {
                if (ptrString != IntPtr.Zero)
                {
                    Marshal.FreeBSTR( ptrString );

                    ptrString = IntPtr.Zero;
                }
            }

            return string.Empty;
        }


        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string ConvertToString<TEnum>(TEnum value) where TEnum : struct
        {
            return value.ToString();
        }

        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="streamingMode">the streaming mode</param>
        /// <returns>returns a string value</returns>
        public static string ConvertToString(RTSPTransmissionType streamingMode)
        {
            switch (streamingMode)
            {
                case RTSPTransmissionType.Unicast:
                    return RTSPHeaderFieldNames.Unicast;

                case RTSPTransmissionType.Multicast:
                    return RTSPHeaderFieldNames.Multicast;
            }

            return string.Empty;
        }

        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="transportType">the transport type</param>
        /// <returns>returns a string value</returns>
        public static string ConvertToString(RTSPTransportType transportType)
        {
            switch (transportType)
            {
                case RTSPTransportType.RTP_AVP_TCP:
                    return RTSPHeaderFieldNames.RtpAvpTcp;

                case RTSPTransportType.RTP_AVP_UDP:
                    return RTSPHeaderFieldNames.RtpAvp;
            }

            return string.Empty;
        }

        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="algorithmType">the algorithm type</param>
        /// <returns>returns a string value</returns>
        public static string ConvertToString(RTSPDigestAlgorithmType algorithmType)
        {
            switch (algorithmType)
            {
                case RTSPDigestAlgorithmType.MD5:
                    return "MD5";

                case RTSPDigestAlgorithmType.SHA_256:
                    return "SHA-256";

                case RTSPDigestAlgorithmType.SHA_512:
                    return "SHA-512";
            }

            return string.Empty;
        }

        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="method">the method type</param>
        /// <returns>returns a string value</returns>
        public static string ConvertToString(RTSPMethod method)
        {
            switch (method)
            {
                case RTSPMethod.Announce:
                    return RTSPMethodNames.ANNOUNCE;

                case RTSPMethod.Describe:
                    return RTSPMethodNames.DESCRIBE;

                case RTSPMethod.GetParameter:
                    return RTSPMethodNames.GET_PARAMETER;

                case RTSPMethod.Options:
                    return RTSPMethodNames.OPTIONS;

                case RTSPMethod.Pause:
                    return RTSPMethodNames.PAUSE;

                case RTSPMethod.Play:
                    return RTSPMethodNames.PLAY;

                case RTSPMethod.Record:
                    return RTSPMethodNames.RECORD;

                case RTSPMethod.Redirect:
                    return RTSPMethodNames.REDIRECT;

                case RTSPMethod.Setup:
                    return RTSPMethodNames.SETUP;

                case RTSPMethod.SetParameter:
                    return RTSPMethodNames.SET_PARAMETER;

                case RTSPMethod.TearDown:
                    return RTSPMethodNames.TEARDOWN;
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
            if ( value == null || value.Length <= 0 )
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

                if ( buffer == null || buffer.Length <= 0 )
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

                if ( buffer == null || buffer.Length <= 0 )
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

        /// <summary>
        /// Convert to a secure string
        /// </summary>
        /// <param name="value">the source</param>
        /// <returns>returns a secure string</returns>
        public static SecureString ConvertToSecureString(string value)
        {
            var result = new SecureString();

            foreach (var character in value ?? string.Empty)
            {
                result.AppendChar(character);
            }

            result.MakeReadOnly();

            return result;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static TEnum ConvertToEnum<TEnum>(string value) where TEnum : struct
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default;
            }

            return Enum.TryParse<TEnum>(value.Trim(), true, out TEnum result) ? result : default;
        }

        /// <summary>
        /// Convert a value
        /// </summary>
        /// <typeparam name="TEnum">the type of enum</typeparam>
        /// <param name="values">the collection of values</param>
        /// <returns>returns a value</returns>
        public static IEnumerable<TEnum> ConvertToEnum<TEnum>(IEnumerable<string> values) where TEnum : struct
        {
            if (values == null)
            {
                yield break;
            }

            foreach (var value in values)
            {
                yield return ConvertToEnum<TEnum>(value);
            }
        }

        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="streamingMode">the streaming mode</param>
        /// <returns>returns a string value</returns>
        public static RTSPTransmissionType ConvertToEnumTransmissionType(string streamingMode)
        {
            if (string.IsNullOrWhiteSpace(streamingMode))
            {
                return RTSPTransmissionType.Unknown;
            }

            var method = streamingMode.Trim();
            var ignoreCase = true;

            if (string.Compare(RTSPHeaderFieldNames.Unicast, method, ignoreCase) == 0)
            {
                return RTSPTransmissionType.Unicast;
            }

            if (string.Compare(RTSPHeaderFieldNames.Multicast, method, ignoreCase) == 0)
            {
                return RTSPTransmissionType.Multicast;
            }

            return RTSPTransmissionType.Unknown;
        }

        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="transportType">the transportType type</param>
        /// <returns>returns a string value</returns>
        public static RTSPTransportType ConvertToEnumTransportType(string transportType)
        {
            if (string.IsNullOrWhiteSpace(transportType))
            {
                return RTSPTransportType.Unknown;
            }

            var method = transportType.Trim();
            var ignoreCase = true;

            if (string.Compare(RTSPHeaderFieldNames.RtpAvp, method, ignoreCase) == 0)
            {
                return RTSPTransportType.RTP_AVP_UDP;
            }

            if (string.Compare(RTSPHeaderFieldNames.RtpAvpUdp, method, ignoreCase) == 0)
            {
                return RTSPTransportType.RTP_AVP_UDP;
            }

            if (string.Compare(RTSPHeaderFieldNames.RtpAvpTcp, method, ignoreCase) == 0)
            {
                return RTSPTransportType.RTP_AVP_TCP;
            }

            return RTSPTransportType.Unknown;
        }

        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="algorithmType">the algorithm type</param>
        /// <returns>returns a string value</returns>
        internal static RTSPDigestAlgorithmType ConvertToEnumDigestAlgorithmType(string algorithmType)
        {
            if (string.IsNullOrWhiteSpace(algorithmType))
            {
                return RTSPDigestAlgorithmType.UnDefined;
            }

            var method = algorithmType.Trim();
            var ignoreCase = true;

            if (string.Compare("MD5", method, ignoreCase) == 0)
            {
                return RTSPDigestAlgorithmType.MD5;
            }

            if (string.Compare("SHA-256", method, ignoreCase) == 0 ||
                 string.Compare("SHA256", method, ignoreCase) == 0
                )
            {
                return RTSPDigestAlgorithmType.SHA_256;
            }

            if (string.Compare("SHA-512", method, ignoreCase) == 0 ||
                 string.Compare("SHA512", method, ignoreCase) == 0
                )
            {
                return RTSPDigestAlgorithmType.SHA_512;
            }

            return RTSPDigestAlgorithmType.UnDefined;
        }

        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="methodName">the method name</param>
        /// <returns>returns a string value</returns>
        public static RTSPMethod ConvertToEnumMethod(string methodName)
        {
            return ConvertToEnumMethod(methodName, true);
        }

        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="methodName">the method name</param>
        /// <param name="ignoreCase">the ignore case</param>
        /// <returns>returns a string value</returns>
        public static RTSPMethod ConvertToEnumMethod(string methodName, bool ignoreCase)
        {
            if (string.IsNullOrWhiteSpace(methodName))
            {
                return RTSPMethod.UnDefined;
            }

            var method = methodName.Trim();

            if (string.Compare(RTSPMethodNames.ANNOUNCE, method, ignoreCase) == 0)
            {
                return RTSPMethod.Announce;
            }

            if (string.Compare(RTSPMethodNames.DESCRIBE, method, ignoreCase) == 0)
            {
                return RTSPMethod.Describe;
            }

            if (string.Compare(RTSPMethodNames.GET_PARAMETER, method, ignoreCase) == 0)
            {
                return RTSPMethod.GetParameter;
            }

            if (string.Compare(RTSPMethodNames.OPTIONS, method, ignoreCase) == 0)
            {
                return RTSPMethod.Options;
            }

            if (string.Compare(RTSPMethodNames.PAUSE, method, ignoreCase) == 0)
            {
                return RTSPMethod.Pause;
            }

            if (string.Compare(RTSPMethodNames.PLAY, method, ignoreCase) == 0)
            {
                return RTSPMethod.Play;
            }

            if (string.Compare(RTSPMethodNames.RECORD, method, ignoreCase) == 0)
            {
                return RTSPMethod.Record;
            }

            if (string.Compare(RTSPMethodNames.REDIRECT, method, ignoreCase) == 0)
            {
                return RTSPMethod.Redirect;
            }

            if (string.Compare(RTSPMethodNames.SETUP, method, ignoreCase) == 0)
            {
                return RTSPMethod.Setup;
            }

            if (string.Compare(RTSPMethodNames.SET_PARAMETER, method, ignoreCase) == 0)
            {
                return RTSPMethod.SetParameter;
            }

            if (string.Compare(RTSPMethodNames.TEARDOWN, method, ignoreCase) == 0)
            {
                return RTSPMethod.TearDown;
            }

            return RTSPMethod.UnDefined;
        }
    }
}

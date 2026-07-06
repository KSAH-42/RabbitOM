using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal static class RtspHeaderValueValidator
    {
        public static T EnsureNotNull<T>( T value ) where T : class
        {
            return ! object.ReferenceEquals( value , null ) ? value : throw new ArgumentNullException( nameof( value ) );
        }

        public static string EnsureNotNullOrEmpty( string value )
        {
            return ! string.IsNullOrEmpty( value ) ? value : throw new ArgumentException( nameof( value ) );
        }

        public static string EnsureWellFormed( string value )
        {
            return EnsureWellFormed( value , RtspHeaderValueValidatorCharSet.BasicToken );
        }

        public static string EnsureWellFormed( string value , RtspHeaderValueValidatorCharSet allowedChars )
        {
            if ( allowedChars == null )
            {
                throw new ArgumentNullException( nameof( allowedChars ) );
            }

            if ( string.IsNullOrEmpty( value ) )
            {
                return string.Empty;
            }

            for ( var i = 0 ; i < value.Length ; ++ i )
            {
                if ( ! allowedChars.Values.Contains( value[ i ] ) )
                {
                    throw new ArgumentException( nameof( value ) );
                }
            }

            return value;
        }

        public static bool IsWellFormed( string value )
        {
            return IsWellFormed( value , RtspHeaderValueValidatorCharSet.BasicToken );
        }

        public static bool IsWellFormed( string value , RtspHeaderValueValidatorCharSet allowedChars )
        {
            if ( allowedChars == null )
            {
                return false;
            }

            if ( string.IsNullOrEmpty( value ) )
            {
                return true;
            }
            
            for ( var i = 0 ; i < value.Length ; ++ i )
            {
                if ( ! allowedChars.Values.Contains( value[ i ] ) )
                {
                    return false;
                }
            }

            return true;
        }
    }
}

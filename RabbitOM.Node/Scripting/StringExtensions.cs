using System;

namespace RabbitOM.Node.Scripting
{
	public static class StringExtensions
	{
		public static bool ToBool( this string source )
		{
			return bool.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		public static char ToChar( this string source )
		{
			return char.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		public static sbyte ToSByte( this string source )
		{
			return sbyte.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		public static byte ToByte( this string source )
		{
			return byte.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		public static short ToShort( this string source )
		{
			return short.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		public static ushort ToUShort( this string source )
		{
			return ushort.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		public static int ToInt( this string source )
		{
			return int.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		public static uint ToUInt( this string source )
		{
			return uint.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		public static long ToLong( this string source )
		{
			return long.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		public static ulong ToULong( this string source )
		{
			return ulong.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		public static double ToDouble( this string source )
		{
			return double.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		public static float ToFloat( this string source )
		{
			return float.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		public static decimal ToDecimal( this string source )
		{
			return decimal.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		public static DateTime ToDateTime( this string source )
		{
			return DateTime.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		public static TimeSpan ToTimeSpan( this string source )
		{
			return TimeSpan.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		public static Guid ToGuid( this string source )
		{
			return Guid.TryParse( EnsureSource( source ) , out var result ) ? result : default;
		}

		private static string EnsureSource( string value )
		{
			return value ?? throw new ArgumentNullException( nameof( value ) );
		}
	}
}

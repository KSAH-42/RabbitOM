using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public static class KnownEncodingTypes
    {
        public const string Zip = "zip";
        public const string GZip = "gzip";
        public const string Tar = "tar";
        public const string Identity = "identity";
        public const string Deflate = "deflate";
        public const string BR = "br";
        public const string Any = "*";




        private static readonly object s_lock = new object();

        private static readonly Lazy<HashSet<string>> s_values = new Lazy<HashSet<string>>( () => 
        {
            return typeof(KnownEncodingTypes)
            .GetFields(System.Reflection.BindingFlags.Public|System.Reflection.BindingFlags.Static)
            .Where( field => field.FieldType == typeof( string ) )
            .Select( field => field.GetValue( null ) as string )
            .ToHashSet<string>( StringComparer.OrdinalIgnoreCase );
        });




        public static IReadOnlyCollection<string> Values { get => s_values.Value; }




        public static void AddEncoding( string value )
        {
            if ( value == null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            lock ( s_lock )
            {
                s_values.Value.Add( value );
            }
        }
        public static void RemoveEncoding( string value )
        {
            lock ( s_lock )
            {
                s_values.Value.Remove( value );
            }
        }

        public static void RemoveEncodings()
        {
            lock ( s_lock )
            {
                s_values.Value.Clear();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public static class KnownEncodingTypes
    {
        public const string ZipEncoding = "zip";
        public const string GZipEncoding = "gzip";
        public const string TarEncoding = "tar";
        public const string IdentityEncoding = "identity";
        public const string DeflateEncoding = "deflate";
        public const string BrEncoding = "br";
        public const string AnyEncoding = "*";





        private static readonly object s_lock = new object();

        private static readonly Lazy<HashSet<string>> s_values = new Lazy<HashSet<string>>( () => 
        {
            return typeof(KnownEncodingTypes)
            .GetFields(System.Reflection.BindingFlags.Public|System.Reflection.BindingFlags.Static)
            .Where( field => field.Name.EndsWith( "Encoding" ) )
            .Select( field => field.GetValue( null ) as string )
            .Where( value => ! string.IsNullOrWhiteSpace( value ) )
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

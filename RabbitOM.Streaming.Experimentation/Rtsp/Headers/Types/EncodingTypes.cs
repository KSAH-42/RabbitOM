using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public static class EncodingTypes
    {
        private static readonly object s_lock = new object();

        private static readonly Lazy<HashSet<string>> s_values = new Lazy<HashSet<string>>( () => new HashSet<string>( StringComparer.OrdinalIgnoreCase)
        {
            "zip",
            "tar",
            "gzip",
            "identity" ,
            "deflate" ,
            "br",
            "*",
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

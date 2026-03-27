using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public static class LanguageTypes
    {
        private static readonly object s_lock = new object();

        private static readonly Lazy<HashSet<string>> s_values = new Lazy<HashSet<string>>( () =>
        {
            var languages = CultureInfo.GetCultures( CultureTypes.AllCultures ).Select( culture => culture.Name );

            return new HashSet<string>( languages , StringComparer.OrdinalIgnoreCase );
        });







        public static IReadOnlyCollection<string> Values { get => s_values.Value; }

        





        public static void AddLanguage( string value )
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

        public static void RemoveLanguage( string value )
        {
            lock ( s_lock )
            {
                s_values.Value.Remove( value );
            }
        }

        public static void RemoveLanguages()
        {
            lock ( s_lock )
            {
                s_values.Value.Clear();
            }
        }
    }
}

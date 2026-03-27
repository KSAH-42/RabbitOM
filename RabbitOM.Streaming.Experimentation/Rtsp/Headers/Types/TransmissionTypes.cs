using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public static class TransmissionTypes
    {
        private static readonly object s_lock = new object();

        private static readonly Lazy<HashSet<string>> s_values = new Lazy<HashSet<string>>( () => new HashSet<string>( StringComparer.OrdinalIgnoreCase)
        {
            "unicast",
            "multicast",
        });






        public static IReadOnlyCollection<string> Values { get => s_values.Value; }






        public static void AddTransmission( string value )
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

        public static void RemoveTransmission( string value )
        {
            lock ( s_lock )
            {
                s_values.Value.Remove( value );
            }
        }

        public static void RemoveTransmissions()
        {
            lock ( s_lock )
            {
                s_values.Value.Clear();
            }
        }
    }
}

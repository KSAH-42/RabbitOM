using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public static class TransportTypes
    {
        private static readonly object s_lock = new object();

        private static readonly Lazy<HashSet<string>> s_values = new Lazy<HashSet<string>>( () => new HashSet<string>( StringComparer.OrdinalIgnoreCase)
        {
            "RTP",
            "RTP/AVP",
            "RTP/AVP/TCP",
            "RTP/AVP/UDP",
            "RTP/AVPF",
            "RTP/AVPF/TCP",
            "RTP/AVPF/UDP",
            "RTP/SAVP",
            "RTP/SAVP/TCP",
            "RTP/SAVP/UDP",
            "RTP/SAVPF",
            "RTP/SAVPF/TCP",
            "RTP/SAVPF/UDP",
            "SRTP",
            "SRTP/AVP",
            "SRTP/AVP/TCP",
            "SRTP/AVP/UDP",
            "SRTP/AVPF",
            "SRTP/AVPF/TCP",
            "SRTP/AVPF/UDP",
            "SRTP/SAVP",
            "SRTP/SAVP/TCP",
            "SRTP/SAVP/UDP",
            "SRTP/SAVPF",
            "SRTP/SAVPF/TCP",
            "SRTP/SAVPF/UDP",
        });





        public static IReadOnlyCollection<string> Values { get => s_values.Value; }





        public static void AddTransport( string value )
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

        public static void RemoveTransport( string value )
        {
            lock ( s_lock )
            {
                s_values.Value.Remove( value );
            }
        }

        public static void RemoveTransports()
        {
            lock ( s_lock )
            {
                s_values.Value.Clear();
            }
        }
    }
}

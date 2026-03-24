using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public static class SupportedTypes
    {
        private static readonly object s_lock = new object();

        private static readonly Lazy<HashSet<string>> s_encodings = new Lazy<HashSet<string>>( () => new HashSet<string>( StringComparer.OrdinalIgnoreCase)
        {
            "zip",
            "tar",
            "gzip",
            "identity" ,
            "deflate" ,
            "br",
            "*",
        });
                
        private static readonly Lazy<HashSet<string>> s_languages = new Lazy<HashSet<string>>( () =>
        {
            var languages = CultureInfo.GetCultures( CultureTypes.AllCultures ).Select( culture => culture.Name );

            return new HashSet<string>( languages , StringComparer.OrdinalIgnoreCase );
        });

        private static readonly Lazy<HashSet<string>> s_transmissions = new Lazy<HashSet<string>>( () => new HashSet<string>( StringComparer.OrdinalIgnoreCase)
        {
            "unicast",
            "multicast",
        });

        private static readonly Lazy<HashSet<string>> s_transports = new Lazy<HashSet<string>>( () => new HashSet<string>( StringComparer.OrdinalIgnoreCase)
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





        public static IReadOnlyCollection<string> Encodings { get => s_encodings.Value; }

        public static IReadOnlyCollection<string> Languages { get => s_languages.Value; }

        public static IReadOnlyCollection<string> Transmissions { get => s_transmissions.Value; }

        public static IReadOnlyCollection<string> Transports { get => s_transports.Value; }






        public static void AddEncoding( string value )
        {
            ThrowIfInvalid( value );

            lock ( s_lock )
            {
                s_encodings.Value.Add( value );
            }
        }

        public static void AddTransmission( string value )
        {
            ThrowIfInvalid( value );

            lock ( s_lock )
            {
                s_transmissions.Value.Add( value );
            }
        }

        public static void RemoveTransmission( string value )
        {
            lock ( s_lock )
            {
                s_transmissions.Value.Remove( value );
            }
        }

        public static void RemoveTransmissions()
        {
            lock ( s_lock )
            {
                s_transmissions.Value.Clear();
            }
        }

        public static void AddTransport( string value )
        {
            ThrowIfInvalid( value );

            lock ( s_lock )
            {
                s_transports.Value.Add( value );
            }
        }

        public static void RemoveTransport( string value )
        {
            lock ( s_lock )
            {
                s_transports.Value.Remove( value );
            }
        }

        public static void RemoveTransports()
        {
            lock ( s_lock )
            {
                s_transports.Value.Clear();
            }
        }

        public static void AddLanguage( string value )
        {
            ThrowIfInvalid( value );

            lock ( s_lock )
            {
                s_languages.Value.Add( value );
            }
        }

        public static void RemoveLanguage( string value )
        {
            lock ( s_lock )
            {
                s_languages.Value.Remove( value );
            }
        }

        public static void RemoveLanguages()
        {
            lock ( s_lock )
            {
                s_languages.Value.Clear();
            }
        }

        private static void ThrowIfInvalid( string value )
        {
            if ( value == null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }
        }
    }
}

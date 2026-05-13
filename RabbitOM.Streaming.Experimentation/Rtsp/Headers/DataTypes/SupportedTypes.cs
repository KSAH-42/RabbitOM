using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public static class SupportedTypes
    {
        private static readonly object s_lock = new object();

        private static readonly HashSet<string> s_mimes = new HashSet<string>( StringComparer.OrdinalIgnoreCase );

        private static readonly HashSet<string> s_languages = new HashSet<string>( StringComparer.OrdinalIgnoreCase );
        
        private static readonly HashSet<string> s_encodings = new HashSet<string>( StringComparer.OrdinalIgnoreCase );
        
        private static readonly HashSet<string> s_transmissions = new HashSet<string>( StringComparer.OrdinalIgnoreCase );
        
        private static readonly HashSet<string> s_transports = new HashSet<string>( StringComparer.OrdinalIgnoreCase );



        static SupportedTypes()
        {
            foreach ( var culture in CultureInfo.GetCultures( CultureTypes.AllCultures ) )
            {
                s_languages.Add( culture.Name );
            }

            s_mimes.Add( "text/sdp" );
            s_mimes.Add( "text/json" );
            s_mimes.Add( "text/xml" );
            s_mimes.Add( "application/sdp" );
            s_mimes.Add( "application/json" );
            s_mimes.Add( "application/x-mpegURL" );
            s_mimes.Add( "application/binary" );
            s_mimes.Add( "application/octet-stream" );
            s_mimes.Add( "application/zip" );
            s_mimes.Add( "image/jpeg" );
            s_mimes.Add( "image/bmp" );
            s_mimes.Add( "image/bitmap" );
            s_mimes.Add( "image/png" );
            s_mimes.Add( "video/jpeg" );
            s_mimes.Add( "video/mpeg" );
            s_mimes.Add( "video/mpeg-generic" );
            s_mimes.Add( "video/mpeg2" );
            s_mimes.Add( "video/mpeg2-generic" );
            s_mimes.Add( "video/mpeg4" );
            s_mimes.Add( "video/mpeg4-generic" );
            s_mimes.Add( "video/H264" );
            s_mimes.Add( "video/H265" );
            s_mimes.Add( "video/H266" );
            s_mimes.Add( "audio/wave" );
            s_mimes.Add( "audio/PCM" );
            s_mimes.Add( "audio/PCMA" );
            s_mimes.Add( "audio/MP3" );

            s_encodings.Add( "zip" );
            s_encodings.Add( "gzip" );
            s_encodings.Add( "tar" );
            s_encodings.Add( "identity" );
            s_encodings.Add( "deflate" );
            s_encodings.Add( "br" );
            s_encodings.Add( "base64" );
            s_encodings.Add( "*" );

            s_transmissions.Add( "unicast" );
            s_transmissions.Add( "multicast" );

            s_transports.Add( "RTP" );
            s_transports.Add( "RTP/AVP" );
            s_transports.Add( "RTP/AVP/TCP" );
            s_transports.Add( "RTP/AVP/UDP" );
            s_transports.Add( "RTP/AVPF" );
            s_transports.Add( "RTP/AVPF/TCP" );
            s_transports.Add( "RTP/AVPF/UDP" );
            s_transports.Add( "RTP/SAVP" );
            s_transports.Add( "RTP/SAVP/TCP" );
            s_transports.Add( "RTP/SAVP/UDP" );
            s_transports.Add( "RTP/SAVPF" );
            s_transports.Add( "RTP/SAVPF/TCP" );
            s_transports.Add( "RTP/SAVPF/UDP" );
            s_transports.Add( "SRTP" );
            s_transports.Add( "SRTP/AVP" );
            s_transports.Add( "SRTP/AVP/TCP" );
            s_transports.Add( "SRTP/AVP/UDP" );
            s_transports.Add( "SRTP/AVPF" );
            s_transports.Add( "SRTP/AVPF/TCP" );
            s_transports.Add( "SRTP/AVPF/UDP" );
            s_transports.Add( "SRTP/SAVP" );
            s_transports.Add( "SRTP/SAVP/TCP" );
            s_transports.Add( "SRTP/SAVP/UDP" );
            s_transports.Add( "SRTP/SAVPF" );
            s_transports.Add( "SRTP/SAVPF/TCP" );
            s_transports.Add( "SRTP/SAVPF/UDP" );
        }



        






        public static IReadOnlyCollection<string> Mimes
        {
            get
            {
                lock ( s_lock )
                {
                    return s_mimes.ToList();
                }
            }
        }
        
        public static IReadOnlyCollection<string> Languages
        {
            get
            {
                lock ( s_lock )
                {
                    return s_languages.ToList();
                }
            }
        }

        public static IReadOnlyCollection<string> Encodings
        {
            get
            {
                lock ( s_lock )
                {
                    return s_encodings.ToList();
                }
            }
        }

        public static IReadOnlyCollection<string> Transmissions
        {
            get
            {
                lock ( s_lock )
                {
                    return s_transmissions.ToList();
                }
            }
        }

        public static IReadOnlyCollection<string> Transports
        {
            get
            {
                lock ( s_lock )
                {
                    return s_transports.ToList();
                }
            }
        }









        public static bool IsMimeSupported( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            lock ( s_lock )
            {
                return s_mimes.Count == 0 || s_mimes.Contains( value );
            }
        }

        public static void AddMime( string value )
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
                s_mimes.Add( value );
            }
        }
        public static void RemoveMime( string value )
        {
            lock ( s_lock )
            {
                s_mimes.Remove( value );
            }
        }

        public static void RemoveMimes()
        {
            lock ( s_lock )
            {
                s_mimes.Clear();
            }
        }
        
        public static bool IsEncodingSupported( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            lock ( s_lock )
            {
                return s_encodings.Count == 0 || s_encodings.Contains( value );
            }
        }

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
                s_encodings.Add( value );
            }
        }
        public static void RemoveEncoding( string value )
        {
            lock ( s_lock )
            {
                s_encodings.Remove( value );
            }
        }

        public static void RemoveEncodings()
        {
            lock ( s_lock )
            {
                s_encodings.Clear();
            }
        }

        public static bool IsLanguageSupported( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            lock ( s_lock )
            {
                return s_languages.Count == 0 || s_languages.Contains( value );
            }
        }

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
                s_languages.Add( value );
            }
        }

        public static void RemoveLanguage( string value )
        {
            lock ( s_lock )
            {
                s_languages.Remove( value );
            }
        }

        public static void RemoveLanguages()
        {
            lock ( s_lock )
            {
                s_languages.Clear();
            }
        }

        public static bool IsTransmissionSupported( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            lock ( s_lock )
            {
                return s_transmissions.Count == 0 || s_transmissions.Contains( value );
            }
        }

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
                s_transmissions.Add( value );
            }
        }

        public static void RemoveTransmission( string value )
        {
            lock ( s_lock )
            {
                s_transmissions.Remove( value );
            }
        }

        public static void RemoveTransmissions()
        {
            lock ( s_lock )
            {
                s_transmissions.Clear();
            }
        }

        public static bool IsTransportSupported( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            lock ( s_lock )
            {
                return s_transports.Count == 0 || s_transports.Contains( value );
            }
        }

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
                s_transports.Add( value );
            }
        }

        public static void RemoveTransport( string value )
        {
            lock ( s_lock )
            {
                s_transports.Remove( value );
            }
        }

        public static void RemoveTransports()
        {
            lock ( s_lock )
            {
                s_transports.Clear();
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class AcceptLanguageRtspHeader
    {
        public static readonly string TypeName = "Accept-Language";




        private readonly HashSet<StringRtspHeader> _languages = new HashSet<StringRtspHeader>();




        public IReadOnlyCollection<StringRtspHeader> Languages
        {
            get => _languages;
        }




        public static bool TryParse( string input , out AcceptLanguageRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( input ) , ',' , out var tokens ) )
            {
                var header = new AcceptLanguageRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( StringRtspHeader.TryParse( token , out var language ) )
                    {
                        header.AddLanguage( language );
                    }
                }
            
                if ( header.Languages.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }




        public bool AddLanguage( StringRtspHeader language )
        {
            if ( StringRtspHeader.IsNullOrEmpty( language ) )
            {
                return false;
            }

            return _languages.Add( language );
        }

        public bool RemoveLanguage( StringRtspHeader encoding )
        {
            return _languages.Remove( encoding );
        }

        public void RemoveLanguages()
        {
            _languages.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _languages );
        }
    }
}

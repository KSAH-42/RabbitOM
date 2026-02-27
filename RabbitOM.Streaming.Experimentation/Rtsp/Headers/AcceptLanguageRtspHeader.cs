using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class AcceptLanguageRtspHeader
    {
        public static readonly string TypeName = "Accept-Language";




        public readonly Lazy<IReadOnlyCollection<string>> SupportedLanguages = new Lazy<IReadOnlyCollection<string>>( () =>
        {
            return new HashSet<string>( CultureInfo.GetCultures( CultureTypes.AllCultures ).Select( culture => culture.Name ) , StringComparer.OrdinalIgnoreCase );
        });




        
        private readonly HashSet<StringWithQuality> _languages = new HashSet<StringWithQuality>();






        public IReadOnlyCollection<StringWithQuality> Languages
        {
            get => _languages;
        }




        public bool AddLanguage( StringWithQuality language )
        {
            if ( StringWithQuality.IsNullOrEmpty( language ) )
            {
                return false;
            }

            if ( SupportedLanguages.Value.Contains( language.Name ) )
            {
                return _languages.Add( language );
            }

            return false;
        }

        public bool RemoveLanguage( StringWithQuality language )
        {
            return _languages.Remove( language );
        }

        public void RemoveLanguages()
        {
            _languages.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _languages );
        }

        
        
        
        
        
        
        public static bool TryParse( string input , out AcceptLanguageRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                var header = new AcceptLanguageRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( StringWithQuality.TryParse( token , out var language ) )
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
    }
}

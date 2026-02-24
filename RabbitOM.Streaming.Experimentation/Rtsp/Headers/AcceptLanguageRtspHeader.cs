using System;
using System.Collections.Generic;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
    
    public sealed class AcceptLanguageRtspHeader
    {
        public static readonly string TypeName = "Accept-Language";





        
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

            foreach ( var culture in CultureInfo.GetCultures( CultureTypes.AllCultures ) )
            {
                if ( string.Equals( culture.Name , language.Name , StringComparison.OrdinalIgnoreCase ) )
                {
                    return _languages.Add( language );
                }
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

            if ( RtspHeaderParser.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , "," , out var tokens ) )
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

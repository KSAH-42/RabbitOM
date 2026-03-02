using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    public sealed class AcceptLanguageRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Accept-Language";




        




        
        private readonly HashSet<WeightedString> _languages = new HashSet<WeightedString>();






        public IReadOnlyCollection<WeightedString> Languages
        {
            get => _languages;
        }




        public bool AddLanguage( WeightedString language )
        {
            if ( WeightedString.IsNullOrEmpty( language ) )
            {
                return false;
            }

            if ( Constants.CurrentLanguages.Contains( language.Value ) )
            {
                return _languages.Add( language );
            }

            return false;
        }

        public bool RemoveLanguage( WeightedString language )
        {
            return _languages.Remove( language );
        }

        public void ClearLanguages()
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

            if ( RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , "," , out var tokens ) )
            {
                var header = new AcceptLanguageRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( WeightedString.TryParse( token , out var language ) )
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

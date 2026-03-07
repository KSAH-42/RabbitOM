using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class AcceptLanguageRtspHeader
    {
        public static readonly string TypeName = "Accept-Language";





        private readonly Dictionary<string,WeightedString> _languages = new Dictionary<string,WeightedString>( StringComparer.OrdinalIgnoreCase );
        



        

        public IReadOnlyCollection<WeightedString> Languages { get => _languages.Values; }
        


        
        
        public bool AddLanguage( WeightedString language )
        {
            if ( WeightedString.IsNullOrEmpty( language ) )
            {
                return false;
            }

            if ( ! SupportedTypes.Languages.Contains( language.Value ) )
            {
                return false;
            }

            if ( _languages.ContainsKey( language.Value ) )
            {
                return false;
            }

            _languages[ language.Value ] = language;

            return true;
        }

        public bool RemoveLanguage( WeightedString language )
        {
            if ( WeightedString.IsNullOrEmpty( language ) )
            {
                return false;
            }

            if ( ! _languages.ContainsValue( language ) )
            {
                return false;
            }

            return _languages.Remove( language.Value );
        }

        public bool RemoveLanguageBy( Func<WeightedString,bool> predicate )
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

            var language = _languages.Values.FirstOrDefault( predicate );

            if ( language == null )
            {
                return false;
            }

            return _languages.Remove( language.Value );
        }

        public void ClearLanguages()
        {
            _languages.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _languages.Values );
        }
        



        
        
        public static bool TryParse( string input , out AcceptLanguageRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , "," , out var tokens ) )
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

using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class AcceptLanguageRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Accept-Language";


        private readonly Dictionary<string,StringWithQualityRtspHeaderValue> _languages = new Dictionary<string,StringWithQualityRtspHeaderValue>( StringComparer.OrdinalIgnoreCase );
        

        public IReadOnlyCollection<StringWithQualityRtspHeaderValue> Languages 
        { 
            get => _languages.Values; 
        }
        

        public static bool TryParse( string input , out AcceptLanguageRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AcceptLanguageRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( StringWithQualityRtspHeaderValue.TryParse( token , out var language ) )
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
        
        
        public bool AddLanguage( StringWithQualityRtspHeaderValue language )
        {
            return AddLanguage( language , RtspHeaderProtocolValidator.IsValidLanguage );
        }

        public bool AddLanguage( StringWithQualityRtspHeaderValue language , Func<string,bool> validator )
        {
            if ( StringWithQualityRtspHeaderValue.IsNullOrEmpty( language ) )
            {
                return false;
            }

            if ( validator == null || ! validator.Invoke( language.Value ) )
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

        public bool RemoveLanguage( StringWithQualityRtspHeaderValue language )
        {
            if ( StringWithQualityRtspHeaderValue.IsNullOrEmpty( language ) || ! _languages.ContainsValue( language ) )
            {
                return false;
            }

            return _languages.Remove( language.Value );
        }

        public bool RemoveLanguageBy( Func<StringWithQualityRtspHeaderValue,bool> predicate )
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

            return _languages.Remove( _languages.Values.FirstOrDefault( predicate )?.Value ?? string.Empty );
        }

        public void RemoveLanguages()
        {
            _languages.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _languages.Select( element => element.Value.ToString() ) );
        }
    }
}

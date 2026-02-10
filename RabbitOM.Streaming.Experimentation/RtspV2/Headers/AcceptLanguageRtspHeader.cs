using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class AcceptLanguageRtspHeader : RtspHeader 
    {
        public const string TypeName = "Accept-Language";

        private readonly List<StringWithQualityRtspHeaderValue> _languages = new List<StringWithQualityRtspHeaderValue>();

        public IReadOnlyList<StringWithQualityRtspHeaderValue> Languages
        {
            get => _languages;
        }

        public override bool TryValidate()
        {
            return _languages.Count > 0;
        }

        public bool TryAddLanguage( StringWithQualityRtspHeaderValue value )
        {
            if ( StringWithQualityRtspHeaderValue.IsNullOrEmpty( value ) )
            {
                return false;
            }

            _languages.Add( value );

            return true;
        }

        public void RemoveLanguage( StringWithQualityRtspHeaderValue value )
        {
            _languages.Remove( value );
        }

        public void RemoveAllLanguages()
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

            if ( ! RtspHeaderParser.TryParse( input , "," , out var tokens ) )
            {
                return false;
            }

            var header = new AcceptLanguageRtspHeader();

            foreach ( var token in tokens )
            {
                if ( StringWithQualityRtspHeaderValue.TryParse( token , out var encoding ) )
                {
                    header.TryAddLanguage( encoding );
                }
            }
            
            if ( header.Languages.Count <= 0 )
            {
                return false;
            }

            result = header;

            return true;
        }
    } 
}

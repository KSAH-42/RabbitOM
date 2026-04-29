using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Compliances
{
    public sealed class DefaultHeaderValidator : StringValueValidator
    {
        private readonly DefaultHeaderValidatorSettings _settings;



        internal DefaultHeaderValidator( DefaultHeaderValidatorSettings settings )
        {
            _settings = settings ?? throw new ArgumentNullException( nameof( settings ) );
        }

        

        // making a prevalidation before doing any thing 
        // to avoid sending illegals chars to the server
        // or when value are applying
    
        public override bool TryValidate( string text )
        {
            var input = text ?? string.Empty;

            foreach ( var character in input )
            {
                if ( character <= 31 || character >= 127 || char.IsControl( character ) || InvalidChars.Contains( character ) )
                {
                    return false;
                }
            }

            if ( ! _settings.AcceptEmptyOrWhiteSpace && string.IsNullOrWhiteSpace( input ) )
            {
                return false;
            }

            if ( _settings.OnValidate?.Invoke( input ) == false )
            {
                return false;
            }

            return true;
        }
    }
}

using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation
{
    public sealed class UriValueValidator : StringValueValidator
    {
        public override bool TryValidate( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return Uri.IsWellFormedUriString( value , UriKind.RelativeOrAbsolute );
        }
    }
}

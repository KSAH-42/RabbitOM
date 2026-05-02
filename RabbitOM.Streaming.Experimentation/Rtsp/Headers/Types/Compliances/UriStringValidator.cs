using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances
{
    public sealed class UriStringValidator : StringValueValidator
    {
        internal UriStringValidator() { }

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

using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Compliances
{
    public sealed class UriRtspHeaderValidator : StringValueValidator
    {
        internal UriRtspHeaderValidator() { }

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

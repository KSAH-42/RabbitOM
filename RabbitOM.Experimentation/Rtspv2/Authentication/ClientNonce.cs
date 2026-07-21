using System;
using System.Text;

namespace RabbitOM.Streaming.RtspV2.Authentication
{
    // Apply the output of this class to <seealso cref="RtspAuthorizationResponseBuilder.ClientNonce"/> property
    public sealed class ClientNonce
    {
        public byte Length { get; set; } = 16;

        public override string ToString()
        {
            var builder = new StringBuilder();
            var random = new Random();

            for ( var i = 0 ; i < Length ; i++ )
            {
                builder.AppendFormat( "{0:x2}" , random.Next( 0 , 256 ) );
            }

            return builder.ToString();
        }
    }
}

using System;
using System.Text;

namespace RabbitOM.Net.RtspV2.Authentication
{
    public sealed class BasicRtspAuthorizationResponseBuilder
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( UserName ) || string.IsNullOrWhiteSpace( Password ) )
            {
                return string.Empty;
            }

            return Convert.ToBase64String( Encoding.UTF8.GetBytes( $"{UserName}:{Password}" ) );
        }
    }
}

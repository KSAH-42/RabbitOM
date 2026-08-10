using System;
using System.Configuration;

namespace RabbitOM.Player.Configuration
{
    public sealed class RtspSourceConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("uri", IsRequired = true)]
        public string Uri
        {
            get => this[ "uri" ] as string;
            set => this[ "uri" ] = value;
        }

        public static implicit operator RtspSourceConfigurationElement( string uri )
        {
            return new RtspSourceConfigurationElement() { Uri = uri };
        }
    }
}

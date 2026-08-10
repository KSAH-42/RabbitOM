using System;
using System.Configuration;

namespace RabbitOM.Player.Configuration
{
    public sealed class RtspConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("sources")]
        public RtspSourceConfigurationElementCollection Sources
        {
            get => this[ "sources" ] as RtspSourceConfigurationElementCollection;
        }
    }
}

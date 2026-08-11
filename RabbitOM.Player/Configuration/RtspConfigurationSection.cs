using System;
using System.Configuration;
using System.Diagnostics;

namespace RabbitOM.Player.Configuration
{
    public sealed class RtspConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("create_sources_if_empty" , DefaultValue = true)]
        public bool CreateSourcesIfEmpty
        {
            get => (bool) this[ "create_sources_if_empty" ];
            set => this[ "create_sources_if_empty" ] = value;
        }


        [ConfigurationProperty("sources")]
        public RtspSourceConfigurationElementCollection Sources
        {
            get => this[ "sources" ] as RtspSourceConfigurationElementCollection;
        }
    }
}

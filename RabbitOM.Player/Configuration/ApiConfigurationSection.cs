using System;
using System.Configuration;

namespace RabbitOM.Player.Configuration
{
    public sealed class ApiConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("api_key" )]
        public string ApiKey
        {
            get => (string) this[ "api_key" ];
            set => this[ "api_key" ] = value;
        }
    }
}

using System;
using System.Configuration;

namespace RabbitOM.Player.Configuration
{
    public static class ConfigurationExtensions
    {
        public static TConfigurationSection GetSectionOrDefault<TConfigurationSection>( this System.Configuration.Configuration configuration , string name ) where TConfigurationSection : ConfigurationSection , new ()
        {
            if ( configuration == null )
            {
                throw new ArgumentNullException( nameof( configuration ) );
            }

            var section = configuration.GetSection( name ) as TConfigurationSection;

            if ( section == null )
            {
                section = new TConfigurationSection();
                configuration.Sections.Add( name , section );
            }

            return section;
        }
    }
}

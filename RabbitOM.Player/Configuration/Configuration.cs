using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;

namespace RabbitOM.Player.Configuration
{
    public sealed class Configuration : IConfiguration
    {
        private readonly RtspConfigurationSection _rtspSection;

        public Configuration( RtspConfigurationSection rtspSection )
        {
            _rtspSection = rtspSection ?? throw new ArgumentNullException( nameof( rtspSection ) );
        }

        public static Configuration Load()
        {
            var configuration = ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.None );

            return new Configuration( configuration.GetSection( "rtsp" ) as RtspConfigurationSection ?? new RtspConfigurationSection() );
        }

        public IReadOnlyCollection<RtspSourceConfigurationElement> GetSources()
        {
            var sources = _rtspSection.Sources ?? new RtspSourceConfigurationElementCollection();

            return sources.Cast<RtspSourceConfigurationElement>().ToList();
        }

        public IReadOnlyCollection<RtspSourceConfigurationElement> GetSourcesOrDefault()
        {
            var sources = _rtspSection.Sources ?? new RtspSourceConfigurationElementCollection();

            if ( sources.Count == 0 && _rtspSection.CreateSourcesIfEmpty )
            {
                sources.AddRange( RtspSourceConfigurationElementFactory.CreateDefaultSources() );
            }

            return sources.Cast<RtspSourceConfigurationElement>().ToList();
        }
    }
}

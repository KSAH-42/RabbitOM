using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;

namespace RabbitOM.Player.Configuration
{
    public sealed class ApplicationConfiguration : IConfiguration
    {
        private readonly RtspConfigurationSection _rtspSection;

        public ApplicationConfiguration( RtspConfigurationSection rtspSection )
        {
            _rtspSection = rtspSection ?? throw new ArgumentNullException( nameof( rtspSection ) );
        }

        public static ApplicationConfiguration Load()
        {
            var configuration = ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.None );

            return new ApplicationConfiguration( configuration.GetSection( "rtsp" ) as RtspConfigurationSection ?? new RtspConfigurationSection() );
        }

        public IReadOnlyCollection<RtspSourceConfigurationElement> GetSources()
        {
            var sources = _rtspSection.Sources ?? new RtspSourceConfigurationElementCollection();

            return sources.Cast<RtspSourceConfigurationElement>().ToList();
        }

        public IReadOnlyCollection<RtspSourceConfigurationElement> GetSourcesOrDefault()
        {
            var sources = _rtspSection.Sources ?? new RtspSourceConfigurationElementCollection();

            if ( sources.Count == 0 && _rtspSection.CreateSourceIfEmpty )
            {
                sources.AddRange( RtspSourceConfigurationElementFactory.CreateDefaultSources() );
            }

            return sources.Cast<RtspSourceConfigurationElement>().ToList();
        }
    }
}

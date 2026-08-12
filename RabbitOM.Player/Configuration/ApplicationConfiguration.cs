using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;

namespace RabbitOM.Player.Configuration
{
    using SystemConfiguration = System.Configuration.Configuration;

    // IConfiguration has been remove due to the coupling of the ABCBlablaConfigurationElement, is not poco class, if it's need IConfiguration should be added but at the higher level not at this level
    public sealed class ApplicationConfiguration
    {
        private readonly SystemConfiguration _configuration = ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.None );

        public void AddSources( IEnumerable<RtspSourceConfigurationElement> elements )
        {
            if ( elements == null )
            {
                throw new ArgumentNullException( nameof( elements ) );
            }

            var section = _configuration.GetSectionOrDefault<RtspConfigurationSection>( "rtsp" );
            var sources = section.EnsureSourcesExists();

            sources.AddRange( elements );
        }

        public IReadOnlyCollection<RtspSourceConfigurationElement> GetSources()
        {
            var section = _configuration.GetSectionOrDefault<RtspConfigurationSection>( "rtsp" );
            var sources = section.EnsureSourcesExists();

            return sources.Cast<RtspSourceConfigurationElement>().ToList();
        }

        public IReadOnlyCollection<RtspSourceConfigurationElement> GetSourcesOrDefault()
        {
            var section = _configuration.GetSectionOrDefault<RtspConfigurationSection>( "rtsp" );
            var sources = section.EnsureSourcesExists();

            if ( section.CreateSourcesIfEmpty && sources.Count == 0 )
            {
                sources.AddRange( RtspSourceConfigurationElementFactory.CreateDefaultSources() );
            }

            return sources.Cast<RtspSourceConfigurationElement>().ToList();
        }

        public void ClearSources()
        {
            var section = _configuration.GetSectionOrDefault<RtspConfigurationSection>( "rtsp" );
            var sources = section.EnsureSourcesExists();

            sources.Clear();
        }

        public void Save()
        {
            _configuration.Save();
        }
    }
}

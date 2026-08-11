using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;

namespace RabbitOM.Player.Configuration
{
    using SystemConfiguration = System.Configuration.Configuration;

    public sealed class ApplicationConfiguration : IConfiguration
    {
        private readonly SystemConfiguration _configuration = ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.None );
        
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

        public void SaveSources( IEnumerable<RtspSourceConfigurationElement> elements )
        {
            if ( elements == null )
            {
                throw new ArgumentNullException( nameof( elements ) );
            }

            var section = _configuration.GetSectionOrDefault<RtspConfigurationSection>( "rtsp" );
            var sources = section.EnsureSourcesExists();

            sources.Clear();
            sources.AddRange( elements );

            _configuration.Save();
        }
    }
}

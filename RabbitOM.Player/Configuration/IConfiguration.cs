using System;
using System.Collections.Generic;

namespace RabbitOM.Player.Configuration
{
    public interface IConfiguration
    {
        void AddSources( IEnumerable<RtspSourceConfigurationElement> elements );

        IReadOnlyCollection<RtspSourceConfigurationElement> GetSources();

        IReadOnlyCollection<RtspSourceConfigurationElement> GetSourcesOrDefault();

        void ClearSources();

        void Save();
    }
}

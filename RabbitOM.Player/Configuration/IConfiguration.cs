using System;
using System.Collections.Generic;

namespace RabbitOM.Player.Configuration
{
    public interface IConfiguration
    {
        IReadOnlyCollection<RtspSourceConfigurationElement> GetSources();

        IReadOnlyCollection<RtspSourceConfigurationElement> GetSourcesOrDefault();
    }
}

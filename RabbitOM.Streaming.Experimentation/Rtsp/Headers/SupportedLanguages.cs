using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class SupportedLanguages
    {
        private static Lazy<IReadOnlyCollection<string>> s_value = new Lazy<IReadOnlyCollection<string>>( () =>
        {
            var languages = CultureInfo.GetCultures( CultureTypes.AllCultures ).Select( culture => culture.Name );

            return new HashSet<string>( languages , StringComparer.OrdinalIgnoreCase );
        });


        public static IReadOnlyCollection<string> Values { get => s_value.Value; }
    }
}

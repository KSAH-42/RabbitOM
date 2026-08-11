using System;
using System.Collections.Generic;
using System.Configuration;

namespace RabbitOM.Player.Configuration
{
    [ConfigurationCollection(typeof( RtspSourceConfigurationElement ) , AddItemName = "source" )]
    public sealed class RtspSourceConfigurationElementCollection : ConfigurationElementCollection
    {
        public void Add( RtspSourceConfigurationElement element )
        {
            BaseAdd( element ?? throw new ArgumentNullException( nameof( element ) ) );
        }

        public void AddRange( IEnumerable<RtspSourceConfigurationElement> elements )
        {
            if ( elements == null )
            {
                throw new ArgumentNullException( nameof( elements ) );
            }

            foreach ( var element in elements )
            {
                Add( element );
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RtspSourceConfigurationElement();
        }

        protected override object GetElementKey( ConfigurationElement element )
        {
            if ( element == null )
            {
                throw new ArgumentNullException( nameof( element ) );
            }

            var rtspSourceConfig = element as RtspSourceConfigurationElement;

            if ( rtspSourceConfig == null )
            {
                throw new ArgumentNullException( nameof( element ) , "invalid type" );
            }

            return rtspSourceConfig.Uri;
        }

        public void Clear()
        {
            BaseClear();
        }
    }
}

using System;
using System.Windows.Controls;

namespace RabbitOM.Streaming.Tests.Mjpeg.Extensions
{
    public static class ControlExtensions
    {
        public static bool Any( this ItemCollection source , string text )
        {
            if ( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            foreach ( var item in source )
            {
                ComboBoxItem element = item as ComboBoxItem;

                if ( element == null )
                {
                    continue;
                }

                if ( string.Compare( element.Content as string ?? string.Empty , text ?? string.Empty , true ) == 0 )
                {
                    return true;
                }
            }

            return false;
        }
    }
}

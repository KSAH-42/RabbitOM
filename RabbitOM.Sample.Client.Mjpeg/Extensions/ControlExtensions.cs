using System;
using System.Windows.Controls;

namespace RabbitOM.Sample.Client.Mjpeg.Extensions
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
                string element = string.Empty;

                if ( item is string )
                {
                    element = item as string;
                }

                if ( item is ComboBoxItem )
                {
                    element = (item as ComboBoxItem).Content as string;
                }

                if ( string.Compare( element as string ?? string.Empty , text ?? string.Empty , true ) == 0 )
                {
                    return true;
                }
            }

            return false;
        }
    }
}

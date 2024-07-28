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
                string element = item as string;

                if ( string.Compare( element ?? string.Empty , text ?? string.Empty , true ) == 1 )
                {
                    return true;
                }
            }

            return false;
        }
    }
}

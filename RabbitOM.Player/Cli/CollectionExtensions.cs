using System;
using System.Windows;

namespace RabbitOM.Player.Cli
{
    public static class CollectionExtensions
    {
        public static TWindow FirstOrDefault<TWindow>( this WindowCollection source ) where TWindow : Window
        {
            if ( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            foreach ( var window in source )
            {
                if ( window is TWindow targetWindow )
                {
                    return targetWindow;
                }
            }

            return null;
        }
    }
}

using System;
using System.Windows.Media;
using System.Windows.Controls;

namespace RabbitOM.Sample.Player.Extensions
{
    public static class ImageExtensions
    {
        public static void ConfigureSource( this Image image , ImageSource source )
        {
            if ( image == null )
            {
                throw new ArgumentNullException( nameof( image ) );
            }

            image.BeginInit();

            try
            {
                image.Source = source;

                RenderOptions.SetCachingHint( image , CachingHint.Cache );
                RenderOptions.SetBitmapScalingMode( image , BitmapScalingMode.HighQuality );
                RenderOptions.SetEdgeMode( image , EdgeMode.Aliased );
            }
            finally
            {
                image.EndInit();
            }
        }
    }
}

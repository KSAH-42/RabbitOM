using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace RabbitOM.Sample.Client.H265.Codecs
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
                RenderOptions.SetBitmapScalingMode( image , BitmapScalingMode.NearestNeighbor );
                RenderOptions.SetEdgeMode( image , EdgeMode.Aliased );
            }
            finally
            {
                image.EndInit();
            }
        }
    }
}

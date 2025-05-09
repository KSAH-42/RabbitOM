using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows;

namespace RabbitOM.Streaming.Tests.Mjpeg.Drawing
{
    public sealed class Screen
    {
        Screen()
        {
            DpiX = GetDpiPropertyValue( nameof( DpiX ) );
            DpiY = GetDpiPropertyValue( nameof( DpiY ) );
        }


        public static Screen Current { get; } = new Screen();
        public int DpiX { get; }
        public int DpiY { get; }


        public static int GetDpiPropertyValue( string propertyName )
        {
            if ( string.IsNullOrWhiteSpace( propertyName ) )
            {
                throw new ArgumentNullException( nameof( propertyName ) );
            }

            var propertyInfo = typeof(SystemParameters).GetProperty( propertyName , BindingFlags.NonPublic | BindingFlags.Static);

            if ( propertyInfo == null )
            {
                return 0;
            }

            return (int) propertyInfo.GetValue( null );
        }
    }
}

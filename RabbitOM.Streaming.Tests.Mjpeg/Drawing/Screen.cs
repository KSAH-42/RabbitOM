using System;
using System.Windows;
using System.Reflection;

namespace RabbitOM.Streaming.Tests.Mjpeg.Drawing
{
    public sealed class Screen
    {
        Screen()
        {
            DpiX = GetDpiPropertyValue( "Dpi" );
            DpiY = GetDpiPropertyValue( "Dpi" );
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
                throw new ArgumentException( "Property not found" , nameof( propertyName ) );
            }

            return (int) propertyInfo.GetValue( null );
        }
    }
}

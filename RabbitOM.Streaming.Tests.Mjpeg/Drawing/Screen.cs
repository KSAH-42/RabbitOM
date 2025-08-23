using System;
using System.Drawing;

namespace RabbitOM.Streaming.Tests.Mjpeg.Drawing
{
    public sealed class Screen
    {
        Screen()
        {
            using (var graphics = Graphics.FromHwnd(IntPtr.Zero) )
            {
                DpiX = Convert.ToInt32( graphics.DpiX );
                DpiY = Convert.ToInt32( graphics.DpiY );
            }
        }

        public int DpiX { get; }
        public int DpiY { get; }
        
        public static Screen Current { get; } = new Screen();
    }
}

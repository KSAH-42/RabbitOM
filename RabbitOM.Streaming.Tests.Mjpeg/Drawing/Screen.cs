using System;
using System.Drawing;

namespace RabbitOM.Streaming.Tests.Mjpeg.Drawing
{
    public sealed class Screen
    {
        Screen()
        {
            var graphics = Graphics.FromHwnd(IntPtr.Zero);

            DpiX = Convert.ToInt32( graphics.DpiX );
            DpiY = Convert.ToInt32( graphics.DpiY );
        }

        public static Screen Current { get; } = new Screen();
        public int DpiX { get; }
        public int DpiY { get; }
    }
}

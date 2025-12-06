using System;
using System.Drawing;

namespace RabbitOM.Streaming.Windows.Presentation
{
    /// <summary>
    /// Represent the screen informations class
    /// </summary>
    public sealed class Screen
    {
        /// <summary>
        /// Initialize a new instance of screen class
        /// </summary>
        Screen()
        {
            using (var graphics = Graphics.FromHwnd(IntPtr.Zero) )
            {
                DpiX = Convert.ToInt32( graphics.DpiX );
                DpiY = Convert.ToInt32( graphics.DpiY );
            }
        }
        




        /// <summary>
        /// Gets the current instance
        /// </summary>
        public static Screen Current { get; } = new Screen();





        /// <summary>
        /// Gets the number of pixel per inch (unit)
        /// </summary>
        public int DpiX { get; }

        /// <summary>
        /// Gets the number of pixel per inch (unit)
        /// </summary>
        public int DpiY { get; }
    }
}

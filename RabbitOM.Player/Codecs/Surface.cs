using FFmpeg.AutoGen;
using System;

namespace RabbitOM.Player.Codecs
{
    public abstract class Surface : IDisposable
    {
        protected Surface( int width , int height )
        {
            Width = width;
            Height = height;
        }

        ~Surface()
        {
            Dispose( false );
        }

        public int Width { get; }

        public int Height { get; }

        public abstract IntPtr Frame { get; }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {

        }
    }
}
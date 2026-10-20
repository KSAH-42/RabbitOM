using System;

namespace RabbitOM.Decoder.FFMpeg.Codecs
{
    public abstract class VideoDecoder : IDisposable
    {
        public event EventHandler<FrameDecodedEventArgs>? FrameDecoded;

        ~VideoDecoder()
        {
            Dispose( false );
        }

        public abstract bool IsOpened { get; }

        public abstract void Open();

        public abstract void Close();

        public abstract bool CanConfigure( byte[] extraParameters );

        public abstract bool Configure( byte[] extraParameters );

        public abstract void Decode( byte[] buffer );

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            Close();
        }

        protected virtual void OnDecoded( FrameDecodedEventArgs e )
        {
            FrameDecoded?.Invoke( this , e );
        }
    }
}
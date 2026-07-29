using System;

namespace RabbitOM.Sample.Client.H265.Codecs.FFMpeg
{
    public abstract class H265Decoder : IDisposable
    {
        public event EventHandler<H265DecodedEventArgs> Decoded;






        ~H265Decoder()
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
            if ( disposing )
            {
                Close();
            }
        }







        protected virtual void OnDecoded( H265DecodedEventArgs e )
        {
            Decoded?.Invoke( this , e );
        }
    }
}
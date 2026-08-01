using System;

namespace RabbitOM.Sample.Player.Codecs
{
    public abstract class Decoder : IDisposable
    {
        public event EventHandler<DecodedEventArgs> Decoded;






        ~Decoder()
        {
            Dispose( false );
        }





        public abstract bool IsOpened { get; }





        public abstract void Open( CodecType type );

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







        protected virtual void OnDecoded( DecodedEventArgs e )
        {
            Decoded?.TryInvoke( this , e );
        }
    }
}
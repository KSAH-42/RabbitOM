using System;

namespace RabbitOM.Streaming.Rtp
{
    public sealed class RtpMediaBuilderAdapter : IMediaBuilder , IDisposable
    {
        public event EventHandler<RtpPacketAddingEventArgs> PacketAdding;

        public event EventHandler<RtpPacketAddedEventArgs> PacketAdded;

        public event EventHandler<RtpMediaBuildedEventArgs> MediaBuilded;

        public event EventHandler<RtpClearedEventArgs> Cleared;





        private IMediaBuilder _builder;






        public void AddPacket( RtpPacket packet )
        {
            _builder?.AddPacket( packet );
        }

        public void Clear()
        {
            _builder?.Clear();
        }

        public void Setup<TBuilder>( Func<TBuilder> factory ) where TBuilder : class , IMediaBuilder
        {
            if ( factory == null )
            {
                throw new ArgumentNullException( nameof( factory ) );
            }

            if ( _builder != null )
            {
                throw new InvalidOperationException( "the builder must be disposed first" );
            }

            _builder = factory();

            _builder.PacketAdded += Builder_PacketAdded;
            _builder.PacketAdding += Builder_PacketAdding;
            _builder.MediaBuilded += Builder_MediaBuilded;
            _builder.Cleared += Builder_Cleared;
        }

        public void Dispose()
        {
            if ( _builder == null )
            {
                return;
            }

            _builder.PacketAdded -= Builder_PacketAdded;
            _builder.PacketAdding -= Builder_PacketAdding;
            _builder.MediaBuilded -= Builder_MediaBuilded;
            _builder.Cleared -= Builder_Cleared;

            if ( _builder is IDisposable disposable )
            {
                disposable.Dispose();
            }

            _builder = null;
        }








        private void Builder_PacketAdding( object sender , RtpPacketAddingEventArgs e )
        {
            PacketAdding?.TryInvoke( this , e );
        }

        private void Builder_PacketAdded( object sender , RtpPacketAddedEventArgs e )
        {
            PacketAdded?.TryInvoke( this , e );
        }

        private void Builder_MediaBuilded( object sender , RtpMediaBuildedEventArgs e )
        {
            MediaBuilded?.TryInvoke( this , e );
        }

        private void Builder_Cleared( object sender , RtpClearedEventArgs e )
        {
            Cleared?.TryInvoke( this , e );
        }
    }
}

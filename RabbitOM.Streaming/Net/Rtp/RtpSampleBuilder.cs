using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public abstract class RtpSampleBuilder : IMediaBuilder , IDisposable
    {
        public event EventHandler<RtpPacketAddingEventArgs> PacketAdding;

        public event EventHandler<RtpPacketAddedEventArgs> PacketAdded;

        public event EventHandler<RtpMediaBuildedEventArgs> MediaBuilded;

        public event EventHandler<RtpClearedEventArgs> Cleared;



        ~RtpSampleBuilder()
        {
            Dispose( false );
        }




        public void AddPacket( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            var addingPacket = new RtpPacketAddingEventArgs( packet );

            OnPacketAdding( addingPacket );

            if ( ! addingPacket.CanContinue )
            {
                return;
            }

            OnPacketAdded( new RtpPacketAddedEventArgs( packet ) );

            var mediaSample = CreateMediaSample( packet );

            if ( mediaSample == null || mediaSample.Buffer == null || mediaSample.Buffer.Length == 0 )
            {
                return;
            }

            OnBuilded( new RtpMediaBuildedEventArgs( mediaSample ) );
        }

        public void Clear()
        {
            OnCleared( RtpClearedEventArgs.Default );
        }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }







        protected virtual RtpMediaElement CreateMediaSample( RtpPacket packet )
        {
            return new RtpMediaElement( packet.Payload.ToArray() );
        }







        protected virtual void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            PacketAdding?.TryInvoke( this , e );
        }

        protected virtual void OnPacketAdded( RtpPacketAddedEventArgs e )
        {
            PacketAdded?.TryInvoke( this , e );
        }

        protected virtual void OnBuilded( RtpMediaBuildedEventArgs e )
        {
            MediaBuilded?.TryInvoke( this , e );
        }

        protected virtual void OnCleared( RtpClearedEventArgs e )
        {
            Cleared?.TryInvoke( this , e );
        }
    }
}

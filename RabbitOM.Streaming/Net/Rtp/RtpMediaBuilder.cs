using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public abstract class RtpMediaBuilder : IMediaBuilder , IDisposable
    {
        public event EventHandler<RtpPacketAddingEventArgs> PacketAdding;

        public event EventHandler<RtpPacketAddedEventArgs> PacketAdded;

        public event EventHandler<RtpBuildEventArgs> Builded;

        public event EventHandler<RtpClearedEventArgs> Cleared;



        ~RtpMediaBuilder()
        {
            Dispose( false );
        }



        public abstract void AddPacket( byte[] packet );
        
        public abstract void AddPacket( RtpPacket packet );
        
        public abstract void Clear();

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        
        
        
        
        
        protected virtual void Dispose( bool disposing )
        {
            if ( disposing )
            {
                Clear();
            }
        }






        protected virtual void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            PacketAdding?.TryInvoke( this , e );
        }

        protected virtual void OnPacketAdded( RtpPacketAddedEventArgs e )
        {
            PacketAdded?.TryInvoke( this , e );
        }

        protected virtual void OnBuild( RtpBuildEventArgs e )
        {
            Builded?.TryInvoke( this , e );
        }

        protected virtual void OnCleared( RtpClearedEventArgs e )
        {
            Cleared?.TryInvoke( this , e );
        }
    }
}

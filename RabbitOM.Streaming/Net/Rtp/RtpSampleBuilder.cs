// TODO: adding errors count, stats ??? 
// TODO: on the add method, need to discard the actual sequence, in case of receiving one invalid or missing packet ?

using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public abstract class RtpSampleBuilder : IMediaBuilder , IDisposable
    {
        public event EventHandler<RtpPacketAddingEventArgs> PacketAdding;

        public event EventHandler<RtpPacketAddedEventArgs> PacketAdded;

        public event EventHandler<RtpBuildEventArgs> Builded;

        public event EventHandler Cleared;



        private int _maximumOfPacketsSize = RtpConstants.DefaultMaximumOfPacketsSize;




        ~RtpSampleBuilder()
        {
            Dispose( false );
        }




        public int MaximumOfPacketsSize
        {
            get => _maximumOfPacketsSize;
        }
        



        public void Configure( int maximumOfPacketsSize )
        {
            _maximumOfPacketsSize = maximumOfPacketsSize > 0 ? maximumOfPacketsSize : throw new ArgumentOutOfRangeException( nameof( maximumOfPacketsSize ) );
        }

        public void AddPacket( byte[] buffer )
        {
            if ( RtpPacket.TryParse( buffer , out var packet ) )
            {
                AddPacket( packet );
            }
        }

        public void AddPacket( RtpPacket packet )
        {
            if ( packet == null || ! packet.TryValidate() )
            {
                return;
            }

            if ( packet.Payload.Count >= MaximumOfPacketsSize )
            {
                return;
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

            OnBuild( new RtpBuildEventArgs( mediaSample ) );
        }

        public void Clear()
        {
            OnCleared( EventArgs.Empty );
        }

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







        protected virtual MediaContent CreateMediaSample( RtpPacket packet )
        {
            return new MediaContent( packet.Payload.ToArray() );
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

        protected virtual void OnCleared( EventArgs e )
        {
            Cleared?.TryInvoke( this , e );
        }
    }
}

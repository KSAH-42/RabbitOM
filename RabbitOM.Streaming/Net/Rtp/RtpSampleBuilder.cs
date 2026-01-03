// TODO: adding errors count, stats ??? 
// TODO: on the add method, need to discard the actual sequence, in case of receiving one invalid or missing packet ?

using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public abstract class RtpSampleBuilder : IMediaBuilder , IDisposable
    {
        public const int DefaultMTU = 1500;

        public const int DefaultMaximumOfPacketsSize = DefaultMTU * 4;







        public event EventHandler<RtpFilteringPacketEventArgs> FilteringPacket;

        public event EventHandler<RtpPacketReceivedEventArgs> PacketReceived;

        public event EventHandler<RtpSampleReceivedEventArgs> SampleReceived;

        public event EventHandler<RtpClearedEventArgs> Cleared;







        private int _maximumOfPacketsSize = DefaultMaximumOfPacketsSize;







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

            var addingPacket = new RtpFilteringPacketEventArgs( packet );

            OnFilteringPacket( addingPacket );

            if ( ! addingPacket.CanContinue )
            {
                return;
            }

            OnPacketReceived( new RtpPacketReceivedEventArgs( packet ) );

            var buffer = packet.Payload.ToArray();

            if ( buffer == null || buffer.Length == 0 )
            {
                return;
            }

            OnSampleReceived( new RtpSampleReceivedEventArgs( new RtpSample( buffer ) ) );
        }

        public void Clear()
        {
            OnCleared( new RtpClearedEventArgs() );
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
        









        protected virtual void OnFilteringPacket( RtpFilteringPacketEventArgs e )
        {
            FilteringPacket?.TryInvoke( this , e );
        }

        protected virtual void OnPacketReceived( RtpPacketReceivedEventArgs e )
        {
            PacketReceived?.TryInvoke( this , e );
        }

        protected virtual void OnSampleReceived( RtpSampleReceivedEventArgs e )
        {
            SampleReceived?.TryInvoke( this , e );
        }

        protected virtual void OnCleared( RtpClearedEventArgs e )
        {
            Cleared?.TryInvoke( this , e );
        }
    }
}

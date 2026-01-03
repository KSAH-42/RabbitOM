// TODO: adding errors count, stats ??? 
// TODO: on the add method, need to discard the actual sequence, in case of receiving one invalid or missing packet ?

using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public abstract class RtpSampleBuilder : MediaBuilder
    {
        private int _maximumOfPacketsSize = Constants.DefaultMaximumOfPacketsSize;




        public int MaximumOfPacketsSize
        {
            get => _maximumOfPacketsSize;
        }
        



        public void Configure( int maximumOfPacketsSize )
        {
            _maximumOfPacketsSize = maximumOfPacketsSize > 0 ? maximumOfPacketsSize : throw new ArgumentOutOfRangeException( nameof( maximumOfPacketsSize ) );
        }

        public override void AddPacket( byte[] buffer )
        {
            if ( RtpPacket.TryParse( buffer , out var packet ) )
            {
                AddPacket( packet );
            }
        }

        public override void AddPacket( RtpPacket packet )
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

            var sample = CreateSample( packet );

            if ( sample == null || sample.Data == null || sample.Data.Length == 0 )
            {
                return;
            }

            OnBuild( new RtpSampleBuildedEventArgs( sample ) );
        }

        public override void Clear()
        {
            OnCleared( new RtpClearedEventArgs() );
        }
        





        protected virtual RtpSample CreateSample( RtpPacket packet )
        {
            return new RtpSample( packet.Payload.ToArray() );
        }
    }
}

using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameBuilder : RtpFrameBuilder
    {
        private readonly object _lock;

        private readonly H265FrameBuilderConfiguration _configuration;

        private readonly H265FrameAggregator _aggregator;
    




        public H265FrameBuilder()
        {
            _lock          = new object();

            _configuration = new H265FrameBuilderConfiguration();
            _aggregator    = new H265FrameAggregator( this );
        }






        public override FrameBuilderType Type
        {
            get => FrameBuilderType.H265;
        }

        public H265FrameBuilderConfiguration Configuration
        {
            get => _configuration;
        }


     



        public override void Write( byte[] buffer )
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            throw new NotImplementedException();
        }

        protected override void Dispose( bool disposing )
        {
        }
    }
}

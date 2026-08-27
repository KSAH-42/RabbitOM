using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CS1998

namespace RabbitOM.Net.RtspV2
{
    using RabbitOM.Net.RtspV2.Headers;

    public sealed class RtspClient : IDisposable
    {
        private readonly RtspClientEnvironment _environment;







        public RtspClient() : this ( new RtspClientEnvironment() )
        {
        }

        public RtspClient( RtspClientEnvironment environment )
        {
            // throwing exceptions are the best friend against hacking something, it just break the path of someone who are trying to digging or access to something
            _environment = environment ?? throw new ArgumentNullException( nameof( environment ) );
        }






        public TimeSpan ReceiveTimeout { get; set; }

        public TimeSpan SendTimeout { get; set; }

        public Uri BaseAddress { get; set; }

        public NetworkCredential Credential { get; set; }

        public Version Version { get; set; }

        public RequestsRtspHeaderCollection DefaultHeaders { get; } = new RequestsRtspHeaderCollection(); // TODO: Remove it and use a readonly collection pass to the rtsp env class







        public async Task<RtspResponse> OptionsAsync()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> OptionsAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> OptionsAsync( RtspClientRequestInfo requestInfo )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> OptionsAsync( RtspClientRequestInfo requestInfo , CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }





        public async Task<RtspResponse> DescribeAsync()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> DescribeAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> DescribeAsync( RtspClientRequestInfo requestInfo )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> DescribeAsync( RtspClientRequestInfo requestInfo , CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }





        public async Task<RtspResponse> SetupAsync()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> SetupAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> SetupAsync( RtspClientRequestInfo requestInfo )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> SetupAsync( RtspClientRequestInfo requestInfo , CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }






        public async Task<RtspResponse> PlayAsync()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> PlayAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> PlayAsync( RtspClientRequestInfo requestInfo )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> PlayAsync( RtspClientRequestInfo requestInfo , CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }





        public async Task<RtspResponse> PauseAsync()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> PauseAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> PauseAsync( RtspClientRequestInfo requestInfo )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> PauseAsync( RtspClientRequestInfo requestInfo , CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }






        public async Task<RtspResponse> TearDownAsync()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> TearDownAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> TearDownAsync( RtspClientRequestInfo requestInfo )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> TearDownAsync( RtspClientRequestInfo requestInfo , CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }







        public async Task<RtspResponse> GetParameterAsync()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> GetParameterAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> GetParameterAsync( RtspClientRequestInfo requestInfo )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> GetParameterAsync( RtspClientRequestInfo requestInfo , CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }







        public async Task<RtspResponse> SetParameterAsync()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> SetParameterAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> SetParameterAsync( RtspClientRequestInfo requestInfo )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> SetParameterAsync( RtspClientRequestInfo requestInfo , CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }







        public async Task<RtspResponse> AnnounceAsync()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> AnnounceAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> AnnounceAsync( RtspClientRequestInfo requestInfo )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> AnnounceAsync( RtspClientRequestInfo requestInfo , CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }







        public async Task<RtspResponse> RedirectAsync()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> RedirectAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> RedirectAsync( RtspClientRequestInfo requestInfo )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> RedirectAsync( RtspClientRequestInfo requestInfo , CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }







        public async Task<RtspResponse> RecordAsync()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> RecordAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> RecordAsync( RtspClientRequestInfo requestInfo )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> RecordAsync( RtspClientRequestInfo requestInfo , CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }








        public async Task<RtspResponse> SendAsync( RtspRequest request )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task<RtspResponse> SendAsync( RtspRequest request , CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task SendAsync( RtspPacket packet )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public async Task SendAsync( RtspPacket packet , CancellationToken cancellationToken )
        {
            throw new NotImplementedException( "To be implemented" );
        }






        public void Dispose()
        {
        }
    }
}

#pragma warning restore CS1998
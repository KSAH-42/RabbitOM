using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CS1998

namespace RabbitOM.Streaming.RtspV2
{
    using RabbitOM.Streaming.RtspV2.Headers;

    public sealed class RtspClient : IDisposable
    {
        private readonly RtspClientEnvironment _environment;







        public RtspClient() : this ( new RtspClientEnvironment() )
        {
        }

        public RtspClient( RtspClientEnvironment environment )
        {
            _environment = environment ?? throw new ArgumentNullException( nameof( environment ) );
        }






        public TimeSpan ReceiveTimeout { get; set; }

        public TimeSpan SendTimeout { get; set; }

        public Uri BaseAddress { get; set; }

        public NetworkCredential Credential { get; set; }

        public Version Version { get; set; }

        public RequestsRtspHeaderCollection Headers { get; } = new RequestsRtspHeaderCollection(); // TODO: Remove it and use a readonly collection pass to the rtsp env class







        public async Task<RtspResponse> OptionsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> OptionsAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> OptionsAsync( RtspClientRequestOptions requestOptions )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> OptionsAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }





        public async Task<RtspResponse> DescribeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> DescribeAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> DescribeAsync( RtspClientRequestOptions requestOptions )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> DescribeAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }





        public async Task<RtspResponse> SetupAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> SetupAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> SetupAsync( RtspClientRequestOptions requestOptions )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> SetupAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }






        public async Task<RtspResponse> PlayAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> PlayAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> PlayAsync( RtspClientRequestOptions requestOptions )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> PlayAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }





        public async Task<RtspResponse> PauseAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> PauseAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> PauseAsync( RtspClientRequestOptions requestOptions )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> PauseAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }






        public async Task<RtspResponse> TearDownAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> TearDownAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> TearDownAsync( RtspClientRequestOptions requestOptions )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> TearDownAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }







        public async Task<RtspResponse> GetParameterAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> GetParameterAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> GetParameterAsync( RtspClientRequestOptions requestOptions )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> GetParameterAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }







        public async Task<RtspResponse> SetParameterAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> SetParameterAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> SetParameterAsync( RtspClientRequestOptions requestOptions )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> SetParameterAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }







        public async Task<RtspResponse> AnnounceAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> AnnounceAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> AnnounceAsync( RtspClientRequestOptions requestOptions )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> AnnounceAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }







        public async Task<RtspResponse> RedirectAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> RedirectAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> RedirectAsync( RtspClientRequestOptions requestOptions )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> RedirectAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }







        public async Task<RtspResponse> RecordAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> RecordAsync( CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> RecordAsync( RtspClientRequestOptions requestOptions )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> RecordAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }








        public async Task<RtspResponse> SendAsync( RtspRequest request )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspResponse> SendAsync( RtspRequest request , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }

        public async Task SendAsync( RtspPacket packet )
        {
            throw new NotImplementedException();
        }

        public async Task SendAsync( RtspPacket packet , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }






        public void Dispose()
        {
        }
    }
}

#pragma warning restore CS1998
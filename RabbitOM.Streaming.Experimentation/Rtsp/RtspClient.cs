using System;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

#pragma warning disable CS1998

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspClient : IDisposable
    {
        private readonly RtspClientEnvironment _environment;





        public RtspClient() : this ( new RtspClientEnvironment() )
        {
        }

        public RtspClient( RtspClientEnvironment environment )
        {
            _environment = environment ?? throw new ArgumentNullException( nameof( _environment ) );
        }






        public TimeSpan ReceiveTimeout { get; set; }

        public TimeSpan SendTimeout { get; set; }

        // should reset the the sockets pools or the comm pools
        // when set, if user contains user name and password, update the Credentials property as well
        public Uri BaseAddress { get; set; }

        public NetworkCredential Credential { get; set; }

        public Version Version { get; set; } // for changing protocol version

        public RequestsRtspHeaderCollection Headers { get; } = new RequestsRtspHeaderCollection();

        public bool IsDisposed { get; }







        public async Task<RtspClientResponse> OptionsAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> OptionsAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }





        public async Task<RtspClientResponse> DescribeAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> DescribeAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }





        public async Task<RtspClientResponse> SetupAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> SetupAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }







        public async Task<RtspClientResponse> PlayAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> PlayAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }






        public async Task<RtspClientResponse> PauseAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> PauseAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }




        public async Task<RtspClientResponse> TearDownAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> TearDownAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }





        public async Task<RtspClientResponse> GetParameterAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> GetParameterAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }





        public async Task<RtspClientResponse> SetParameterAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> SetParameterAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }





        public async Task<RtspClientResponse> AnnounceAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> AnnounceAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }




        public async Task<RtspClientResponse> RedirectAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> RedirectAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }




        public async Task<RtspClientResponse> RecordAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> RecordAsync( RtspClientRequestOptions requestOptions , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }





        public async Task<RtspClientResponse> SendAsync( RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        // for pushing data to the server using interleaved, if the server support this feature
        // the rfc allow that, but it's will unsual
        // normally it should be present in the interface because this feature is supported on the paper
        // for ip camera, it doesn't make sense to use this method, but for computer server, it make sense
        // for instance, an iot device or event a smartphone pushing data to a server, calling setup, then record and push interleaved packet, and then teardown when there is nothing to send
        public async Task SendAsync( Packet packet , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }









        public void Dispose()
        {
            // TODO
        }
    }
}

#pragma warning restore CS1998
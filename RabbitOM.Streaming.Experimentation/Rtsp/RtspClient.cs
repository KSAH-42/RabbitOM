using System;
using System.Net;
using System.Threading.Tasks;

#pragma warning disable CS1998

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    using System.Threading;

    // removing connect and disconnect methods
    // and use a socket pool to retrieve the current active socket
    // and keep the current active and discard socket after an expiration delay
    // add configuration class to specify how to manage the socket pools
    //  the fast mode, will keep a single socket
    // don't add static socket pool class
    // and the end add async methods as partial class

    public sealed class RtspClient : IDisposable
    {
        private readonly RtspClientContext _context;





        public RtspClient() : this ( new RtspClientContext() )
        { 
        }

        public RtspClient( RtspClientContext context )
        {
            _context = context ?? throw new ArgumentNullException( nameof( _context ) );
        }






        public TimeSpan ReceiveTimeout { get; set; }

        public TimeSpan SendTimeout { get; set; }

        public Uri BaseAddress { get; set; } // should reset the the sockets pools or the comm pools

        public Version Version { get; set; } // for changing protocol version

        public NetworkCredential Credential { get; set; }

        public RequestsRtspHeaderCollection DefaultRequestHeaders { get; } = new RequestsRtspHeaderCollection();

        public bool IsDisposed { get; }









        public async Task<RtspClientResponse> OptionsAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> OptionsAsync( RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> OptionsAsync( string uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> OptionsAsync( string uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> OptionsAsync( Uri uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> OptionsAsync( Uri uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }







        public async Task<RtspClientResponse> DescribeAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> DescribeAsync( RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> DescribeAsync( string uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> DescribeAsync( string uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> DescribeAsync( Uri uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> DescribeAsync( Uri uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }








        public async Task<RtspClientResponse> SetupAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> SetupAsync( RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> SetupAsync( string uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> SetupAsync( string uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> SetupAsync( Uri uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> SetupAsync( Uri uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }








        public async Task<RtspClientResponse> PlayAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> PlayAsync( RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> PlayAsync( string uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> PlayAsync( string uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> PlayAsync( Uri uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> PlayAsync( Uri uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }








        public async Task<RtspClientResponse> PauseAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> PauseAsync( RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> PauseAsync( string uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> PauseAsync( string uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> PauseAsync( Uri uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> PauseAsync( Uri uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }







        public async Task<RtspClientResponse> TearDownAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> TearDownAsync( RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> TearDownAsync( string uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> TearDownAsync( string uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> TearDownAsync( Uri uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> TearDownAsync( Uri uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }







        public async Task<RtspClientResponse> GetParameterAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> GetParameterAsync( RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> GetParameterAsync( string uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> GetParameterAsync( string uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> GetParameterAsync( Uri uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> GetParameterAsync( Uri uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }







        public async Task<RtspClientResponse> SetParameterAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> SetParameterAsync( RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> SetParameterAsync( string uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> SetParameterAsync( string uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> SetParameterAsync( Uri uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> SetParameterAsync( Uri uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }






        public async Task<RtspClientResponse> AnnounceAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> AnnounceAsync( RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> AnnounceAsync( string uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> AnnounceAsync( string uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> AnnounceAsync( Uri uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> AnnounceAsync( Uri uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }





        public async Task<RtspClientResponse> RedirectAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> RedirectAsync( RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> RedirectAsync( string uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> RedirectAsync( string uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> RedirectAsync( Uri uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> RedirectAsync( Uri uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }




        public async Task<RtspClientResponse> RecordAsync( CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> RecordAsync( RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> RecordAsync( string uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> RecordAsync( string uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> RecordAsync( Uri uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> RecordAsync( Uri uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }




        public async Task<RtspClientResponse> SendAsync( RtspMethod method , string uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> SendAsync( RtspMethod method , string uri , RtspClientRequest request , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> SendAsync( RtspMethod method , Uri uri , CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException();
        }

        public async Task<RtspClientResponse> SendAsync( RtspMethod method , Uri uri , RtspClientRequest request , CancellationToken cancellationToken = default )
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
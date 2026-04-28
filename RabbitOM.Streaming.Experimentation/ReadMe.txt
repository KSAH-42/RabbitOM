this assembly is used for experimentation for finding different approachs of some existing implementation.
If the implementation will be enougth, it will be moved to the main assembly.

some modifications on headers will comes and can potentially changed entirely the existing code.

it should not be considered as the final implementation but closer to the final implementation.

so do not used theses classes, until it was moved to the main assembly.


public void Run()
{            
    using var client = new RtspClient( packet => Console.WriteLine( "Data received: channel:{0} size:{0}" , packet.Channel , packet.Buffer.Length ) );
            
    client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("text/plain") );
    client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("text/custom") );
   	client.DefaultHeaders.Add( "X-Header-1" , "123")
    client.DefaultHeaders.Add( "X-Header-2" , "123")
    client.DefaultHeaders.Add( "X-Header-3" , "123")
    client.DefaultHeaders.Add( "X-Header-4" , "123")
   	        
    client.Connect(Uri);

    using var optionsResponse = client.Options();
           
    optionsResponse.EnsureSuccess();

    using var describeResponse = client.Describe();
    
    describeResponse.EnsureSuccess();
            
    if ( ! RtspSessionDescriptor.TryParse( describeResponse.Body.ReadAsString() , out var sdp ) )
    {
        throw new InvalidOperationException("no sdp");
    }
            
    SetupRtspRequestBuilder setupBuilder = UseMulticastTranport
        ? new SetupMulticastRtspRequestBuilder()   { IpAddress = "224.0.0.1" , Port = 152 , TTL = 123 }
        : UseUdpTranport
        ? new SetupUnicastUdpRtspRequestBuilder()  { Port = 123 }
        : new SetupInterleavedRtspRequestBuilder();
            
    using var setupResponse = client.Setup( sdp.TrackUri , setupBuilder.BuildRequest() );
            
    var sessionHeader = SessionRtspHeader.Parse( setupResponse.Body.ReadAsString() );

    var playBuilder = new PlayTransportRtspRequestBuilder() { SessionId = sessionHeader.Id };

    using var playResponse = client.Play( playBuilder.BuildRequest() );

    playResponse.EnsureSuccess();

    Console.WriteLine( "playing.." );
    Console.WriteLine( "Press any keys to stop..." );

    Console.ReadKey();

    var tearDownBuilder = new TearDownRtspRequestBuilder() { SessionId = sessionHeader.Id };
            
    client.TearDown( tearDownBuilder.BuildRequest() );
}

internal class Program
{
    static void Main( string[] args )
    {
        using ( var client = new RabbitOM.Net.Rtsp.RtspClient() )
        {
            client.DefaultHeaders.Accept = new AcceptRtspHeader();
   	        client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("application/text") );
            client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("text") );
            client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("text/data") );
            client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("text/data") );
            client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("text/data") );
            client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("text/data") );
   	        
            client.DefaultHeaders.AcceptEncoding = new AcceptEncodingRtspHeader();
   	        client.DefaultHeaders.AcceptEncoding.Formats.Add( new StringWithQuality("plain") );
   	        client.DefaultHeaders.AcceptEncoding.Formats.Add( new StringWithQuality("zip") );
   	        client.DefaultHeaders.AcceptEncoding.Formats.Add( new StringWithQuality("tar") );
   	        client.DefaultHeaders.AcceptEncoding.Formats.Add( new StringWithQuality("br") );
   	        client.DefaultHeaders.AcceptEncoding.Formats.Add( new StringWithQuality("newWinZip") );
            
   	        client.Connect( "rtsp://127.0.0.1/city1.mp4" );

            var request = new RtspClientRequest();

            request.Headers.Accept.Mimes.Add( new StringWithQuality("rtsp/options") );
            request.Headers.Accept.Mimes.Add( new StringWithQuality("rtsp/options/blabla") );
            request.Headers.Accept.Mimes.Add( new StringWithQuality("rtsp/options/blabla") );
            request.Headers.Accept.Mimes.Add( new StringWithQuality("rtsp/options/blabla") );
            request.Headers.Accept.Mimes.Add( new StringWithQuality("rtsp/options/blabla") );
            request.Headers.Accept.Mimes.Add( new StringWithQuality("rtsp/options/blabla") );
            request.Headers.Add( "X-Sdp-Encryption" , "algorithm=abcde;public-key=123123z1zer213==" );

            var response0 = client.Options();

            var response1 = client.Options( request );
            
            var response2 = client.Options( "*" , request );

            request = new RtspClientRequest();

            request.Headers.Add( "X-Sdp-Encryption" , "algorithm=abcde;public-key=123123z1zer213==" );

            var response3 = client.Describe( request );
        }
    }
}

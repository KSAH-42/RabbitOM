
internal class Program
{
    static void Main( string[] args )
    {
        var context = new RtspClientContext();
            
        using ( var client = new RtspClient( context ) )
        {
            client.BaseAddress = new Uri( "rtsp://127.0.0.1/channel/1?type=mpeg" );
                
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
            
            var request = new RtspClientRequest();

            request.Headers.Accept.Mimes.Add( new StringWithQuality("rtsp/options") );
            request.Headers.Accept.Mimes.Add( new StringWithQuality("rtsp/options/blabla") );
            request.Headers.Accept.Mimes.Add( new StringWithQuality("rtsp/options/blabla") );
            request.Headers.Accept.Mimes.Add( new StringWithQuality("rtsp/options/blabla") );
            request.Headers.Accept.Mimes.Add( new StringWithQuality("rtsp/options/blabla") );
            request.Headers.Accept.Mimes.Add( new StringWithQuality("rtsp/options/blabla") );
            request.Headers.Add( "X-Sdp-Encryption" , "algorithm=abcde;public-key=123123z1zer213==" );

            var optionsResponse = client.Options();
                
            optionsResponse.EnsureSuccess();

            var describeResponse = client.Describe();

            describeResponse.EnsureSuccess();

            var setupResponse = client.Setup();

            setupResponse.EnsureSuccess();
                
            var playResponse = client.Play();

            playResponse.EnsureSuccess();

            var tearDownResponse = client.TearDown();

            tearDownResponse.EnsureSuccess();
        }
    }
}


Try to make a quick search about the existance of idl framework for encoders and decoders, a kind of media rpc standard.

Put idl on decoder  => to generate code
Put idl on encoder  => to generate code

Put idl on CDN ???  => to generate code


and idl for streaming with rpc style for building CDN ? I don't know if rtsp can be a good candidate because it we can describe the idl
but it provide many things streaming transport. but...

> before todo: next project after closing this one 
 => how to generate a local decoder process based from schema ? forget ffmpeg.



this assembly is used for experimentation for finding different approachs of some existing implementation.
If the implementation will be enougth, it will be moved to the main assembly.

some modifications on headers will comes and can potentially changed entirely the existing code.

it should not be considered as the final implementation but closer to the final implementation.

so do not used theses classes, until it was moved to the main assembly.



public sealed class RtspClientTemporyTest
{
    public string Uri { get; set; }
    public string TransportType { get; set; }
    private bool UseTcpTranport { get => TransportType == "tcp" || TransportType == "interleaved"; }
    private bool UseUdpTranport { get => TransportType == "udp"; }
    private bool UseMulticastTranport { get => TransportType == "multicast"; }


    public void Run()
    {            
        using var client = new RtspClient() );
           
        client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("application/text") );
        client.DefaultHeaders.Accept.Mimes.Add( new StringWithQuality("text") );
        client.DefaultHeaders.Add( "X-Header-1" , "123" );
        client.DefaultHeaders.Add( "X-Header-2" , "1234" );
        client.DefaultHeaders.Add( "X-Header-3" , "12345" );
            
        using var optionsResponse = client.Options( "*" );
           
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

        var playBuilder = new PlayRtspRequestBuilder() { SessionId = sessionHeader.Id };

        using var playResponse = client.Play( playBuilder.BuildRequest() );

        playResponse.EnsureSuccess();

        Console.WriteLine( "playing.." );
        Console.WriteLine( "Press any keys to stop..." );

        Console.ReadKey();

        var tearDownBuilder = new TearDownRtspRequestBuilder() { SessionId = sessionHeader.Id };
            
        client.TearDown( tearDownBuilder.BuildRequest() );
    }
}

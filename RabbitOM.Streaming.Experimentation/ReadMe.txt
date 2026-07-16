static class Program
{
    private static async Task Main()
    {
        using ( var client = new RtspClient() )
        {
            client.BaseAddress = new Uri( "rtsp://127.0.0.1:554/building-toxic-society-with-controlling-disorder-factor.mp4" );
            
            client.Headers.Accept = new AcceptRtspHeaderValue();
            client.Headers.Accept.Values.Add( new MediaTypeWithQualityRtspHeaderValue("application/text") );
            client.Headers.Accept.Values.Add( new MediaTypeWithQualityRtspHeaderValue("application/sdp") );
                            
            await client.OptionsAsync( new RtspClientRequestOptionsBuilder()
                .SetUri( "rtsp://127.0.0.1:554/xyz.mp4" )
                .AddHeader("A","1")
                .AddHeader("B","2")
                .AddHeader("B","2")
                .AddHeader("B","2")
                .AddHeader("B","2")
                .AddHeader("B","2")
                .WriteBody("parameter1=1\r\n")
                .WriteBody("parameter2=2\r\n")
                .WriteBody( new byte[] { 1,2,3 } )
                .Build()
                )
                ;
        }
    }
}




//////////////////////////////////////////////////////////////////
// Server impl idea
//////////////////////////////////////////////////////////////////

using RabbitOM.Streaming.Net.Rtsp.Servers; // ??? => storing listeners, attrbutes, base controllers, etc...

[Controller("stream/main")]
public sealed class MainStreamController : RtspController
{
    private readonly ILogger<MainStreamController> _logger;

    public MainStream(ILogger<MainStreamController> logger)
    {
        _logger = logger;
    }

    [RtspOptions()]
    public RtspResult Options()
    {
        return Ok();
    }

    [RtspDescribe()]
    [RtspAuthorize]
    public RtspResult Describe()
    {
        _logger.LogInformation("Describing");
        
        var sdp = new StringBuilder();
        
        sdp.AppendLine( "v=0")
        sdp.AppendLine( "a=0")
        sdp.AppendLine( "a=0")
        sdp.AppendLine( "a=0")
        sdp.AppendLine( "a=0")
        sdp.AppendLine( "a=0")

        return Ok(sdp.ToString()); 
    }

    [RtspSetup()]
    [RtspAuthorize] 
    public RtspResult Setup()
    {
        return Ok();
    }

    [RtspPlay()]
    [RtspAuthorize] 
    public RtspResult Play()
    {
        return Ok();
    }

    [RtspTeardown()]
    [RtspAuthorize] 
    public RtspResult Teardown()
    {
        return Ok();
    }
}
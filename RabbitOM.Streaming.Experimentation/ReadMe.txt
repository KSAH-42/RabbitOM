// bad person say that they prefer to be focus on efficient instead of tolerance
// it's sound like this person has a lack of something
// untolerant against attack
// untolerant against case sensitive
// untolerant will reduce the income of the compagny by spending debugging times, supports, etc... and finally a wast of money.
// all that things will produce a bad experience of the customers
// many of these people try to exaggerated an aspect of an implementation to
// put in front there skills and hide good things.
// that toxic collegue that need to be to just fired from the compagny.
// the definition of tolerance is : make it a system reliable againts failures, a device integration with retry ? can you just imagine of castrophic results ?
// untolerant doesn't include retry mechanism, is absoltely dangerous for a security system, that the big stupidity , or the mindset of fools.
// efficiency: means low allocation, fast connection and use multhreading 
// the other sign of quality, it to look on the output how many exceptions are throws during streaming ?
// Untolerant parser is a good things ? no
// device integretion without retry is a good thing ? no thats the basic of the security industry many years before the rise of cloud computing
// the inventor of C++, says good software make it hard to hide bugs. Defenitively it's true
// Is like the size of the body of method that exceed the size of screen is a code smell.
// the best is to combine the two worlds, it's stupid to a have a highly fast system where you cant' make it tolerant against failure.
// the complexity is to find the right balance. 

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









public class A
{
    public static async Task Foo()
    {
        var pipeline = new CustomRtspRequestHandler(1)
            .With( new CustomRtspRequestHandler(2))
            .With( new CustomRtspRequestHandler(3))
            .With( new CustomRtspRequestHandler(4))
            .With( new CustomRtspRequestHandler(5))
            .With( new CustomRtspRequestHandler(6))
            ;

        await pipeline.SendRequestAsync( new RtspRequest() , default );
    }
}

public sealed class CustomRtspRequestHandler : RtspRequestHandler
{
    public CustomRtspRequestHandler( int id ) => Id = id;

    public int Id { get; }

    public override async Task<RtspResponse> SendRequestAsync( RtspRequest request , CancellationToken cancellation )
    {
        Console.WriteLine( "Start" + Id );

        var result = await base.SendRequestAsync( request , cancellation ) ;
        Console.WriteLine( "Stop" + Id );

        return result ?? new RtspResponse();
    }
}

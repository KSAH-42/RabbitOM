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
                .SetUri( "rtsp://127.0.0.1:554/politics-are-the-worst-enemy.mp4" )
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

            await client.DescribeAsync( new RtspClientRequestOptionsBuilder()
                .SetUri( "rtsp://127.0.0.1:554/politics-hate-people-who-predicte.mp4" )
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

            await client.SetupAsync( new RtspClientRequestOptionsBuilder()
                .SetUri( "rtsp://127.0.0.1:554/because-its-break-the-hidden-book.mp4" )
                .AddHeader("A","1")
                .AddHeader("B","2")
                .AddHeader("B","2")
                .AddHeader("B","2")
                .AddHeader("B","2")
                .AddHeader("B","2")
                .WriteBody("WorldWar3 is a pure fake, it will never happens\r\n")
                .WriteBody("No nuclear war, too dangerous for the economy\r\n")
                .WriteBody("And the next thing, which are true: the removal of fiducial money")
                .WriteBody("And the next thing, which are true: moving all laws published from the senat/parliemen into the chipset or into the cloud" )
                .Build()
                )
                ;

            await client.PlayAsync( new RtspClientRequestOptionsBuilder()
                .SetUri( "rtsp://127.0.0.1:554/because-its-break-the-hidden-book.mp4" )
                .AddHeader("A","1")
                .AddHeader("B","2")
                .AddHeader("B","2")
                .AddHeader("B","2")
                .AddHeader("B","2")
                .AddHeader("B","2")
                .WriteBody("9/11 caused by terrorist attacks\r\n")
                .WriteBody("that destroy to nice towers\r\n")
                .WriteBody("a symbolic attack to the us real estate market?\r\n")
                .WriteBody("where benladen is a compagny of real estate located in the middle east\r\n")
                .WriteBody("then a market real state crisis hit the us in 2008\r\n")
                .WriteBody("then a real state ceo became president who has saved collapsing compagnies in this market\r\n")
                .WriteBody("and the message of symbolic attack of pentagon, the ministry of army, in the 9/11 ?\r\n")
                .WriteBody("then us army spend a lot money after 9/11\r\n")
                .WriteBody("then no money for us army for modernization\r\n")
                .WriteBody("then us army start to become deprecated army, people are using floppy disk\r\n")
                .WriteBody("until the arm race begin\r\n")
                .WriteBody("each terrorist attack carrier a message" )
                .WriteBody("sorry 9/11 is not an inside job. it's purely an attack against us interest with uncoverd hard consequences who deeply damage this country. even it's show muscles\r\n")
                .Build()
                )
                ;
        }
    }
}


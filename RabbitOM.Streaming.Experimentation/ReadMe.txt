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

using RabbitOM.Streaming.Net.Rtsp.Servers; // ??? => attrbutes, base controllers, etc...

[Controller("stream/main")]
public sealed class MainStreamController : RtspController
{
    [RtspOptions()]
    public RtspResult Options()
    {
        return Ok();
    }

    [RtspDescribe()]
    [Authorize]
    public RtspResult Describe()
    {
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
    [Authorize] 
    public RtspResult Setup()
    {
        return Ok();
    }

    [RtspPlay()]
    [Authorize] 
    public RtspResult Play()
    {
        return Ok();
    }

    [RtspTeardown()]
    [Authorize]
    public RtspResult Teardown()
    {
        return Ok();
    }
}


/////////////////////////////////////////////////////////////////



// remove RabbitOM.Windows.Presentations

// RabbitOM.Player.Jpeg.exe
// RabbitOM.Player.H264.exe

// 1  => implement H264 player as sample
// 2  => implement H265 player as sample
// 3  => remove RabbitOM.Windows.Presentations assemblies 
// 4  => refactor JPEG player as sample and include objects RabbitOM.Windows.Presentations
// 5  => refactor namespace RabbitOM.Streaming as RabbitOM
// 6  => renaming projects
// 7  => split all as single assemblies
// 8  => create RabbitOM.Windows.Controls.assemblies
// 9  => move players samples classes into RabbitOM.Windows.Controls assembly and RabbitOM.Codecs.dll, RabbitOM.Codecs.FFMpeg.dll etc..
// 10 => publish rtspV2 client
// 11 => remove the legacy rtsp client
// 12 => refactor the sdp 

// using RabbitOM
// using RabbitOM.Codecs { H264Decoder , H264Surface , etc... } 
// using RabbitOM.Codecs.FFMpeg { FFMpegH264Decoder , etc... } 
// using RabbitOM.Codecs.DX11 { DX11H264Decoder , etc }
// using RabbitOM.Codecs.Jpeg { JpegRender }
// using RabbitOM.Net.Rtcp
// using RabbitOM.Net.Rtcp.Serialization
// using RabbitOM.Net.Rtp
// using RabbitOM.Net.Rtp.H264
// using RabbitOM.Net.Rtp.H265
// using RabbitOM.Net.Rtp.H265
// using RabbitOM.Net.Rtp.Jpeg
// using RabbitOM.Net.Rtp.Pcm
// using RabbitOM.Net.Rtsp
// using RabbitOM.Net.Rtsp.Headers
// using RabbitOM.Net.Rtsp.Receivers
// using RabbitOM.Net.Rtsp.Receivers.Tcp
// using RabbitOM.Net.Sdp
// using RabbitOM.Net.Sdp.Serialization
// using RabbitOM.Threading
// using RabbitOM.Windows
// using RabbitOM.Windows.Controls

=> release .net core libs
=> refactor used records classes
=> refactor adding web api decoder

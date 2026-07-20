static class Program
{
    private static async Task Main()
    {
        using ( var client = new RtspClient() )
        {
            var result = await client.OptionsAsync( new RtspClientRequestOptionsBuilder()
                .SetUri( "rtsp://127.0.0.1/xyz.mp4" )
                .Headers( items =>
                {
                    items.Accept = new AcceptRtspHeaderValue();
                    items.Accept.Values.Add( new MediaTypeWithQualityRtspHeaderValue( "a" ) );
                    items.Accept.Values.Add( new MediaTypeWithQualityRtspHeaderValue( "b" ) );
                    items.Accept.Values.Add( new MediaTypeWithQualityRtspHeaderValue( "c" ) );
                    items.AcceptEncoding = new AcceptEncodingRtspHeaderValue();
                    items.AcceptEncoding.Values.Add( new StringWithQualityRtspHeaderValue( "zip" ) );
                    items.AcceptEncoding.Values.Add( new StringWithQualityRtspHeaderValue( "tar" ) );
                    items.AcceptEncoding.Values.Add( new StringWithQualityRtspHeaderValue( "br"  ) );
                } )
                .WriteBody("parameter1=1\r\n")
                .WriteBody("parameter2=2\r\n")
                .WriteBody("parameter3=3\r\n")
                .WriteBody("parameter4=4\r\n")
                .WriteBody("parameter5=5\r\n")
                .WriteBody("parameter6=6\r\n")
                .WriteBody( new byte[] { 1,2,3 } )
                .Build()
                );
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
// using RabbitOM.Media
// using RabbitOM.Media.FFMpeg
// using RabbitOM.Streaming.Rtcp
// using RabbitOM.Streaming.Rtp
// using RabbitOM.Streaming.Rtp.H264
// using RabbitOM.Streaming.Rtp.H265
// using RabbitOM.Streaming.Rtp.H265
// using RabbitOM.Streaming.Rtp.Jpeg
// using RabbitOM.Streaming.Rtp.Pcm
// using RabbitOM.Streaming.Rtsp
// using RabbitOM.Streaming.Rtsp.Headers
// using RabbitOM.Streaming.Rtsp.Receivers
// using RabbitOM.Streaming.Rtsp.Receivers.Tcp
// using RabbitOM.Streaming.Rtsp.Transports
// using RabbitOM.Streaming.Rtsp.Transports.Channels
// using RabbitOM.Streaming.Sdp
// using RabbitOM.Streaming.Sdp.Serialization
// using RabbitOM.Streaming.Onvif
// using RabbitOM.Threading
// using RabbitOM.UI.Controls

=> release .net core libs
=> refactor used records classes
=> refactor adding web api decoder

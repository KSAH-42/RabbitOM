# A RTSP client streaming library based on the .NET Framework 

[![Build](https://github.com/KSAH-42/RabbitOM/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/KSAH-42/RabbitOM/actions/workflows/dotnet-desktop.yml)

![Player](https://github.com/KSAH-42/RabbitOM/blob/master/Resources/Images/RabbitOM.Player.Zoom.png)

Follow this link to download binaries: https://github.com/KSAH-42/RabbitOM/releases

# Main features

* No external dependencies
* Support video format RTP - RFC 7798 - H.265 / HEVC
* Support video format RTP - RFC 6184 - H.264
* Support video format RTP - RFC 2435 - MJPEG
* Support audio format RTP - G711 µ-Law
* Support audio format RTP - G711 A-Law
* Support audio format RTP - G726
* Support audio format RTP - L24
* Support audio format RTP - L16
* Support audio format RTP - L8
* Support multiple authentication schemes as: basic and digest ( MD5, SHA1, SHA256 )
* Support RTP packets reordering
* Support RTSP messages reordering when multiple requests are sended and responses arrive in a different order
* Support Unicast TCP (interleaved mode) transport
* Support Unicast UDP transport 
* Support Multicast transport
* Support auto reconnection in case of network failures
* Support events Handlers for connection loss, receiving packet, etc...
* Reduce memory copy when using large memory blocks by using System.ArraySegment<byte> in order to minimize the usage of System.Buffer.BlockCopy
* Force the creation of ports used for receiving packets in case if the ports are temporaly used by some others applications or even block by the windows firewall.

➡️ Next Breaking changes

* Making the mediaplayer usercontrol run on a seperate thread

➡️ Next arrivals:

* Adding a CLI on the media player
* Adding the SRTP support 
* Adding the Replay feature
* Adding the new RTSP Client 
* Adding new RTSP receivers
* Adding RTCP layer 
* Onvif
* Net Core Migration

The actual RtspClient class WILL BE REMOVED (see streaming.experimentation project which is actually in progress)

# About the actual rtsp client and how to receive packets ?

~~~~C#

using ( var client = new RtspClient() )
{
    client.CommunicationStarted += ( sender , e ) =>
    {
        Console.WriteLine( "Communication started - " + DateTime.Now );
    };

    client.CommunicationStopped += ( sender , e ) =>
    {
        Console.WriteLine( "Communication stopped - " + DateTime.Now );
    };
    
    client.Connected += (sender, e) =>
    {
        Console.WriteLine("Client connected - " + client.Configuration.Uri);
    };

    client.Disconnected += (sender, e) =>
    {
        Console.WriteLine("Client disconnected - " + DateTime.Now + " - trying to reconnect..." );
    };

    client.PacketReceived += (sender, e) =>
    {
        if ( RtpPacket.TryParse( e.Packet.Data , out var packet ) )
            Console.WriteLine( "rtp packet received - payload length: {0}" , packet.Payload.Length );
    };

    client.Configuration.Uri = "rtsp://127.0.0.1/toy.mp4";
    client.Configuration.UserName = "admin";
    client.Configuration.Password = "camera123";
    client.Configuration.KeepAliveType = RtspKeepAliveType.Options; 
    client.Configuration.ReceiveTimeout = TimeSpan.FromSeconds(3);
    client.Configuration.SendTimeout = TimeSpan.FromSeconds(5);

    client.Configuration.DeliveryMode = RtspDeliveryMode.Tcp;
    client.Configuration.MediaFormat = RtspMediaFormat.Video;

    client.StartCommunication(); 

    Console.WriteLine("Press any keys to close the application");
    Console.ReadKey();
}

~~~~

# About Player / VideoDecoder sample

GUI is written using WPF without using MVVM. MVVM is not a good approach for handling video streaming. And by design, CustomControl can not expose a DataContext. Even if Mvvm is great, it introduce a lot classes. It's pretty rare to have a view where it's datacontext will be changed during the lifetime of the view. And using only mvvm, people will not have a deep understanding about how wpf works. And sometimes nugetpackage need to be added to avoid adding code on the code behind. 
Take a look, on projects like mahapps on github, mvvm is not used, even the custom MessageBox dialog don't used mvvm. mahapps framework include a BaseViewModel class, but it's not used internally... Without using mvvm requiered a more deeper knowledge on the presentation. User Mvvm for writting usercontrols, and introducing something strange. I prefer the harder way. Some people prefer using sourcetree or an equivalent tool, i prefer using git on a terminal.
    
# RabbitOM.Player used to decode RTP packets (HEVC/H264/JPEG)

This sample demonstrate how to create decoder that support different codec using FFMpeg.AutoGen dependencies.
This sample include an example of how to build a player using decoder and render running in seperate threads.
This sample include statitics component to display the framerate, the network bandwidth just making a right click.
This sample include a zoom feature (keep down the mouse left button, and draw the zoom area and release the button).

![Player](https://github.com/KSAH-42/RabbitOM/blob/master/Resources/Images/RabbitOM.Player.H264.png)

# RabbitOM.Player streaming over different transport types

You can receive media content using TCP / UDP or Multicast

![Player](https://github.com/KSAH-42/RabbitOM/blob/master/Resources/Images/RabbitOM.Player.TransportTypes.png)

If your camera is located I strongly recommended to used TCP. Otherwise if you used UDP or the event the multicast transport, contact your IT administrator and discuss with him.

# If you test first with VLC

If your are using some cameras and you may use first VLC for testing, you may observed that VLC fail to display BUT the Media.Player will display the stream.

![Player](https://github.com/KSAH-42/RabbitOM/blob/master/Resources/Images/RabbitOM.Player.H265.HIK.png)

First of all, here I use a HIK camera, and HIK camera just works as expected and works well.

The issue does not really come from VLC, but where ? VLC just used an existing external RTSP-Library, and this library doesn't support all digest algs supported by the camera.
VLC (3.0.23 and probably previous versions) will enter in a loop and will normally ask to you to enter the credentials until the authentication succeed, but the rtsp source will reply by a forbidden access result, and VLC will repeat again and again and never leave the authentication loop.
According to the rtsp headers there is the name of dll used here located on the useragent header. If you get more details, look at the Authorization header (emitted by client) and WWW-Authentication (server response).
If you familliar with ASP.Net Web API, even if the ASP.Net hide the details and support more schemes than the RTSP protocols, here it'is excatly the same headers, etc... 
And the repository that own the lib, the implementation use an Authenticator capable to computeDigestResponse, an try to createAuthenticatorString but it doesn't support latest digest algorithms needed to authenticate successfully the rtsp server. And maybe, if sales engineer identify this issue, they just can push this kind of camera for pushing the enduser for vms product replacement after making a cyber security audit.

![Player](https://github.com/KSAH-42/RabbitOM/blob/master/Resources/Images/HIK.Settings.png)

# RabbitOM.Node used to receive packet and run .net scripts triggered by the rtsp client events

This process is used to receive packets from a rtsp source (ip camera,recorder, etc...) and to run .net script written in C# or VB. 
You can configure the application edit a xml file. it support the following language:

In this example, the node will monitor the status of communication and trigger a vocal alert when the communication is back or lost.
You can also configure the script: in the xml section called "properties", the name of property are case sensitive may be the same of property define into the class.

~~~~XML

<?xml version="1.0" encoding="utf-8"?>
<script>
	<name>Device montoring script</name>
	<language>csharp</language>
	<assemblies>
		<!-- first open ILSpy -> go to menu File/Open from the GAC to the search the real path, it may change -->
		<assembly>C:\WINDOWS\Microsoft.NET\assembly\GAC_MSIL\System.Speech\v4.0_4.0.0.0__31bf3856ad364e35\System.Speech.dll</assembly>
	</assemblies>
	<properties>
		<property name="CommunicationStartedMessage">the communication is started, please contact Lilian for doing something</property>
	    <property name="CommunicationStoppedMessage">the communication is stopped</property>
		<property name="ConnectedMessage">connected to the camera, please contact Lydia for reporting</property>
		<property name="DisconnectedMessage">disconnected from the camera</property>
	</properties>
	<code>
		using RabbitOM.Node;
		using RabbitOM.Node.Scripting;
		using RabbitOM.Net.Rtsp.Clients;
		using System;
		using System.Speech.Synthesis;

		public sealed class DeviceMonitoringScript : NodeScript
		{
			private readonly SpeechSynthesizer _synthesizer = new SpeechSynthesizer();

			public DeviceMonitoringScript()
			{
				_synthesizer.SetOutputToDefaultAudioDevice();
			}

			public string CommunicationStartedMessage { get; set; }
			public string CommunicationStoppedMessage { get; set; }
			public string ConnectedMessage { get; set; }
			public string DisconnectedMessage { get; set; }

			public override void Handle( object sender , EventArgs e )
			{
				if ( e is RtspClientConnectedEventArgs )
				{
					_synthesizer.Speak( ConnectedMessage );
				}
				else if ( e is RtspClientDisconnectedEventArgs )
				{
					_synthesizer.Speak( DisconnectedMessage );
				}
			}

			protected override void Dispose( bool disposing )
			{
				if ( disposing )
				{
					_synthesizer.Dispose();
				}
			}
		}
	</code>
</script>

~~~~

and run by executing the following command (take about the path it may be incorrect)

~~~~
rtsp://admin:camera123@127.0.0.1/toy.mp4 -s ..\..\..\Resources\Configuration\node-script.xml
~~~~

# RabbitOM.NodeShell used to receive packet and run scripts triggered by the rtsp client events

This process is used to receive packets from a rtsp source (ip camera,recorder, etc...) and interact with the shell. 
You can configure the application edit a yaml file (Parse by YamlDotNet) to run scripts triggered by the rtsp client events. For instance, when the communication back with a camera you can use curl.
Here in this example, the application will used powershell to logs the activity of the communication.

~~~~YML

name: client handler

handlers:
  - name: start handler
    type: on-communication-started
    code: |
      powershell -command "New-Item -Path C:\Projects\rtsp-log.txt"
      powershell -command "$TimeStamp = Get-Date; Add-Content -Path C:\Projects\rtsp-log.txt -Value \"$TimeStamp - Communication Started\""

  - name: stop handler
    type: on-communication-stopped
    code: |
      powershell -command "$TimeStamp = Get-Date; Add-Content -Path C:\Projects\rtsp-log.txt -Value \"$TimeStamp - Communication Stopped\""

  - name: connected handler
    type: on-connected
    code: |
      powershell -command "$TimeStamp = Get-Date; Add-Content -Path C:\Projects\rtsp-log.txt -Value \"$TimeStamp - Connected\""

  - name: disconnected handler
    type: on-disconnected
    code: |
      powershell -command "$TimeStamp = Get-Date; Add-Content -Path C:\Projects\rtsp-log.txt -Value \"$TimeStamp - Disconnected\""

  - name: error handler
    type: on-error
    code: |
      powershell -command "$TimeStamp = Get-Date; Add-Content -Path C:\Projects\rtsp-log.txt -Value \"$TimeStamp - Error\""

~~~~

and run by executing the following command (take about the path it may be incorrect)

~~~~
rtsp://admin:camera123@127.0.0.1/toy.mp4 -s ..\..\..\Resources\Configuration\node-workflow.yml
~~~~

# About the next rtsp client (experimental)

The actual rtsp client will be replace by receivers class, and the new rtsp client will be also the replacement of the actual RtspConnection class with new features.

The implementation will be very similar to the following piece of code:

~~~~C#

static class Program
{
    private static async Task Main()
    {
        using ( var client = new RtspClient() )
        {
            client.BaseAddress = new Uri( "rtsp://127.0.0.1:554/living-in-a-toxic-society.mp4" );

            client.DefaultHeaders.Accept = new AcceptRtspHeaderValue();
            client.DefaultHeaders.Accept.Values.Add( new MediaTypeWithQualityRtspHeaderValue("a/a") );
            
            var response = await client.OptionsAsync( new RtspClientRequestInfoBuilder()
                .SetUri( "*" )
                .Headers( items =>
                {
                    items.Accept = new AcceptRtspHeaderValue();
                    items.Accept.Values.Add( new MediaTypeWithQualityRtspHeaderValue("a/a1") );
                    items.Accept.Values.Add( new MediaTypeWithQualityRtspHeaderValue("a/b1") );
                    items.Accept.Values.Add( new MediaTypeWithQualityRtspHeaderValue("a/c1") );
                    items.AcceptEncoding = new AcceptEncodingRtspHeaderValue();
                    items.AcceptEncoding.Values.Add( new StringWithQualityRtspHeaderValue( "zip" ) );
                    items.AcceptEncoding.Values.Add( new StringWithQualityRtspHeaderValue( "tar" ) );
                    items.AcceptEncoding.Values.Add( new StringWithQualityRtspHeaderValue( "br" ) );
                } )
                .WriteBody("parameter1=1\r\n")
                .WriteBody("parameter2=2\r\n")
                .WriteBody("parameter3={0}\r\n" , DateTime.Now )
                .WriteBody( new byte[] { 1,2,3 } )
                .Build()
                )
                ;

            response.EnsureSuccess();
        }
    }
}

~~~~

# Getting more details ?

If you want to get more details, you can send me an email to "a.sahnine@netcourrier.com" or "kader.sahnine11@gmail.com"

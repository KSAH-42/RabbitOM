# A resilent RTSP client based on the .net framework

[![Build](https://github.com/KSAH-42/RabbitOM/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/KSAH-42/RabbitOM/actions/workflows/dotnet-desktop.yml)

A [RTSP](https://www.rfc-editor.org/rfc/rfc2326) .net library for receiving raw audio/video streams. 

![Player](https://github.com/KSAH-42/RabbitOM/blob/master/Resources/Images/RabbitOM.Tests.Client.Mjpeg.png)

# Main features

* No external dependencies
* Support video format RTP - RFC 7798 - H.265 / HEVC
* Support video format RTP - RFC 6184 - H.264
* Support video format RTP - RFC 2435 - MJPEG
* Support audio format RTP - G711 µ-Law
* Support audio format RTP - G711 A-Law
* Support audio format RTP - G726
* Support audio format RTP - L16
* Support audio format RTP - L8
* Support RTP packet reordering
* Support Unicast TCP (interleaved mode) transport
* Support Unicast UDP transport 
* Support Multicast transport
* Support RTSP message reordering when multiple requests are sended and responses arrive in a different order
* Support request / response handshake when receiving video streams on the same tcp channel used for sending requests and receiving responses
* Support multiple types of authentications: basic and digest ( MD5, SHA1, SHA256, SHA512 )
* Support auto reconnection in case of network failures
* Support events Handlers for connection loss, receiving packet, etc...
* Provide classes to access to the SDP informations
* Thread safe, except the sdp classes and some rtp classes
* Reduce memory copy when using large memory blocks by using System.ArraySegment<byte> in order to minimize the usage of System.Buffer.BlockCopy
* Handle large streams with a high bitrate like 50 MBits per second
* Force the creation of ports used for receiving packets in case if the ports are temporaly used by some others applications

➡️ Breaking changes since the version 2.0.0.0:

* Namespace reorganization
* Rtp layer refactorization and adding add packet inspection, adding support of H264, H265, G711, etc...
* Moving and renaming classes to different namespace
* Add new class libray for rending jpeg using wpf
* Improve cpu an memory consumption during rending

➡️ Next arrival things:

* Adding RTCP layer 
* Adding support video decoder for rendering MPEG using C++ WRL or COM/ATL
* Onvif integration will comes with deep integration events support, ptz, io, config, discovery, etc..

* ⚠️ COMING REFACTORY: the RtspClient class WILL BE REMOVED totally and will REPLACED by a better implementation with immutable types and builder, and other nices things

# How to receive raw rtp packets using the rtsp client ?

Like this :

~~~~C#

using ( var client = new RtspClient() )
{
    // Raised when a successfull connection or when the communication has been recovered after a lost
    client.Connected += (sender, e) =>
    {
        Console.WriteLine("Client connected - " + client.Configuration.Uri);
    };

    // Raised when the communication has been lost
    client.Disconnected += (sender, e) =>
    {
        Console.WriteLine("Client disconnected - " + DateTime.Now);
    };

    // Raised when a raw media data has been received 
    client.PacketReceived += (sender, e) =>
    {
        var interleavedPacket = e.Packet as RtspInterleavedPacket;

		if ( interleavedPacket != null && interleavedPacket.Channel > 0 )
	    	return;
	
		if ( RTPPacket.TryParse( e.Packet.Data , out RTPPacket packet ) )
            Console.WriteLine( "DataReceived {0}" , packet.Payload.Length );
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

# About the connection class

➡️ First of all, unlike HTTP which is a STATELESS protocol, RTSP is STATEFULL. 

Thats means, that any rtsp clients must keep it's own socket opened during all the time when calling any methods.

The RtspConnection class will give you more features and a direct access to the protocol messaging layer. The following code demonstrate how to list the supported methods available on a security camera:

~~~~C#

using ( var connection = new RtspConnection() )
{
    // Events subscriptions

    connection.Opened               += (sender, e) => Console.WriteLine("Connected");
    connection.MessageReceived      += (sender, e) => Console.WriteLine("Message received");
    connection.MessageSended        += (sender, e) => Console.WriteLine("Message sended");
    connection.DataReceived         += (sender, e) => Console.WriteLine("Data received");
    connection.AuthenticationFailed += (sender, e) => Console.WriteLine("Authentication failed");
    connection.Closed               += (sender, e) => Console.WriteLine("Connection closed");
    connection.Error                += (sender, e) => Console.WriteLine("Error occurs");

    // Connect to rtsp server (happytime-rtsp-server.exe)
    
    connection.Open("rtsp://192.168.1.11/city1.mp4");
     
    // Request the available methods from a server

    connection
            .GetOptions()
            .Invoke()
            .Response
            .GetHeaderPublicOptions()
            .ToList()
            .ForEach( Console.WriteLine )
            ;
}

~~~~

Below, on the pseudo code, the remote method invocation works like this:

~~~~C#

var bodyResult =

 connection

        .Play()

        .AddHeader( new RtspHeaderContentType( RtspMimeType.ApplicationText ) )
        .AddHeader( "X-Header1" , "my value 1")
        .AddHeader( "X-Header2" , "my value 2")
        .AddHeader( "X-Header3" , "my value 3")
        .AddHeader( "X-Header4" , "my value 4")
        .AddHeader( "X-Header5" , "my value 5")
        .AddHeader( "X-Header6" , "some_values={0}-{1};other_param={2};" , 1234 , 4321 , 5000 )

        .WriteBodyLine( "timestamp:{0}" , DateTime.Now )
        .WriteBodyLine( "computer:{0}" , System.Environment.Machine )
        .WriteBodyLine( "uuid:{0}" , Guid.NewGuid() )

        .Invoke()
	
        .Response

        .GetBody()
        ;
~~~~

You will be able to decorate each request by adding customs headers, because some cameras can not reply to a request that just contains only standard headers or if there any messages contains incomplete headers. If you want to invoke a method on a particular server, you MUST read the server documentation especially the SETUP method. For instance, the SETUP are used to ask to the camera to create (not to start) a streaming session that just relies on a specific transport layers.

⚠️: The only thing to known about the connection class is that the current implementation does not support SSL/TLS. But not yet.


# About Session Description Protocol layer

That's an exemple of how to used the [SDP](https://www.rfc-editor.org/rfc/rfc4566).

~~~~C#

var sessionDescriptor = new SessionDescriptor();

sessionDescriptor.Origin.UserName = "John";
sessionDescriptor.Origin.Address = "192.168.1.23";
sessionDescriptor.Origin.AddressType = AddressType.IPV4;
sessionDescriptor.Origin.NetworkType = NetworkType.Internet;
sessionDescriptor.Origin.Version = 1;
sessionDescriptor.Origin.SessionId = 123456789;
sessionDescriptor.Version.Value = 1;
sessionDescriptor.SessionName.Value = "My session name";
sessionDescriptor.Repeats.Add(new RepeatField(new ValueTime(1, 2), new ValueTime(3, 4)));
sessionDescriptor.Repeats.Add(new RepeatField(new ValueTime(10, 20), new ValueTime(30, 40)));
sessionDescriptor.Attributes.Add(new AttributeField("myAttribute1", "myValue1"));
sessionDescriptor.Attributes.Add(new AttributeField("myAttribute2", "myValue2"));
sessionDescriptor.Attributes.Add(new AttributeField("myAttribute2", "myValue3"));

sessionDescriptor.Phones.Add(new PhoneField("+33 1 12 34 56 78"));
sessionDescriptor.Phones.Add(new PhoneField("+33 1 12 34 56 79"));
sessionDescriptor.Uri.Value = "rtsp://192.168.1.11:554/video/channel/1";

for ( int i = 1; i <= 10; ++ i )
{
    var mediaDescription = new MediaDescriptionField();

    mediaDescription.Payload = 1 + i;
    mediaDescription.Port = 10 + i;
    mediaDescription.Profile = ProfileType.AVP;
    mediaDescription.Protocol = ProtocolType.RTP;
    mediaDescription.Type = MediaType.Video;

    mediaDescription.Encryption.Key = "myKey"+i.ToString();
    mediaDescription.Encryption.Method = "myMethod"+i.ToString();
    mediaDescription.Connection.Address = "192.168.1."+i.ToString();
    mediaDescription.Connection.AddressType = AddressType.IPV4;
    mediaDescription.Connection.NetworkType = NetworkType.Internet;
    mediaDescription.Bandwiths.Add(new BandwithField("modifier", i));
    mediaDescription.Bandwiths.Add(new BandwithField("modifier"+i.ToString(), i+i));
    mediaDescription.Attributes.Add(new AttributeField("myAttribute1", "myValue1"));
    mediaDescription.Attributes.Add(new AttributeField("myAttribute2", "myValue2"));
    mediaDescription.Attributes.Add(new AttributeField("myAttribute3", "myValue3"));

    sessionDescriptor.MediaDescriptions.Add(mediaDescription);
}

Console.WriteLine( sessionDescriptor.ToString() );

if ( SessionDescriptor.TryParse( sessionDescriptor.ToString() , out SessionDescriptor descriptor ) )
{
    Console.WriteLine("Session descriptor parsed");
}

~~~~

# Test player

This project contains also a sample that demonstrate how to create a video player from a mjpeg rtsp source. 

![Player](https://github.com/KSAH-42/RabbitOM/blob/master/Resources/Images/RabbitOM.Tests.Client.Mjpeg.Hik.png)

# Getting more details ?

If you want to get more details, you can send me an email to "a.sahnine@netcourrier.com"

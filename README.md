# A resilient rtsp client based on the .net framework

[![Build](https://github.com/KSAH-42/RabbitOM/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/KSAH-42/RabbitOM/actions/workflows/dotnet-desktop.yml)

A .net library for receiving raw audio/video streams using [rtsp](https://www.rfc-editor.org/rfc/rfc2326). 

# Main features

* No external dependencies, pure .net
* Support Unicast TCP (interleaved mode) / Unicast UDP / Multicast Streaming transports
* Support multiple authentications: Basic, MD5, SHA256, SHA512
* Support auto reconnection in case of network failures
* Support message reordering when multiple requests are sended and responses arrive in a different order
* Easy to used
* Provide Event Handlers for connection loss, receiving packet, etc...
* Provide classes to access to the SDP informations
* Thread safe, except the sdp classes and some rtp classes
* Handle large streams with a high bitrate like 50 MBits/seconds
* Force the creation of ports used for receiving packets in case if the ports are temporaly used by some others applications
* Support RTP 2435 - frame parsing (MJPEG) : reconstruct the picture from packets (new implementation)



➡️ The current implementation is only based on the .net Framework 4.8

➡️ RTP H264 and RTP HVEC the new implementation is actually in progress


# How to receive raw rtp packets using the rtsp client ?

Like this :

~~~~C#

using ( var client = new RtspClient() )
{
    // Fired when a successfull connection or when the communication has been recovered after a lost
    client.Connected += (sender, e) =>
    {
        Console.WriteLine("Client connected - " + client.Configuration.Uri);
    };

    // Fired when the communication has been lost
    client.Disconnected += (sender, e) =>
    {
        Console.WriteLine("Client disconnected - " + DateTime.Now);
    };

    // Fired when a raw media data has been received 
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

You will be able to decorate each request by adding customs headers, because some cameras can not reply to a request that just contains only standard headers or if there the message contains incomplete headers. If you want to invoke a method on a particular server, you MUST read the server documentation especially the SETUP method. For instance, the SETUP are used to ask to the camera to create a streaming session that relies on a specific transport layers.

⚠️: The only thing to known about the connection class is that the current implementation does not support SSL/TLS. But not yet.


# About Session Description Protocol layer

That an exemple of how to use this .net implementation of [SDP](https://www.rfc-editor.org/rfc/rfc4566), please not that is implementation provide custom validation classes which are not explain in this example.

~~~~C#

var sessionDescriptor = new SessionDescriptor();

sessionDescriptor.Origin.UserName = "John";
sessionDescriptor.Origin.Address = "192.168.1.23";
sessionDescriptor.Origin.AddressType = AddressType.IPV4;
sessionDescriptor.Origin.NetworkType = NetworkType.Internet;
sessionDescriptor.Origin.Version = "V1";
sessionDescriptor.Origin.SessionId = "123456789";
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

There is an existing tool used to display picture from a rtsp source that render only a MJPEG steam. This sample can demonstrate how to build a player.

![Player](https://github.com/KSAH-42/RabbitOM/blob/master/Resources/Images/RabbitOM.Streaming.Tests.Mjpeg.png)

# Getting more details ?

If you want to get more details, send me an email to "a.sahnine@netcourrier.com"

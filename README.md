# RabbitOM : a resilient RTSP client in .net

[![Build](https://github.com/KSAH-42/RabbitOM/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/KSAH-42/RabbitOM/actions/workflows/dotnet-desktop.yml)

A .net assemblies which can be used for receiving raw audio/video streams. After recfactoring the client, I will add the rtp layers which already exists, but i need to do something before to publish it.

Classes used for decoding was already implemented but not present in this repository at this moment.

If you want get more details, you can send me an email to "a.sahnine@netcourrier.com"

# Main features

* Support Unicast TCP / Unicast UDP / Multicast Streaming transports
* Support multiple authentications: Basic, MD5, SHA256, SHA512
* Support auto reconnection in case of network failures
* Support message reordering when multiple requests are sended and responses arrive in a different order
* Easy to used
* Provide Event Handlers for connection loss, receiving packet, etc...
* Provide classes to access to the SDP informations
* Thread safe
* Handle large streams like 50 MBits/seconds
* Force the creation of ports used for receiving packets in case if the ports are temporaly used by some applications
 
Get more power ? I don't like the terms power, but if you want to get more power, use the RTSPConnection class instead of the RTSPClient class, which are more generic. The RTSPConnection class will give you more features and a direct access to the protocol messaging layer. The only thing to known about the connection class is that the current implementation does not support SSL/TLS. But not yet.

# How to receive raw rtp packets using the rtsp client ?

Like this :

~~~~C#

using ( var client = new RTSPClient() )
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
        var interleavedPacket = e.Packet as RTSPInterleavedPacket;

        if ( interleavedPacket != null && interleavedPacket.Channel > 0 )
             return;

        Console.WriteLine("DataReceived {0} ", e.Packet.Data.Length); // raw rtp go here
    };

    client.Configuration.Uri = Constants.LocalServer;
    client.Configuration.UserName = Constants.UserName;
    client.Configuration.Password = Constants.Password;
    client.Configuration.DeliveryMode = RTSPDeliveryMode.Tcp;
    client.Configuration.KeepAliveType = RTSPKeepAliveType.Options; 
    client.Configuration.ReceiveTimeout = TimeSpan.FromSeconds(3);
    client.Configuration.SendTimeout = TimeSpan.FromSeconds(5);

    client.StartCommunication(); 

    Console.WriteLine("Press any keys to close the application");
    Console.ReadKey();
}

~~~~

# About the connection class

The following code demonstrate how to list the supported methods available on a security camera:

~~~~C#

using ( var connection = new Rtsp.Remoting.RTSPConnection() )
{
    // Events subscriptions

    connection.Opened               += (sender, e) => Console.WriteLine("Connected");
    connection.MessageReceived      += (sender, e) => Console.WriteLine("Message received");
    connection.MessageSended        += (sender, e) => Console.WriteLine("Message sended");
    connection.DataReceived         += (sender, e) => Console.WriteLine("Data received");
    connection.AuthenticationFailed += (sender, e) => Console.WriteLine("Authentication failed");
    connection.Closed               += (sender, e) => Console.WriteLine("Connection closed");
    connection.Error                += (sender, e) => Console.WriteLine("Error occurs");

    // Connect to RTSP server (happytime-rtsp-server.exe)
    
    connection.Open("rtsp://192.168.1.11/city1.mp4");
     
    // Request the available methods from a server

    connection
            .GetOptions()
            .Invoke()
            .Response
            .GetHeaderPublicOptions()
            .ToList()
            .ForEach( supportedMethod => Console.WriteLine(supportedMethod) )
            ;
}

~~~~

Below, on the pseudo code, the remote method invocation works like this:

~~~~C#

var bodyResult =

 connection

        .SetParameter() // Some RTSP Method like GetOption, Play

        .AddHeader( new RTSPHeaderContentType( RTSPMimeType.ApplicationText ) )
        .AddHeader( "X-Header1" , "my value 1")
        .AddHeader( "X-Header2" , "my value 2")
        .AddHeader( "X-Header3" , "my value 3")
        .AddHeader( "X-Header4" , "my value 4")
        .AddHeader( "X-Header5" , "my value 5")
        .AddHeader( "X-Header6" , "port_range={0}-{1};timeout={2};" , 1234 , 4321 , 5000 )

        .WriteBodyLine( "timestamp:{0}" , DateTime.Now )
        .WriteBodyLine( "computer:{0}" , System.Environment.Machine )
        .WriteBodyLine( "uuid:{0}" , Guid.NewGuid() )

        .Invoke()
	
        .Response

        .GetBody()
        ;
~~~~

You will be able to decorate each request by adding customs headers, because some cameras can not reply to a request that just contains only standard headers or if there the message contains incomplete headers. If you want to invoke a method on a particular server, you MUST read the server documentation especially the SETUP method. For instance, the SETUP are used to ask to the camera to create a streaming session based on RTP multicast channel.

# About Session Description Protocol layer

The actual implementation provide a strong type objects. Please notes, that this implementation has been tested ONLY with many security cameras models and many RTSP servers, but NOT with VoIP devices.

Usage:

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

Console.WriteLine(sessionDescriptor.ToString());

if ( SessionDescriptor.TryParse( sessionDescriptor.ToString() , out SessionDescriptor descriptor ) )
{
    Console.WriteLine("Session descriptor parsed");
}

~~~~


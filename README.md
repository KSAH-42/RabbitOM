# RabbitOM

# Introduction

This is a library (in .net) used to connect, managed, and received video/audio streams from security camera using standard protocols like:

| Module / Protocols           | Status                                    |
| ---------------------------- | ----------------------------------------- |
| SDP                          | actually implemented                      |
| RTSP                         | actually implemented                      |
| RTP                          | not actually implemented                  |
| RTCP                         | not actually implemented                  |
| Onvif                        | not actually implemented                  |


# About Session Description Protocol

What is SDP ?

SDP is a protocol used to describe a streaming session configuration, and contains important informations like the real uri and the keys used by Codecs, which are NOT accessible using Onvif protocols. Theses keys like VPS, PPS, SPS are mandatories. You can NOT decode video streams just be receiving data from a RTP channel. And theses keys are stored inside the SDP "document" where the SDP are only exchanged during a RTSP session. The SDP protocol are used not only by security cameras but also used by device that support SIP protocols like VoIP systems.

About the implementation

The actual implementation provide a strong type objects. I found many implementation that just implement a SDP using a dictionary of string/string or string/object. In many projects, when people add more and more features, it may difficult to access to the data. Using a simple dictionary can introduce an anti pattern called "primitive obsession". To avoid this ugly approach of using just a dictionary, I decided to implement a set of classes that provide a better access to the data located inside the SDP document. According to the RFC, the serialization mecanism MUST respect a particular order. So here, you will find a tolerant serializer. This actual implementation provide a tolerant serialization mecanism that handle many cases, like formating issues, case sensitive issues, ordering issues, extra whitespaces between separators, etc... which are sometimes, present in some systems that can deliver a SDP and may cause interpretation issues. This implementation has been tested ONLY with many security cameras models and many RTSP servers, but NOT with VoIP devices.


Usage:

~~~~C#

var sessionDescriptor = new SessionDescriptor();

sessionDescriptor.Version.Value = 1;
sessionDescriptor.SessionName.Value = "My session name";
sessionDescriptor.Repeats.Add(new RepeatField(new ValueTime(1, 2), new ValueTime(3, 4)));
sessionDescriptor.Repeats.Add(new RepeatField(new ValueTime(10, 20), new ValueTime(30, 40)));
sessionDescriptor.Origin.Address = "192.168.1.23";
sessionDescriptor.Origin.AddressType = AddressType.IPV4;
sessionDescriptor.Origin.NetworkType = NetworkType.Internet;
sessionDescriptor.Origin.UserName = "Kader";
sessionDescriptor.Origin.Version = "V1";
sessionDescriptor.Origin.SessionId = "123456789";
sessionDescriptor.Attributes.Add(new AttributeField("myAttribute1", "myValue1"));
sessionDescriptor.Attributes.Add(new AttributeField("myAttribute2", "myValue2"));
sessionDescriptor.Attributes.Add(new AttributeField("myAttribute2", "myValue3"));

sessionDescriptor.Phones.Add(new PhoneField("+33 1 12 34 56 78"));
sessionDescriptor.Phones.Add(new PhoneField("+33 1 12 34 56 79"));
sessionDescriptor.Uri.Value = "rtsp://192.168.0.11:554/video/channel/1";

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
    mediaDescription.Connection.Address = "192.168.0."+i.ToString();
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
    Console.WriteLine("Parsed by rabbit");
}

~~~~


# About Real Time Streaming Protocol

Where RTSP are used ?

RTSP are used by security cameras and also used by Mircast technology. SmartTV start a RTSP client and connect to RTSP server on you local machine to receive the video of you desktop.

What is RTSP ?

RTSP is a protocol used to control and to receive video/audio streams. RTSP is very similar to the HTTP protocol. Like HTTP protocol, you have a some methods like GET/POST/TRACE/DELETE, and somes headers separated by carriage sreturns and a message body. Here it is exactly the same thing exception that the method are dedicated for the streaming. RTSP propose the following methods:

| Methods                      | Description                                               |
| ---------------------------- | --------------------------------------------------------- |
| OPTIONS                      | List the supported methods (DESCRIBE/PLAY/SETUP,etc...    |
| DESCRIBE                     | Retrive the SDP                                           |
| SETUP                        | Setup the transport layer (unicast/multicast/interleaved) |
| PLAY                         | Start the streaming                                       |
| PAUSE                        | Pause the streaming                                       |
| STOP                         | Stop the streaming                                        |
| GET_PARAMETER                | List customs parameters                                   |
| SET_PARAMETER                | Change customs parameters                                 |
| TEARDOWN                     | Destroy the session                                       |
| ANNOUNCE                     | Posts the description of a media                          |
| RECORD                       | Ask for recording                                         |
| REDIRECT                     | This method is used to redirects the traffic              |

Depending of cameras, you MUST send periodically a heart beat message using a particular message, otherwise the streaming will be closed by the server. Please notes also, to maintain a session active you must read the documentation of the camera to know which RTSP method is need to keep alive a session. There is not predifined method for all cameras. If you are using Onvif protocol, the Onvif tells that the GetParameter must be used, but in the real world some manufacturer used the GET_PARAMETER or the SET_PARAMETER or the OPTIONS methods. It's depends of the product.

By essence, RTSP is very similar to http message except important things:

* RTSP has some propriatary and mandatory header like CSeq header
* RTSP works asynchonously. RTSP used message correlation identifier stored on CSeq header used on each request and response and must have the same message identifier. Message identifier increment after each remote method invocation, not during a retries. So, depending to the server, it is possible that you can receive a response of a previous request after receiving a response of the new / actual request. 
* Unlike HTTP, the RTSP server can send spontaneously a request to the client ON THE SAME TCP Channel, it means you open tcp socket client, you can send a request and a response, but the server can also send a request to the client on the same socket.
* Using HTTP 1.1, Video stream are push using multipart technics. With RTSP, video packets are received on the same client socket where you are sending requests and waiting at the same time the response: This is called interleaved mode.

These things are handle by the lib, it's also support the lastest digest authentication used by the lastest professional security cameras.

About the implementation

I use the fluent to technic to perform remote method invocation:

The following code demonstrate how to list the supported methods exposed by a security camera:

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
    
    if ( ! connection.Open("rtsp://192.168.1.11/city1.mp4", new Rtsp.RTSPCredentials("admin", "camera123")) )
    {
        Console.WriteLine("Connection failed");
        return;
    }
     
    // Request the available methods from a server

    connection
            .GetOptions()
            .Invoke()
            .Response
            .GetHeaderPublicOptions()
            .ToArray()
            .ToList()
            .ForEach( supportedMethod => Console.WriteLine(supportedMethod) )
            ;
}

~~~~

Please notes, that if you want to receive video streams, you must invoke a series of methods, please take some times to read RFC about the RTSP or make some research on google about the "RTSP session state machine".

Below, on the pseudo code, the remote method invocation works like this:

~~~~C#

var bodyResult =

 connection

        .XxxxxxxMethod1() // Some RTSP Method

        .AddHeader( "X-Header1" , "my value 1")
        .AddHeader( "X-Header2" , "my value 2")
        .AddHeader( "X-Header3" , "my value 3")
        .AddHeader( "X-Header4" , "my value 4")
        .AddHeader( "X-Header5" , "my value 5")
        .AddHeader( new RTSPContenRTSPHeaderContentType( RTSPMimeType.ApplicationText ) )

        .WriteBody( "Parameters")
        .WriteBodyLine()
        .WriteBodyLine( "Parameter1:{0}" , DateTime.Now )
        .WriteBodyLine( "Parameter2:{0}" , Guid.NewGuid() )
        .WriteBodyLine( "Parameter3:{0}" , System.Environment.Machine )

        .Invoke()
	
        .Response

        .GetBody()
        ;


~~~~

You can decorate each request, because some camera can not reply if there is not custom mandatory headers.
And of course, some camera or server doesn't replay or give a the right response because there some headers particular.
If you invoke a method on a particular server, you MUST read the server documentation.

NOTES:

Please notes that it is not the final implementation, event it can be used for production.
I need to change some parts of the code located on headers classes and to implement a better packet message decoder.
I espect some code refactoring of many classes.
This the rtsp connection has been tested with a lot of professional security cameras IP.

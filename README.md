# RabbitOM

# Introduction

This is a net library used to connect, managed, and received video/audio streams from security camera using standard protocols like:

In the past, I have created a similar set classes, but here I want to produce a better implementation.
For instance, I found a security issue, I don't think it is a good to things to expose credentials as getter property. I will used SecureString instead of a string type for storing password.
And review some existing classes that I have already created on RTSP Layer and Onvif Layer.


# About Session Description Protocol

What is SDP ?

SDP is a protocol used to describe a streaming session configuration, and contains important informations like the real uri and the keys used by Codecs, which are NOT accessible using Onvif protocols. Theses keys like VPS, PPS, SPS are mandatories. You can NOT decode video streams just be receiving data from a RTP channel. And theses keys are stored inside the SDP "document" where the SDP are only exchanged during a RTSP session. The SDP protocol are used not only by security cameras but also used by device that support SIP protocols like VoIP systems.

About the implementation

The actual implementation provide a strong type objects. I found many implementation that just implement a SDP using a dictionary of string/string or string/object. In many projects, when people add more and more features, it may difficult to access to the data. Using a simple dictionary can introduce an anti pattern called "primitive obsession". To avoid this ugly approach of using just a dictionary, I decided to implement a set of classes that provide a better access to the data located inside the SDP document. According to the RFC, the serialization mecanism MUST respect a particular order. So here, you will find a tolerant serializer. This actual implementation provide a tolerant serialization mecanism that handle many cases, like formating issues, case sensitive issues, ordering issues, extra whitespaces between separators, etc... which are sometimes, present in some systems that can deliver a SDP and may cause interpretation issues. This implementation has been tested ONLY with many security cameras models and many RTSP servers, but NOT with VoIP devices.
The implemtation is not truely finished. I except to add distinct Value Objects/Content Value objects

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
    Console.WriteLine("Parsed by rabbit");
}

~~~~


# About Real Time Streaming Protocol

Where RTSP are used ?

RTSP are used by security cameras and also used by Miracast technology. SmartTV start a RTSP client and connect to RTSP server that run on your local machine in order to receive the video of your desktop.

What is RTSP ?

RTSP is a protocol used to control and to receive video/audio streams. RTSP is very similar to the HTTP protocol. Like HTTP protocol, you have some methods like GET/POST/TRACE/DELETE/PUT and so on, and somes headers separated by carriage returns and a message body. Here it is exactly the same thing except that the methods are dedicated for the streaming operations. RTSP proposed the following methods:

| Methods                      | Description                                                                       |
| ---------------------------- | --------------------------------------------------------------------------------- |
| OPTIONS                      | List the supported methods (OPTIONS/DESCRIBE/PLAY/SETUP,etc...)                   |
| DESCRIBE                     | Retrive the SDP                                                                   |
| SETUP                        | Ask for creating a session with a transport layer (unicast/multicast/interleaved) |
| PLAY                         | Start the streaming                                                               |
| PAUSE                        | Pause the streaming                                                               |
| STOP                         | Stop the streaming                                                                |
| GET_PARAMETER                | List customs parameters                                                           |
| SET_PARAMETER                | Change customs parameters                                                         |
| TEARDOWN                     | Destroy the session and stop the associated stream. It doesn't stop all streams!  |
| ANNOUNCE                     | Posts the description of a media                                                  |
| RECORD                       | Ask for recording                                                                 |
| REDIRECT                     | This method is used to redirects the traffic                                      |

Depending of cameras, you MUST send periodically a heart beat message using a particular message, otherwise the streaming will be closed by the server. Please notes also, to maintain a session active you must read the documentation of the camera to know which RTSP method is need to keep alive a session. There is no predefined method for all cameras. If you are using Onvif protocol, the Onvif tells that the GetParameter must be used, but in the real world some manufacturer used the GET_PARAMETER or the SET_PARAMETER or the OPTIONS methods. It's depends of the product.

By essence, RTSP is very similar to http message except important things:

* RTSP has some proprietary and mandatory header like CSeq header
* RTSP works asynchonously. RTSP used message correlation identifier stored on CSeq header used on each request and response and must have the same message identifier. Message identifier increment after each remote method invocation, not during a retry. So, depending to the server, it is possible that you can receive a response of a previous request after receiving a response of the new / actual request. 
* Unlike HTTP, the RTSP server can send spontaneously a request to the client ON THE SAME TCP Channel, it means when you open a tcp socket client and you send a request it may possible that the server can send a request to the client on the same socket during you request operation.
* Using HTTP/1.1, video stream are push using multipart technics. With RTSP, packets are received on the same client socket where you are sending requests and waiting at the same time the response: This is called interleaved mode.

All these things are handle by the lib, and it's also support the lastest digest authentication used by the lastest professional security cameras.


# About the implementation of RTSP classes

I have already build this class, but I will commit in another moment after a code refactoring.

In my previous implementation, I use the fluent OOP approach to perform remote method invocation, I will preserve this approach.

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
            .ToList()
            .ForEach( supportedMethod => Console.WriteLine(supportedMethod) )
            ;
}

~~~~

Please notes, that if you want to receive video streams, you must invoke a series of methods, please take sometimes to read the RFC about RTSP or just make some research on google about the "RTSP session state machine".

Below, on the pseudo code, the remote method invocation works like this:

~~~~C#

var bodyResult =

 connection

        .XxxxxxxMethod() // Some RTSP Method like GetOption, Play

        .AddHeader( new RTSPHeaderContentType( RTSPMimeType.ApplicationText ) )
        .AddHeader( "X-Header1" , "my value 1")
        .AddHeader( "X-Header2" , "my value 2")
        .AddHeader( "X-Header3" , "my value 3")
        .AddHeader( "X-Header4" , "my value 4")
        .AddHeader( "X-Header5" , "my value 5")
        .AddHeader( "X-Header6" , "port_range={0}-{1];timeout={2};" , 1234 , 4321 , 5000 )

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

You will be able to decorate each request by adding customs headers, because some cameras can not reply to a request that just contains only standard headers or if there the message contains incomplete headers. If you want to invoke a method on a particular server, you MUST read the server documentation especially the SETUP method. For instance, the SETUP are used to ask to the camera to create a streaming session based on RTP multicast channel.

Actual, I am "redesigning" the objects related to the rtsp communication layer, the final connection will be similar to the following code:

~~~~C#

public interface IRtspConnection : IDisposable
{
	event EventHandler<RtspOpenedEventArgs> Opened;
	event EventHandler<RtspClosedEventArgs> Closed; // The close eventargs will contains the reason why the connection has been closed

	event EventHandler<RtspAuthenticationFailedEventArgs> AuthenticationFailed;

	event EventHandler<RtspMessageEventArgs> MessageReceived;
	event EventHandler<RtspMessageEventArgs> MessageSended;

	event EventHandler<RtspDataReceiveEventArgs> DataReceived;

	event EventHandler<RtspErrorEventArg> ReceivedError;
	event EventHandler<RtspErrorEventArg> SendedError;

	event EventHandler<RtspErrorEventArg> OpenedFailed;

	event EventHandler<RtspSessionEventArg> SessionCreated;
	event EventHandler<RtspSessionEventArg> SessionClosed;
	event EventHandler<RtspErrorEventArg> SessionError;


	string Uri { get; }    // store the uri without the credentials
	string Server { get; } // hostname or ip stored on the uri
	int Port { get; }      // the port on the uri
	TimeSpan ConnectionTimeout { get; }
	TimeSpan ReceiveTimeout { get; }
	TimeSpan SendTimeout { get; }
	int CurrentSequenceId { get;}
	RTSPConnectionState Status { get; }

	IReadOnlyCollection<RtspSessionInfo> Sessions { get; } // the collection will updated internal by some classes like the differents implementation of invokers

	void Open(string uri);
	void Open(string uri,TimeSpan openTimeout);
	void Open(string uri,TimeSpan openTimeout , string userName , string password );
	void Open(string uri,TimeSpan openTimeout , string userName , SecureString password );
	void Close();
	void ConfigureTimeouts(TimeSpan ioTimeout);
	void ConfigureTimeouts(TimeSpan receiveTimeout,TimeSpan sendTimeout);
	
	IRtspInvoker GetOptions(); // Gets the default invoker used to call the OPTIONS method
	IRtspInvoker Describe();   // Gets the describe invoker blablabla
	IRtspInvoker Setup();      // Gets the setup invoker blablabla
	IRtspInvoker Setup(string trackUri);
	IRtspInvoker Play(); // Get the default play invoker
	IRtspInvoker Play(string sessionId); // Throw exception if session id does not exist and add the correspondings headers
	IRtspInvoker Pause(); // blablabla
	IRtspInvoker Pause(string sessionId); // Throw exception if session id does not exist and add the correspondings headers
	IRtspInvoker TearDown();
	IRtspInvoker TearDown(string sessionId); // Throw exception if session id does not exist and add the correspondings headers
	IRtspInvoker Record();
	IRtspInvoker Announce();
	IRtspInvoker GetParameter();
	IRtspInvoker SetParameter();

	// I will move these methods below on the class implementation
	// Or move it on seperate interface using segregation patterns
	
	TInvoker CreateInvoker<TInvoker>() where TInvoker : class, IRtspInvoker;
	IRtspInvoker CreateInvoker(string method);
	IRtspInvoker CreateInvoker(string method, IDictionary<string,string> headers);
	IRtspInvoker CreateInvoker(string method, IEnumerable<KeyValuePair<string,string>> headers);
	IRtspInvoker CreateMutlicastSessionInvoker(string trackUri,string address, int port);
	IRtspInvoker CreateMutlicastSessionInvoker(string trackUri,string address, int port,int ttl);
	IRtspInvoker CreateUnicastSessionInvoker(string trackUri);
	IRtspInvoker CreateUnicastSessionInvoker(string trackUri,string address);
	IRtspInvoker CreateUnicastSessionInvoker(string trackUri,string address,int port);
	IRtspInvoker KeepAlive(); // implement the common ping strategy
	IRtspInvoker KeepAlive(int keepAliveMode); 
}

public enum RTSPConnectionState { Closed , Opening , Opened, Broken, }

// A possible implementation of session info class
// This session info will be created by the SetupInvoker class 
public sealed class RtspSessionInfo
{
	public string UniqueId { get; private set; }
	public string TrackUri { get; private set; }
	public DateTime CreationTime { get; private set; }
	public TimeSpan Timeout { get; private set; }
	public TimeSpan ExpirationTimeout { get; private set; }
	public DateTime TimeStamp { get; private set; }
	public bool IsPlaying { get; private set; }
	public bool IsPaused { get; private set; }

	public string Address { get; private set; }
	public int Port { get; private set; }
	public int TTL { get; private set; }
	public Guid ProtocolType { get; private set; }
	public Guid MediaType { get; private set; }

	public object Tag { get; set; }

	public bool HasExpired()
	{
	   // Something like this
		return TimeStamp.Add( ExpirationTimeout ) < DateTime.Now;
	}

	internal void KeepAlive()
	{
		TimeStamp = DateTime.Now;
	}

	internal void ChangePlayStatus( bool status ) {
		throw new NotImplementedException()
	}

	internal void ChangePauseStatus( bool status ) {
		throw new NotImplementedException()
	}

	internal static RtspSessionInfo CreateInterleavedSessionInfo( /* string id , Guid mediaType .... */ )
	{
		throw new NotImplementedException()
	}

	internal static RtspSessionInfo CreateMulticastSessionInfo( /* string id , Guid mediaType .... */ )
	{
		throw new NotImplementedException()
	}

	internal static RtspSessionInfo CreateUnicastSessionInfo( /* string id , Guid mediaType .... */ )
	{
		throw new NotImplementedException()
	}

	internal static RtspSessionInfo CreateXXXXXSessionInfo( /* string id , Guid mediaType .... */ )
	{
		throw new NotImplementedException()
	}
}

~~~~
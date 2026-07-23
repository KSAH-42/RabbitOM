# A resilent RTSP client based on the .net framework

[![Build](https://github.com/KSAH-42/RabbitOM/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/KSAH-42/RabbitOM/actions/workflows/dotnet-desktop.yml)

![Player](https://github.com/KSAH-42/RabbitOM/blob/master/Resources/Images/RabbitOM.Sample.Client.png)

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
* Support multiple authentication schemes as: basic and digest ( MD5, SHA1, SHA256, SHA512 )
* Support RTP packets reordering
* Support RTSP messages reordering when multiple requests are sended and responses arrive in a different order
* Support Unicast TCP (interleaved mode) transport
* Support Unicast UDP transport 
* Support Multicast transport
* Support auto reconnection in case of network failures
* Support events Handlers for connection loss, receiving packet, etc...
* Reduce memory copy when using large memory blocks by using System.ArraySegment<byte> in order to minimize the usage of System.Buffer.BlockCopy
* Force the creation of ports used for receiving packets in case if the ports are temporaly used by some others applications

➡️ Breaking changes since the version 2.0.0.2:

* Refactorization of rtp layers as (H264,H265,H266)
* Namespace reorganization
* Rtp layer refactorization and adding add packet inspection, adding support of H264, H265, G711, and so on.
* Add new class libray for rending jpeg using wpf
* Improve cpu an memory consumption during rending

➡️ Breaking changes since the version 2.0.0.4:

* namespace reorganization
* adding H264 player 
* adding H265 player 

➡️ Next arrivals:

* Adding next RTSP Client 
* Adding RTSP receivers
* Adding RTCP layer 
* Onvif

The actual RtspClient class WILL BE REMOVED (see streaming.experimentation project which is actually in progress)

# About the actual rtsp client and how to receive packets ?

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

# About Player samples

# MJpeg Player used to decode RTP packets ( RFC 2435 )

This sample demonstrate how to create MJpeg player to reconstruct a complete frame from jpeg fragments using a homemade jpeg builder without any externals dependencies.

![Player](https://github.com/KSAH-42/RabbitOM/blob/master/Resources/Images/RabbitOM.Sample.Client.Mjpeg.png)

# H264 Player used to decode RTP packets ( RFC 6184 )

This sample demonstrate how to create h264 decoder using FFMpeg.AutoGen dependencies.

![Player](https://github.com/KSAH-42/RabbitOM/blob/master/Resources/Images/RabbitOM.Sample.Client.H264.png)

# H265/HVEC Player used to decode RTP packets ( RFC 7798 )

This sample demonstrate how to create h265 decoder using FFMpeg.AutoGen dependencies.

![Player](https://github.com/KSAH-42/RabbitOM/blob/master/Resources/Images/RabbitOM.Sample.Client.H265.png)


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
            client.BaseAddress = new Uri( "rtsp://127.0.0.1:554/toxic-society.mp4" );
            
            client.Headers.Accept = new AcceptRtspHeaderValue();
            client.Headers.Accept.Values.Add( new MediaTypeWithQualityRtspHeaderValue("application/text") );
            
            var response = await client.OptionsAsync( new RtspClientRequestOptionsBuilder()
                .SetUri( "*" )
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

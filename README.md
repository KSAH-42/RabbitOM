# RabbitOM

# Introduction

This is a library (in .net) used to connect, managed, and received video/audio streams from security camera using standard protocols like:

* SDP (actually implemented)
* RTSP (not actually implemented)
* RTP (not actually implemented)
* RTCP (not actually implemented)
* Onvif (not actually implemented)


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

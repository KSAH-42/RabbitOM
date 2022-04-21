# RabbitOM

# Introduction

After many research on Internet, I didn't find a stable library (in .net) used to connect, managed, and received video/audio stream from security camera using standard protocols like:

* SDP (actually implemented)
* RTSP (not actually implemented)
* RTP (not actually implemented)
* RTCP (not actually implemented)
* Onvif (not actually implemented)


# About Session Description Protocol

The actual implementation provide a strong type objects. I found many implementation that just implement a SDP using a dictionary of string/string or string/object. In many projects, when people add more and more features, it may difficult to access to the data. Using a simple dictionary introduce anti pattern called primitive obsession anti pattern. To avoid this ugly approach of using a just a dictionary, I decided to implement a set of classes that provide a better access to data located inside SDP document. This implementation has been tested with many security camera models and RTSP servers. The serialization mecanism MUST respect a certain order. So here, you will find a tolerant serializer. This actual implementation provide a tolerant serialization mecanism that handle many cases, like formating issue, case sensitive issue, ordering issues, extra whitespaces, etc... 

Example:

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
sessionDescriptor.Uri.Value = "rtsp://192.168.0.11:554";

for ( int i = 1; i <= 10; ++ i )
{
    var mediaAttribute = new MediaDescriptionField();

    mediaAttribute.Payload = 1 + i;
    mediaAttribute.Port = 10 + i;
    mediaAttribute.Profile = ProfileType.AVP;
    mediaAttribute.Protocol = ProtocolType.RTP;
    mediaAttribute.Type = MediaType.Video;

    mediaAttribute.Encryption.Key = "myKey"+i.ToString();
    mediaAttribute.Encryption.Method = "myMethod"+i.ToString();
    mediaAttribute.Connection.Address = "192.168.0."+i.ToString();
    mediaAttribute.Connection.AddressType = AddressType.IPV4;
    mediaAttribute.Connection.NetworkType = NetworkType.Internet;
    mediaAttribute.Bandwiths.Add(new BandwithField("modifier", i));
    mediaAttribute.Bandwiths.Add(new BandwithField("modifier"+i.ToString(), i+i));
    mediaAttribute.Attributes.Add(new AttributeField("myAttribute1", "myValue1"));
    mediaAttribute.Attributes.Add(new AttributeField("myAttribute2", "myValue2"));
    mediaAttribute.Attributes.Add(new AttributeField("myAttribute3", "myValue3"));

    sessionDescriptor.MediaDescriptions.Add(mediaAttribute);
}

Console.WriteLine(sessionDescriptor.ToString());

if ( SessionDescriptor.TryParse( sessionDescriptor.ToString() , out SessionDescriptor descriptor ) )
{
    Console.WriteLine("Ok");
}

~~~~

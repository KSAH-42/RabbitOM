using System;

namespace RabbitOM.Tests.Sdp
{
    using RabbitOM.Streaming.Net.Sdp;

    class Program
    {
        static void Main( string[] args )
        {
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
            sessionDescriptor.Emails.Add( new EmailField("a@b.com","a"));
            sessionDescriptor.Emails.Add( new EmailField("c@d.com","c"));
            sessionDescriptor.Times.Add( new TimeField(1,2) );
            sessionDescriptor.Times.Add( new TimeField(3,4) );
            sessionDescriptor.TimeZone.Value = "Paris";
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

        }
    }
}

﻿using System;

namespace RabbitOM.Net.Sdp.Tests.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var descriptor = new SessionDescriptor();

			descriptor.Origin.UserName = "John";
            descriptor.Origin.Address = "192.168.1.23";
            descriptor.Origin.AddressType = AddressType.IPV6;
            descriptor.Origin.NetworkType = NetworkType.Internet;
            descriptor.Origin.Version = 1;
            descriptor.Origin.SessionId = 123456789;
            descriptor.Version.Value = 1;
            descriptor.SessionInformation.Value = "some infos";
            descriptor.SessionName.Value = "My session name";
            descriptor.Uri.Value = "rtsp://192.168.0.11:554";
            descriptor.Repeats.Add(new RepeatField(new ValueTime(1, 2), new ValueTime(3, 4)));
            descriptor.Repeats.Add(new RepeatField(new ValueTime(10, 20), new ValueTime(30, 40)));
            descriptor.Attributes.Add(new AttributeField("myAttribute1", "myValue1"));
            descriptor.Attributes.Add(new AttributeField("myAttribute2", "myValue2"));
            descriptor.Attributes.Add(new AttributeField("myAttribute2", "myValue3"));
            descriptor.Phones.Add(new PhoneField("+33 1 12 34 56 78"));
            descriptor.Phones.Add(new PhoneField("+33 1 12 34 56 79"));
            descriptor.Emails.Add(new EmailField("rabbit1@hole.com", "rabbit1"));
            descriptor.Emails.Add(new EmailField("rabbit2@hole.com", "rabbit2"));
            descriptor.Emails.Add(new EmailField("rabbit3@hole.com", "rabbit3"));
            descriptor.Emails.Add(new EmailField("rabbit4@hole.com", "rabbit4"));
                                    
            for ( int i = 1; i <= 10; ++ i )
            {
                var mediaDescription = new MediaDescriptionField();

                mediaDescription.Payload = 1 + i;
                mediaDescription.Port = 10 + i;
                mediaDescription.Profile = ProfileType.SAVP;
                mediaDescription.Protocol = ProtocolType.RTP;
                mediaDescription.Type = MediaType.Video;

                mediaDescription.Encryption.Key = "myKey"+i.ToString();
                mediaDescription.Encryption.Method = "myMethod"+i.ToString();
                mediaDescription.Connection.Address = "192.168.0."+i.ToString();
                mediaDescription.Connection.AddressType = AddressType.IPV4;
                mediaDescription.Connection.NetworkType = NetworkType.Internet;
                mediaDescription.Connection.TTL = 1;
                mediaDescription.Bandwiths.Add(new BandwithField("modifier", i));
                mediaDescription.Bandwiths.Add(new BandwithField("modifier"+i.ToString(), i+i));
                mediaDescription.Attributes.Add(new AttributeField("myAttribute1", "myValue1"));
                mediaDescription.Attributes.Add(new AttributeField("myAttribute2", "myValue2"));
                mediaDescription.Attributes.Add(new AttributeField("myAttribute3", "myValue3"));

                descriptor.MediaDescriptions.Add(mediaDescription);
            }
            
            var text = descriptor.ToString();

            Console.WriteLine(text);

            if ( SessionDescriptor.TryParse( SdpSampleFactory.CreateSimpleSdp() , out SessionDescriptor sdp ) )
            {
                Console.WriteLine("Ok");
				Console.WriteLine(sdp.ToString());
            }
        }
    }
}

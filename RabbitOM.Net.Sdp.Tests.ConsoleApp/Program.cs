using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitOM.Net.Sdp.Tests.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
				
			var sessionDescriptor = new SessionDescriptor();

			sessionDescriptor.Repeats.Remove(null);
			sessionDescriptor.SessionName.Value = "the session";
			sessionDescriptor.Repeats.Add(new RepeatField(new ValueTime(1, 2), new ValueTime(3, 4)));
			sessionDescriptor.Repeats.Add(new RepeatField());
			sessionDescriptor.Repeats.Add(new RepeatField());
			sessionDescriptor.Repeats.Add(new RepeatField());
			sessionDescriptor.Repeats.TryAdd(null);
			sessionDescriptor.Origin.Address = "192.168.1.23";
			sessionDescriptor.Origin.AddressType = AddressType.IPV4;
			sessionDescriptor.Origin.NetworkType = NetworkType.Internet;
			sessionDescriptor.Origin.UserName = "Kader";
			sessionDescriptor.Origin.Version = "V1";
			sessionDescriptor.Origin.SessionId = "1234";
			sessionDescriptor.Attributes.TryAdd(null);
			sessionDescriptor.Attributes.TryAdd(new AttributeField("key1", "val1"));
			sessionDescriptor.Attributes.TryAdd(new AttributeField("key1", "val1"));
			sessionDescriptor.Attributes.TryAdd(new AttributeField("key2", "val2"));
			


			sessionDescriptor.Phones.TryAdd(null);
			sessionDescriptor.Phones.TryAdd(new PhoneField("+33 1 12 34 56 78"));
			sessionDescriptor.Phones.TryAdd(new PhoneField("+33 1 12 34 56 79"));
			sessionDescriptor.Uri.Value = "rtsp://123.123.35.5:34";
			
			Console.WriteLine(sessionDescriptor.Attributes.FirstOrDefault( x => x.Name == " key1 "));

			var session = SessionDescriptor.Parse(sessionDescriptor.ToString());

			Console.WriteLine(session);
			Console.WriteLine();
			Console.WriteLine(sessionDescriptor);
		}
	}
}

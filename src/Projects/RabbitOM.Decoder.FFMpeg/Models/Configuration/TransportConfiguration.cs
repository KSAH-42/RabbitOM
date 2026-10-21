using System.Text.Json.Serialization;

namespace RabbitOM.Decoder.FFMpeg.Models.Configuration
{
	[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
	[JsonDerivedType(typeof(SharedMemoryTransportConfiguration), typeDiscriminator: "shared-memory")]
	[JsonDerivedType(typeof(HttpSenderTransportConfiguration), typeDiscriminator: "http-sender")]
	[JsonDerivedType(typeof(HttpTransportConfiguration), typeDiscriminator: "http")]
	public class TransportConfiguration
	{
	}
}

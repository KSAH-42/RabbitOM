using System.Text.Json.Serialization;

namespace RabbitOM.Decoder.FFMpeg.Models.Configuration
{
	[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
	[JsonDerivedType(typeof(SharedMemoryTransportConfiguration), typeDiscriminator: "shared-memory")]
	[JsonDerivedType(typeof(TcpSenderTransportConfiguration), typeDiscriminator: "tcp-sender")]
	public class TransportConfiguration
	{
	}
}

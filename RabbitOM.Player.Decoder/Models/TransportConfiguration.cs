using System.Text.Json.Serialization;

namespace RabbitOM.Player.Decoder.Models
{
	[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
	[JsonDerivedType(typeof(SharedMemoryTransportConfiguration), typeDiscriminator: "shared-memory")]
	[JsonDerivedType(typeof(TcpSenderTransportConfiguration), typeDiscriminator: "tcp-sender")]
	public class TransportConfiguration
	{
	}
}

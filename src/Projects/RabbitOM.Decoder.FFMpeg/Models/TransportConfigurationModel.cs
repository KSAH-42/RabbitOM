using System.Text.Json.Serialization;

namespace RabbitOM.Decoder.FFMpeg.Models
{
	[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
	[JsonDerivedType(typeof(SharedMemoryConfigurationModel), typeDiscriminator: "shared-memory")]
	[JsonDerivedType(typeof(HttpSenderConfigurationModel), typeDiscriminator: "http-sender")]
	[JsonDerivedType(typeof(HttpConfigurationModel), typeDiscriminator: "http")]
	public class TransportConfigurationModel
	{
	}
}

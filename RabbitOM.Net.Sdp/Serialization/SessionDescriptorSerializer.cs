namespace RabbitOM.Net.Sdp.Serialization
{
	/// <summary>
	/// Represent a tolerant sdp serializer
	/// </summary>
	/// <remarks>
	///    <para>According to the global authorities, the serialization mecanism MUST respect a certain order: please follow this link to get more details <see href="https://tools.ietf.org/html/rfc4566#page-39"/></para>
	/// </remarks>
	public static class SessionDescriptorSerializer
	{
		/// <summary>
		/// Serialize
		/// </summary>
		/// <param name="sdp">the sdp</param>
		/// <returns>returns string</returns>
		public static string Serialize(SessionDescriptor sdp)
		{
			if (sdp == null)
			{
				return string.Empty;
			}

			var writer = new SessionDescriptorWriter();

			writer.WriteField(sdp.Version);
			writer.WriteField(sdp.Origin);
			writer.WriteField(sdp.SessionName);
			writer.WriteField(sdp.SessionInformation);
			writer.WriteField(sdp.Uri);
			writer.WriteFields(sdp.Emails);
			writer.WriteFields(sdp.Phones);
			writer.WriteField(sdp.Connection);
			writer.WriteFields(sdp.Bandwiths);
			writer.WriteFields(sdp.Times);
			writer.WriteFields(sdp.Repeats);
			writer.WriteField(sdp.TimeZone);
			writer.WriteField(sdp.Encryption);
			writer.WriteFields(sdp.Attributes);

			foreach (var mediaDescription in sdp.MediaDescriptions)
			{
				writer.WriteField(mediaDescription);
				writer.WriteField(mediaDescription.Connection);
				writer.WriteFields(mediaDescription.Bandwiths);
				writer.WriteField(mediaDescription.Encryption);
				writer.WriteFields(mediaDescription.Attributes);
			}

			return writer.ToString();
		}

		/// <summary>
		/// Deserialize
		/// </summary>
		/// <param name="input">the input value</param>
		/// <returns>returns an instance</returns>
		public static SessionDescriptor Deserialize(string input)
		{
			using (var reader = new SessionDescriptorReader(input))
			{
				var builder = new SessionDescriptorBuilder();

				while (reader.Read())
				{
					if (reader.IsVersionHeader)
					{
						builder.SetVersion(reader.CurrentHeaderValue);
					}

					else if (reader.IsOriginHeader)
					{
						builder.SetOrigin(reader.CurrentHeaderValue);
					}

					else if (reader.IsSessionNameHeader)
					{
						builder.SetSessionName(reader.CurrentHeaderValue);
					}

					else if (reader.IsSessionInformationHeader)
					{
						builder.SetSessionInformation(reader.CurrentHeaderValue);
					}

					else if (reader.IsUriHeader)
					{
						builder.SetUri(reader.CurrentHeaderValue);
					}

					else if (reader.IsEmailHeader)
					{
						builder.AddEmail(reader.CurrentHeaderValue);
					}

					else if (reader.IsPhoneHeader)
					{
						builder.AddPhone(reader.CurrentHeaderValue);
					}

					else if (reader.IsConnectionHeader)
					{
						builder.SetConnection(reader.CurrentHeaderValue);
					}

					else if (reader.IsBandwithHeader)
					{
						builder.AddBandwith(reader.CurrentHeaderValue);
					}

					else if (reader.IsTimeHeader)
					{
						builder.AddTime(reader.CurrentHeaderValue);
					}

					else if (reader.IsRepeatHeader)
					{
						builder.AddRepeat(reader.CurrentHeaderValue);
					}

					else if (reader.IsTimeZoneHeader)
					{
						builder.SetTimeZone(reader.CurrentHeaderValue);
					}

					else if (reader.IsEncryptionHeader)
					{
						builder.SetEncryption(reader.CurrentHeaderValue);
					}

					else if (reader.IsAttributeHeader)
					{
						builder.AddAttribute(reader.CurrentHeaderValue);
					}

					if (reader.IsUnderMediaSection)
					{
						do
						{
							if (reader.IsMediaDescriptionHeader)
							{
								builder.CreateMediaDescription(reader.CurrentHeaderValue);
							}

							if (reader.IsConnectionHeader)
							{
								builder.SetMediaConnection(reader.CurrentHeaderValue);
							}

							if (reader.IsEncryptionHeader)
							{
								builder.SetMediaEncryption(reader.CurrentHeaderValue);
							}

							if (reader.IsBandwithHeader)
							{
								builder.AddMediaBandwith(reader.CurrentHeaderValue);
							}

							if (reader.IsAttributeHeader)
							{
								builder.AddMediaAttribute(reader.CurrentHeaderValue);
							}
						}
						while (reader.Read() && reader.IsUnderMediaSection);
					}
				}

				return builder.Build();
			}
		}
	}
}

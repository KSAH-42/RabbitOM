using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Net.Sdp.Serialization.Formatters
{
	/// <summary>
	/// Represent a class used to format and parse data
	/// </summary>
	public static class FormatAttributeValueFormatter
	{
		/// <summary>
		/// Format to string the field
		/// </summary>
		/// <param name="field">the field</param>
		/// <returns>returns a string</returns>
		public static string Format(FormatAttributeValue field)
		{
			if (field == null)
			{
				return string.Empty;
			}

			var builder = new StringBuilder();

			builder.AppendFormat("{0} ", field.PayloadType);
			builder.AppendFormat("{0}={1};", FormatAttributeValue.TypePacketizationMode, field.PacketizationMode);

			if (!string.IsNullOrWhiteSpace(field.ProfileLevelId))
			{
				builder.AppendFormat(" {0}={1};", FormatAttributeValue.TypeProfileLevelId, field.ProfileLevelId);
			}

			if (!string.IsNullOrWhiteSpace(field.PPS))
			{
				builder.AppendFormat(" {0}={1},{2};", FormatAttributeValue.TypeSPropParmeterSets, field.SPS, field.PPS);
			}

			if (!string.IsNullOrWhiteSpace(field.Mode))
			{
				builder.AppendFormat(" {0}={1};", FormatAttributeValue.TypeMode, field.Mode);
			}

			if (field.SizeLength.HasValue)
			{
				builder.AppendFormat(" {0}={1};", FormatAttributeValue.TypeSizeLength, field.SizeLength);
			}

			if (field.IndexLength.HasValue)
			{
				builder.AppendFormat(" {0}={1};", FormatAttributeValue.TypeIndexLength, field.IndexLength);
			}

			if (field.IndexDeltaLength.HasValue)
			{
				builder.AppendFormat(" {0}={1};", FormatAttributeValue.TypeIndexDeltaLength, field.IndexDeltaLength);
			}

			if (!string.IsNullOrWhiteSpace(field.Configuration))
			{
				builder.AppendFormat(" {0}={1};", FormatAttributeValue.TypeConfiguration, field.Configuration);
			}

			if (!field.Extensions.IsEmpty)
			{
				foreach (var extension in field.Extensions)
				{
					builder.AppendFormat(" {0}", extension);
				}
			}

			return builder.ToString();
		}

		/// <summary>
		/// Try to parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="result">the field result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryParse(string value, out FormatAttributeValue result)
		{
			result = null;

			if (string.IsNullOrWhiteSpace(value))
			{
				return false;
			}

			var tokens = value.Trim().Split(new char[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);

			if (!tokens.Any())
			{
				return false;
			}

			foreach (var token in tokens)
			{
				if (!StringPair.TryParse(token, new char[] { '=', ':' }, out StringPair pair))
				{
					continue;
				}

				if (result == null)
				{
					result = new FormatAttributeValue()
					{
						PayloadType = SessionDescriptorDataConverter.ConvertToByte(tokens.FirstOrDefault() ?? string.Empty),
					};
				}

				if (string.Compare(pair.First, AttributeNames.FormatPayload, true) == 0)
				{
					result.PayloadType = SessionDescriptorDataConverter.ConvertToByte(pair.Second);
				}

				else

				if (string.Compare(pair.First, FormatAttributeValue.TypeProfileLevelId, true) == 0)
				{
					result.ProfileLevelId = pair.Second;
				}

				else

				if (string.Compare(pair.First, FormatAttributeValue.TypePacketizationMode, true) == 0)
				{
					result.PacketizationMode = SessionDescriptorDataConverter.ConvertToLong(pair.Second);
				}

				else

				if (string.Compare(pair.First, FormatAttributeValue.TypeSPropParmeterSets, true) == 0)
				{
					if (!string.IsNullOrWhiteSpace(pair.Second))
					{
						var ppValues = pair.Second.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

						result.SPS = ppValues.ElementAtOrDefault(0);
						result.PPS = ppValues.ElementAtOrDefault(1);
					}
				}

				else

				if (string.Compare(pair.First, FormatAttributeValue.TypeMode, true) == 0)
				{
					result.Mode = pair.Second;
				}

				else

				if (string.Compare(pair.First, FormatAttributeValue.TypeSizeLength, true) == 0)
				{
					result.SizeLength = SessionDescriptorDataConverter.ConvertToNullableLong(pair.Second);
				}

				else

				if (string.Compare(pair.First, FormatAttributeValue.TypeIndexLength, true) == 0)
				{
					result.IndexLength = SessionDescriptorDataConverter.ConvertToNullableLong(pair.Second);
				}

				else

				if (string.Compare(pair.First, FormatAttributeValue.TypeIndexDeltaLength, true) == 0)
				{
					result.IndexDeltaLength = SessionDescriptorDataConverter.ConvertToNullableLong(pair.Second);
				}

				else

				if (string.Compare(pair.First, FormatAttributeValue.TypeConfiguration, true) == 0)
				{
					result.Configuration = pair.Second;
				}

				else
				{
					result.Extensions.TryAdd(token);
				}
			}

			return result != null;
		}
	}
}

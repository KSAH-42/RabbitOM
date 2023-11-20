using System;
using System.Text;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent a sdp field factory
	/// </summary>
	public static class AttributeFieldFactory
	{
		/// <summary>
		/// Create an attribute
		/// </summary>
		/// <returns>returns an instance</returns>
		public static AttributeField NewSendReceiveAttribute()
		{
			return new AttributeField(AttributeNames.SendReceive);
		}

		/// <summary>
		/// Create an attribute
		/// </summary>
		/// <returns>returns an instance</returns>
		public static AttributeField NewSendOnlyAttribute()
		{
			return new AttributeField(AttributeNames.SendOnly);
		}

		/// <summary>
		/// Create an attribute
		/// </summary>
		/// <returns>returns an instance</returns>
		public static AttributeField NewReceiveOnlyAttribute()
		{
			return new AttributeField(AttributeNames.ReceiveOnly);
		}

		/// <summary>
		/// Create an attribute
		/// </summary>
		/// <param name="uri">the uri</param>
		/// <returns>returns an instance</returns>
		/// <exception cref="ArgumentNullException"/>
		public static AttributeField NewControlAttribute(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException(nameof(uri));
			}

			return new AttributeField(AttributeNames.Control, uri.ToString());
		}

		/// <summary>
		/// Create an attribute
		/// </summary>
		/// <param name="width">the width</param>
		/// <param name="heigth">the heigth</param>
		/// <returns>returns an instance</returns>
		/// <exception cref="ArgumentException"/>
		public static AttributeField NewDimensionsAttribute(long width, long heigth)
		{
			if (width <= 0)
			{
				throw new ArgumentException(nameof(width));
			}

			if (heigth <= 0)
			{
				throw new ArgumentException(nameof(heigth));
			}

			return new AttributeField(AttributeNames.Dimensions, $"{width},{heigth}");
		}

		/// <summary>
		/// Create an attribute
		/// </summary>
		/// <param name="payload">the payload</param>
		/// <param name="encoding">the encoding like H264</param>
		/// <param name="clockRate">the clock rate</param>
		/// <returns>returns an instance</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="ArgumentNullException"/>
		public static AttributeField NewRTPMapAttribute(long payload, string encoding, long clockRate)
		{
			if (payload < 0)
			{
				throw new ArgumentException(nameof(payload));
			}

			if (string.IsNullOrWhiteSpace(encoding))
			{
				throw new ArgumentNullException(nameof(encoding));
			}

			if (clockRate <= 0)
			{
				throw new ArgumentNullException(nameof(clockRate));
			}

			return new AttributeField(AttributeNames.RTPMap, $"{payload} {encoding}/{clockRate}");
		}

		/// <summary>
		/// Create an attribute
		/// </summary>
		/// <param name="type">the type</param>
		/// <param name="port">the port</param>
		/// <param name="protocol">the protocol</param>
		/// <param name="profile">the profile</param>
		/// <param name="format">the format</param>
		/// <returns>returns an instance</returns>
		/// <exception cref="ArgumentException"/>
		public static AttributeField NewMediaAttribute(MediaType type, int port, ProtocolType protocol, ProfileType profile, int format)
		{
			if (type == MediaType.None)
			{
				throw new ArgumentException(nameof(type));
			}

			if (port < 0)
			{
				throw new ArgumentException(nameof(port));
			}

			if (protocol == ProtocolType.None)
			{
				throw new ArgumentException(nameof(protocol));
			}

			if (profile == ProfileType.None)
			{
				throw new ArgumentException(nameof(profile));
			}

			if (format < 0)
			{
				throw new ArgumentException(nameof(format));
			}

			var builder = new StringBuilder();

			builder.Append(DataConverter.ConvertToString(type));
			builder.Append(" ");
			builder.Append(port);
			builder.Append(" ");
			builder.Append(DataConverter.ConvertToString(protocol));
			builder.Append(" ");
			builder.Append(DataConverter.ConvertToString(profile));
			builder.Append(" ");
			builder.Append(format);

			return new AttributeField(builder.ToString());
		}
	}
}

using System;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent a media track
	/// </summary>
	public sealed class MediaTrack
	{
		private readonly Guid _uniqueId = Guid.Empty;

		private string _controlUri = string.Empty;

		private string _mimeType = string.Empty;

		private string _address = string.Empty;

		private int _port = 0;

		private readonly RtpMapAttributeValue _rtpMap = new RtpMapAttributeValue();

		private readonly FormatAttributeValue _format = new FormatAttributeValue();




		/// <summary>
		/// Constructor
		/// </summary>
		public MediaTrack()
			: this(Guid.NewGuid())
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="uniqueId">the unique identifier</param>
		public MediaTrack(Guid uniqueId)
		{
			_uniqueId = uniqueId;
		}



		/// <summary>
		/// Gets the unique identifier
		/// </summary>
		public Guid UniqueId
		{
			get => _uniqueId;
		}

		/// <summary>
		/// Gets / Sets the control uri
		/// </summary>
		public string ControlUri
		{
			get => _controlUri;
			set => _controlUri = SessionDescriptorDataConverter.Filter(value);
		}

		/// <summary>
		/// Gets / Sets the mime type
		/// </summary>
		public string MimeType
		{
			get => _mimeType;
			set => _mimeType = SessionDescriptorDataConverter.Filter(value);
		}

		/// <summary>
		/// Gets / Sets the address
		/// </summary>
		public string Address
		{
			get => _address;
			set => _address = SessionDescriptorDataConverter.Filter(value);
		}

		/// <summary>
		/// Gets / Sets the port
		/// </summary>
		public int Port
		{
			get => _port;
			set => _port = value;
		}

		/// <summary>
		/// Gets the rtp map info
		/// </summary>
		public RtpMapAttributeValue RtpMap
		{
			get => _rtpMap;
		}

		/// <summary>
		/// Gets the format payload info
		/// </summary>
		public FormatAttributeValue Format
		{
			get => _format;
		}



		/// <summary>
		/// Check if the transport layer has been opened
		/// </summary>
		public bool Validate()
		{
			if (_uniqueId == Guid.Empty)
			{
				return false;
			}

			if (string.IsNullOrWhiteSpace(_controlUri) || string.IsNullOrWhiteSpace(_address))
			{
				return false;
			}

			if (_port <= 0)
			{
				return false;
			}

			return _rtpMap.TryValidate();
		}

		/// <summary>
		/// Create a media track
		/// </summary>
		/// <returns>returns an instance, otherwise null</returns>
		public static MediaTrack Create()
		{
			return Create(Guid.NewGuid());
		}

		/// <summary>
		/// Create a media track
		/// </summary>
		/// <param name="identifier">the identifier</param>
		/// <returns>returns an instance, otherwise null</returns>
		public static MediaTrack Create(Guid identifier)
		{
			return new MediaTrack(identifier);
		}
	}
}
